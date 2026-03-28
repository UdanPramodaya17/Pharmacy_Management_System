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
    public class PharmacyController
    {
        public int InsertNewPharmacy(Pharmacy newPharmacy)
        {
            using (SqlConnection newCon = DataAccessLayer.CreateConnection())
            {
                string insertPharmacyQuery = @"INSERT INTO PharmacyRegistration
                (PharmacyName, Address, PhoneNumber, Username, Password, VerifyPassword) 
                VALUES (@PharmacyName, @Address, @PhoneNumber, @Username, @Password, @VerifyPassword)";

                using (SqlCommand newCommand = new SqlCommand(insertPharmacyQuery, newCon))
                {
                    newCommand.Parameters.AddWithValue("@PharmacyName", newPharmacy.PharmacyName);
                    newCommand.Parameters.AddWithValue("@Address", newPharmacy.Address);
                    newCommand.Parameters.AddWithValue("@PhoneNumber", newPharmacy.PhoneNumber);
                    newCommand.Parameters.AddWithValue("@Username", newPharmacy.Username);
                    newCommand.Parameters.AddWithValue("@Password", newPharmacy.Password);
                    newCommand.Parameters.AddWithValue("@VerifyPassword", newPharmacy.VerifyPassword);

                    newCon.Open();
                    return newCommand.ExecuteNonQuery();
                }
            }
        }


        // Update Stock
        public int UpdatePharmacy(Pharmacy newPharmacy)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"UPDATE PharmacyRegistration
                                 SET PharmacyName = @PharmacyName, Address = @Address, PhoneNumber = @PhoneNumber, 
                                     Username = @Username, Password = @Password, VerifyPassword = @VerifyPassword
                                 WHERE PharmacyID = @PharmacyID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PharmacyID", newPharmacy.PharmacyID);
                    cmd.Parameters.AddWithValue("@PharmacyName", newPharmacy.PharmacyName);
                    cmd.Parameters.AddWithValue("@Address", newPharmacy.Address);
                    cmd.Parameters.AddWithValue("@PhoneNumber", newPharmacy.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Username", newPharmacy.Username);
                    cmd.Parameters.AddWithValue("@Password", newPharmacy.Password);
                    cmd.Parameters.AddWithValue("@VerifyPassword", newPharmacy.VerifyPassword);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete Stock
        public int DeletePharmacy(int PharmacyID)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = "DELETE FROM PharmacyRegistration WHERE PharmacyID = @PharmacyID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PharmacyID", PharmacyID);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Get All Stock
        public List<Pharmacy> GetAllPharmacy()
        {
            List<Pharmacy> PharmacyList = new List<Pharmacy>();

            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = "SELECT * FROM PharmacyRegistration";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    Pharmacy pharmacy = new Pharmacy
                    {
                        PharmacyID = Convert.ToInt32(row["PharmacyID"]),
                        PharmacyName = row["PharmacyName"].ToString(),
                        Address = row["Address"].ToString(),
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        Username = row["Username"].ToString(),
                        Password = row["Password"].ToString(),
                        VerifyPassword = row["VerifyPassword"].ToString()
                    };
                    PharmacyList.Add(pharmacy);
                }
            }
            return PharmacyList;
        }


        public bool VerifyPharmacyLogin(string username, string password)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"SELECT COUNT(*) 
                                 FROM PharmacyRegistration 
                                 WHERE Username = @Username AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

    }
}