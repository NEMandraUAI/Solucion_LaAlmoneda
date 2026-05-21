using BE.Entidades;
using DAL.Base;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorios
{
    public class UnidadVentaDAL
    {
        public void Guardar(UnidadVentaBE unidad, int? idPadre = null, SqlTransaction tx = null, SqlConnection conn = null)
        {
            bool manejarConexion = (conn == null);
            if (manejarConexion)
            {
                conn = ConexionDAL.ObtenerConexion();
                conn.Open();
                tx = conn.BeginTransaction();
            }
            try
            {
                string sql = @"INSERT INTO UnidadVenta (Nombre, Descripcion, PrecioBase, EsLote, IdPadre) 
                               VALUES (@Nombre, @Desc, @Precio, @EsLote, @IdPadre);
                               SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(sql, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@Nombre", unidad.Nombre);
                    cmd.Parameters.AddWithValue("@Desc", unidad.Descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Precio", unidad is LoteBE ? 0 : unidad.CalcularPrecioBase());
                    cmd.Parameters.AddWithValue("@EsLote", unidad is LoteBE);
                    cmd.Parameters.AddWithValue("@IdPadre", idPadre ?? (object)DBNull.Value);
                    unidad.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                if (unidad is LoteBE lote)
                {
                    foreach (var componente in lote.Componentes)
                    {
                        Guardar(componente, unidad.Id, tx, conn);
                    }
                }
                if (manejarConexion) tx.Commit();
            }
            catch (Exception)
            {
                if (manejarConexion) tx.Rollback();
                throw;
            }
            finally
            {
                if (manejarConexion)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        public List<UnidadVentaBE> ObtenerDisponiblesParaSubasta()
        {
            List<UnidadVentaBE> lista = new List<UnidadVentaBE>();
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string query = @"SELECT Id, Nombre, Descripcion, PrecioBase, EsLote 
                         FROM UnidadVenta 
                         WHERE IdPadre IS NULL AND Id NOT IN (
                         SELECT IdUnidadVenta FROM Subasta WHERE Estado != 'Cancelada'
                         )";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool esLote = Convert.ToBoolean(reader["EsLote"]);
                            if (esLote)
                            {
                                lista.Add(new LoteBE
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString()
                                });
                            }
                            else
                            {
                                lista.Add(new ArticuloBE
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    PrecioBaseHistorico = Convert.ToDecimal(reader["PrecioBase"])
                                });
                            }
                        }
                    }
                }
            }
            foreach (var item in lista)
            {
                if (item is LoteBE lote)
                {
                    lote.Componentes = CargarComponentesPorPadre(lote.Id);
                }
            }
            return lista;
        }
        private List<UnidadVentaBE> CargarComponentesPorPadre(int idPadre)
        {
            List<UnidadVentaBE> componentes = new List<UnidadVentaBE>();
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string query = "SELECT Id, Nombre, Descripcion, PrecioBase, EsLote FROM UnidadVenta WHERE IdPadre = @IdPadre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdPadre", idPadre);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool esLote = Convert.ToBoolean(reader["EsLote"]);
                            if (esLote)
                            {
                                componentes.Add(new LoteBE
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString()
                                });
                            }
                            else
                            {
                                componentes.Add(new ArticuloBE
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    PrecioBaseHistorico = Convert.ToDecimal(reader["PrecioBase"])
                                });
                            }
                        }
                    }
                }
            }
            foreach (var comp in componentes)
            {
                if (comp is LoteBE subLote)
                {
                    subLote.Componentes = CargarComponentesPorPadre(subLote.Id);
                }
            }
            return componentes;
        }
    }
}
