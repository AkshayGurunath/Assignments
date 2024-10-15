using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.util
{
    public class DBCUtil
    {
        // Method to get SqlConnection object using the provided property file
        public static SqlConnection GetDBConnection()
        {
            // Get the connection string from the property file
            string connectionString = "Data Source=LAPTOP-2H2P4CPV;Initial Catalog=SISDB;Integrated Security=True;Encrypt=False";
            SqlConnection connection = new SqlConnection(connectionString); ;
           
            return connection;
        }
    }
}

