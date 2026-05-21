using BE.Entidades;
using BE.Excepciones;
using DAL.Base;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorios
{
    public class SubastaDAL
    {
        public void GuardarPujaYActualizarSubasta(SubastaBE subasta, PujaBE puja)
        {
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                conn.Open();
                using (SqlTransaction tx = conn.BeginTransaction())
                {
                    try
                    {
                        string sqlUpdate = @"UPDATE Subasta 
                                            SET PrecioActual = @Precio, IdGanador = @IdGanador 
                                            WHERE Id = @Id AND FilaVersion = @FilaVersion";
                        using (SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, tx))
                        {
                            cmdUpdate.Parameters.AddWithValue("@Precio", subasta.PrecioActual);
                            cmdUpdate.Parameters.AddWithValue("@IdGanador", subasta.Ganador.Id);
                            cmdUpdate.Parameters.AddWithValue("@Id", subasta.Id);
                            cmdUpdate.Parameters.AddWithValue("@FilaVersion", subasta.FilaVersion);
                            int filasAfectadas = cmdUpdate.ExecuteNonQuery();
                            if (filasAfectadas == 0)
                            {
                                throw new ConcurrenciaException("Tu puja fue rechazada. Alguien más pujó o el estado cambió.");
                            }
                        }
                        string sqlInsert = @"INSERT INTO Puja (IdSubasta, IdUsuario, Monto, FechaHora) 
                                            VALUES (@IdSubasta, @IdUsuario, @Monto, @FechaHora)";
                        using (SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, tx))
                        {
                            cmdInsert.Parameters.AddWithValue("@IdSubasta", puja.Subasta.Id);
                            cmdInsert.Parameters.AddWithValue("@IdUsuario", puja.Usuario.Id);
                            cmdInsert.Parameters.AddWithValue("@Monto", puja.Monto);
                            cmdInsert.Parameters.AddWithValue("@FechaHora", puja.FechaHora);
                            cmdInsert.ExecuteNonQuery();
                        }
                        tx.Commit();
                    }
                    catch (Exception)
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }
        public List<SubastaBE> ObtenerTodasActivas()
        {
            List<SubastaBE> lista = new List<SubastaBE>();
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string query = @"SELECT s.Id, s.PrecioActual, s.Estado, s.FilaVersion, s.IdGanador, 
                                 u.Nombre AS NombreGanador,
                                 uv.Id AS IdUV, uv.Nombre AS NombreUV, uv.Descripcion AS DescUnidad, uv.EsLote, uv.PrecioBase
                                 FROM Subasta s
                                 INNER JOIN UnidadVenta uv ON s.IdUnidadVenta = uv.Id
                                 LEFT JOIN Usuario u ON s.IdGanador = u.Id
                                 WHERE s.Estado = 'Abierta'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool esLote = Convert.ToBoolean(reader["EsLote"]);
                            UnidadVentaBE unidad;
                            if (esLote)
                            {
                                unidad = new LoteBE
                                {
                                    Id = Convert.ToInt32(reader["IdUV"]),
                                    Nombre = reader["NombreUV"].ToString(),
                                    Descripcion = reader["DescUnidad"].ToString()
                                };
                            }
                            else
                            {
                                unidad = new ArticuloBE
                                {
                                    Id = Convert.ToInt32(reader["IdUV"]),
                                    Nombre = reader["NombreUV"].ToString(),
                                    Descripcion = reader["DescUnidad"].ToString(),
                                    PrecioBaseHistorico = Convert.ToDecimal(reader["PrecioBase"])
                                };
                            }
                            UsuarioBE ganadorObj = null;
                            if (reader["IdGanador"] != DBNull.Value)
                            {
                                ganadorObj = new UsuarioBE
                                {
                                    Id = Convert.ToInt32(reader["IdGanador"]),
                                    Nombre = reader["NombreGanador"].ToString()
                                };
                            }
                            lista.Add(new SubastaBE
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                PrecioActual = Convert.ToDecimal(reader["PrecioActual"]),
                                Estado = reader["Estado"].ToString(),
                                FilaVersion = (byte[])reader["FilaVersion"],
                                UnidadVenta = unidad,
                                TienePujas = reader["IdGanador"] != DBNull.Value,
                                Ganador = ganadorObj
                            });
                        }
                    }
                }
            }
            return lista;
        }
        public void CargarComponentesHijos(LoteBE lote)
        {
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string query = "SELECT Id, Nombre, Descripcion, PrecioBase, EsLote FROM UnidadVenta WHERE IdPadre = @IdPadre";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdPadre", lote.Id);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool esLote = Convert.ToBoolean(reader["EsLote"]);
                            UnidadVentaBE hijo;
                            if (esLote)
                            {
                                hijo = new LoteBE
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString()
                                };
                                CargarComponentesHijos((LoteBE)hijo);
                            }
                            else
                            {
                                hijo = new ArticuloBE
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    PrecioBaseHistorico = Convert.ToDecimal(reader["PrecioBase"])
                                };
                            }
                            lote.Agregar(hijo);
                        }
                    }
                }
            }
        }
        public void InsertarSubasta(SubastaBE subasta)
        {
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string sql = @"INSERT INTO Subasta (IdUnidadVenta, Estado, PrecioActual) 
                       VALUES (@IdUV, 'Abierta', @PrecioBase);
                       SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUV", subasta.UnidadVenta.Id);
                    cmd.Parameters.AddWithValue("@PrecioBase", subasta.UnidadVenta.CalcularPrecioBase());
                    conn.Open();
                    subasta.Id = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        public void ActualizarEstadoSubasta(SubastaBE subasta, string nuevoEstado)
        {
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string sql = "UPDATE Subasta SET Estado = @Estado, FechaHoraCierre = GETDATE() WHERE Id = @Id AND FilaVersion = @FilaVersion";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                    cmd.Parameters.AddWithValue("@Id", subasta.Id);
                    cmd.Parameters.AddWithValue("@FilaVersion", subasta.FilaVersion);
                    conn.Open();
                    int afectadas = cmd.ExecuteNonQuery();
                    if (afectadas == 0) throw new ConcurrenciaException("La subasta fue modificada por otro proceso.");
                }
            }
        }
        public List<SubastaBE> ObtenerSubastasCerradasDelDia()
        {
            List<SubastaBE> lista = new List<SubastaBE>();
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string query = @"SELECT s.Id, s.PrecioActual, s.Estado, s.FilaVersion, s.IdGanador, 
                                 u.Nombre AS NombreGanador,
                                 uv.Id AS IdUV, uv.Nombre AS NombreUV, uv.Descripcion AS DescUnidad, uv.EsLote, uv.PrecioBase
                                 FROM Subasta s
                                 INNER JOIN UnidadVenta uv ON s.IdUnidadVenta = uv.Id
                                 LEFT JOIN Usuario u ON s.IdGanador = u.Id
                                 WHERE s.Estado = 'Cerrada' 
                                 AND CAST(s.FechaHoraCierre AS DATE) = CAST(GETDATE() AS DATE)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool esLote = Convert.ToBoolean(reader["EsLote"]);
                            UnidadVentaBE unidad;
                            if (esLote)
                            {
                                unidad = new LoteBE
                                {
                                    Id = Convert.ToInt32(reader["IdUV"]),
                                    Nombre = reader["NombreUV"].ToString(),
                                    Descripcion = reader["DescUnidad"].ToString()
                                };
                            }
                            else
                            {
                                unidad = new ArticuloBE
                                {
                                    Id = Convert.ToInt32(reader["IdUV"]),
                                    Nombre = reader["NombreUV"].ToString(),
                                    Descripcion = reader["DescUnidad"].ToString(),
                                    PrecioBaseHistorico = Convert.ToDecimal(reader["PrecioBase"])
                                };
                            }
                            UsuarioBE ganadorObj = null;
                            if (reader["IdGanador"] != DBNull.Value)
                            {
                                ganadorObj = new UsuarioBE
                                {
                                    Id = Convert.ToInt32(reader["IdGanador"]),
                                    Nombre = reader["NombreGanador"].ToString()
                                };
                            }
                            lista.Add(new SubastaBE
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                PrecioActual = Convert.ToDecimal(reader["PrecioActual"]),
                                Estado = reader["Estado"].ToString(),
                                FilaVersion = (byte[])reader["FilaVersion"],
                                UnidadVenta = unidad,
                                TienePujas = reader["IdGanador"] != DBNull.Value,
                                Ganador = ganadorObj
                            });
                        }
                    }
                }
            }
            foreach (var sub in lista)
            {
                if (sub.UnidadVenta is LoteBE lote)
                {
                    CargarComponentesHijos(lote);
                }
            }
            return lista;
        }
    }
}