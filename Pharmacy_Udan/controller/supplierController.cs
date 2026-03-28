using Pharmacy_Udan.data;
using Pharmacy_Udan.module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Pharmacy_Udan.controller
{
    public class supplierController
    {
		public int InsertNewsupplier(Supplier newsupplier)
		{
			SqlConnection newCon = DataAccessLayer.CreateConnection();
			string insertsupplierQuery = @"insert into Supplier
(supplierid,suppliername,Supplieraddress,Supplieremail,Suppliercontact_number,SupplierPassword)values
(@supplierID,@SupplierName,@Supplieraddress,@Supplieremail,@Suppliercontact_number,@Supplierpass)";
			SqlCommand newcom = new SqlCommand(insertsupplierQuery, newCon);

			newcom.Parameters.AddWithValue("@supplierID", newsupplier.suplierId);
			newcom.Parameters.AddWithValue("@SupplierName", newsupplier.name);
			newcom.Parameters.AddWithValue("@Supplieraddress", newsupplier.address);
			newcom.Parameters.AddWithValue("@Supplieremail", newsupplier.email);
			newcom.Parameters.AddWithValue("@Suppliercontact_number", newsupplier.contact_number);
			newcom.Parameters.AddWithValue("@Supplierpass", newsupplier.Password);
			newCon.Open();
			return newcom.ExecuteNonQuery();

		}



        public int UpdateSupplier(Supplier updatedSupplier)
        {
            SqlConnection newCon = DataAccessLayer.CreateConnection();
            string updateSupplierQuery = @"UPDATE Supplier 
        SET suppliername = @SupplierName, 
            Supplieraddress = @Supplieraddress, 
            Supplieremail = @Supplieremail, 
            Suppliercontact_number = @Suppliercontact_number, 
            SupplierPassword = @Supplierpass 
        WHERE supplierid = @supplierID";

            SqlCommand newcom = new SqlCommand(updateSupplierQuery, newCon);
            newcom.Parameters.AddWithValue("@supplierID", updatedSupplier.suplierId);
            newcom.Parameters.AddWithValue("@SupplierName", updatedSupplier.name);
            newcom.Parameters.AddWithValue("@Supplieraddress", updatedSupplier.address);
            newcom.Parameters.AddWithValue("@Supplieremail", updatedSupplier.email);
            newcom.Parameters.AddWithValue("@Suppliercontact_number", updatedSupplier.contact_number);
            newcom.Parameters.AddWithValue("@Supplierpass", updatedSupplier.Password);

            newCon.Open();
            int rowsAffected = newcom.ExecuteNonQuery();
            newCon.Close();
            return rowsAffected;
        }


        public int DeleteSupplier(int supplierID)
        {
            SqlConnection newCon = DataAccessLayer.CreateConnection();
            string deleteSupplierQuery = @"DELETE FROM Supplier WHERE supplierid = @supplierID";

            SqlCommand newcom = new SqlCommand(deleteSupplierQuery, newCon);
            newcom.Parameters.AddWithValue("@supplierID", supplierID);

            newCon.Open();
            int rowsAffected = newcom.ExecuteNonQuery();
            newCon.Close();
            return rowsAffected;
        }


        public Supplier SearchSupplier(int supplierID)
        {
            SqlConnection newCon = DataAccessLayer.CreateConnection();
            string searchSupplierQuery = @"SELECT * FROM Supplier WHERE supplierid = @supplierID";
            SqlCommand newcom = new SqlCommand(searchSupplierQuery, newCon);
            newcom.Parameters.AddWithValue("@supplierID", supplierID);

            newCon.Open();
            SqlDataReader reader = newcom.ExecuteReader();
            Supplier supplier = null;
            if (reader.Read())
            {
                supplier = new Supplier
                {
                    suplierId = Convert.ToInt32(reader["supplierid"]),
                    name = reader["suppliername"].ToString(),
                    address = reader["Supplieraddress"].ToString(),
                    email = reader["Supplieremail"].ToString(),
                    contact_number = Convert.ToInt32(reader["Suppliercontact_number"]),
                    Password = reader["SupplierPassword"].ToString()
                };
            }
            newCon.Close();
            return supplier;
        }


        public bool VerifySupplierLogin(string supplierName, string Supplierpass)
        {
            SqlConnection newCon = DataAccessLayer.CreateConnection();
            string verifyQuery = @"SELECT COUNT(*) 
                          FROM Supplier 
                          WHERE suppliername = @SupplierName AND SupplierPassword = @Supplierpass";

            SqlCommand newcom = new SqlCommand(verifyQuery, newCon);
            newcom.Parameters.AddWithValue("@SupplierName", supplierName);
            newcom.Parameters.AddWithValue("@Supplierpass", Supplierpass);

            newCon.Open();
            int count = (int)newcom.ExecuteScalar();
            newCon.Close();

            return count > 0;  // Return true if a record exists, otherwise false
        }
















    }
}