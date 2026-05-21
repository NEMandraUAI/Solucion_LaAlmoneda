using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base
{
    internal static class ConexionDAL
    {
        private static readonly string CadenaConexion = @"Data Source=.;Initial Catalog=LaAlmonedaDB;Integrated Security=True;Trust Server Certificate=True";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(CadenaConexion);
        }
    }
}
