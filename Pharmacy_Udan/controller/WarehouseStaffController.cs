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
    public class WarehouseStaffController
    {

        public int InsertNewStaff(WarehouseStaff newStaff)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"INSERT INTO WarehouseStaff
                (FullName, Position, ContactNumber, Email, Password) 
                VALUES (@FullName, @Position, @ContactNumber, @Email, @Password)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@FullName", newStaff.FullName);
                    cmd.Parameters.AddWithValue("@Position", newStaff.Position);
                    cmd.Parameters.AddWithValue("@ContactNumber", newStaff.ContactNumber);
                    cmd.Parameters.AddWithValue("@Email", newStaff.Email);
                    cmd.Parameters.AddWithValue("@Password", newStaff.Password);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int UpdateStaff(WarehouseStaff staff)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"UPDATE WarehouseStaff
                                 SET FullName = @FullName, Position = @Position, ContactNumber = @ContactNumber,
                                     Email = @Email, Password = @Password
                                 WHERE StaffID = @StaffID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StaffID", staff.StaffID);
                    cmd.Parameters.AddWithValue("@FullName", staff.FullName);
                    cmd.Parameters.AddWithValue("@Position", staff.Position);
                    cmd.Parameters.AddWithValue("@ContactNumber", staff.ContactNumber);
                    cmd.Parameters.AddWithValue("@Email", staff.Email);
                    cmd.Parameters.AddWithValue("@Password", staff.Password);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DeleteStaff(int staffID)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = "DELETE FROM WarehouseStaff WHERE StaffID = @StaffID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StaffID", staffID);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public List<WarehouseStaff> GetAllStaff()
        {
            List<WarehouseStaff> staffList = new List<WarehouseStaff>();

            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = "SELECT * FROM WarehouseStaff";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    WarehouseStaff staff = new WarehouseStaff
                    {
                        StaffID = Convert.ToInt32(row["StaffID"]),
                        FullName = row["FullName"].ToString(),
                        Position = row["Position"].ToString(),
                        ContactNumber = Convert.ToInt32(row["ContactNumber"]),
                        Email = row["Email"].ToString(),
                        Password = row["Password"].ToString()
                    };
                    staffList.Add(staff);
                }
            }
            return staffList;
        }

        public bool VerifyStaffLogin(string email, string password)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"SELECT COUNT(*) 
                                 FROM WarehouseStaff 
                                 WHERE Email = @Email AND Password = @Password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    con.Open();
                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
        }

    }
}