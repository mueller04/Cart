using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCart.BLL
{
    public class DBFactory
    {
        public static IDbConnection GetLocalDbConnection()
        {
            string conn = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();
            IDbConnection connection = new SqlConnection(conn);
            connection.Open();
 
            return connection;
        }
    }
}

 
