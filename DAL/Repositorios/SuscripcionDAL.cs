using DAL.Base;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositorios
{
    public class SuscripcionDAL
    {
        public void GuardarSuscripcion(int idUsuario, int idSubasta)
        {
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string query = "INSERT INTO Suscripcion (IdUsuario, IdSubasta) VALUES (@IdUsu, @IdSub)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsu", idUsuario);
                    cmd.Parameters.AddWithValue("@IdSub", idSubasta);
                    conn.Open();
                    try { cmd.ExecuteNonQuery(); } catch { }
                }
            }
        }
        public void EliminarSuscripcion(int idUsuario, int idSubasta)
        {
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string query = "DELETE FROM Suscripcion WHERE IdUsuario = @IdUsu AND IdSubasta = @IdSub";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsu", idUsuario);
                    cmd.Parameters.AddWithValue("@IdSub", idSubasta);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<int> ObtenerSubastasSuscritasPorUsuario(int idUsuario)
        {
            List<int> ids = new List<int>();
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string query = "SELECT IdSubasta FROM Suscripcion WHERE IdUsuario = @IdUsu";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdUsu", idUsuario);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(Convert.ToInt32(reader["IdSubasta"]));
                        }
                    }
                }
            }
            return ids;
        }
    }
}
