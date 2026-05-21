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
    public class UsuarioDAL
    {
        public UsuarioBE ObtenerPorEmail(string email)
        {
            UsuarioBE usuarioLogueado = null;
            using (SqlConnection conn = ConexionDAL.ObtenerConexion())
            {
                string query = "SELECT Id, Nombre, Email, Contrasena, EsAdmin FROM Usuario WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuarioLogueado = new UsuarioBE
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nombre = reader["Nombre"].ToString(),
                                Email = reader["Email"].ToString(),
                                Contrasena = reader["Contrasena"].ToString(),
                                EsAdmin = Convert.ToBoolean(reader["EsAdmin"])
                            };
                        }
                    }
                }
            }
            return usuarioLogueado;
        }
    }
}
