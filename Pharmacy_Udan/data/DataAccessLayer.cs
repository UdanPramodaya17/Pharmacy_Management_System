using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Pharmacy_Udan.data
{
    public class DataAccessLayer
    {
        public static SqlConnection CreateConnection()
        {
            string connectionString = @"Server=LAPTOP-O6BJ19N8\SQLEXPRESS02;Database=spcdb;Trusted_Connection=True;";







            SqlConnection connection = new SqlConnection(connectionString);
            return connection;


        }
    }
}