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
    public class ManufacturingPlantController
    {
        public int InsertNewPlant(ManufacturingPlant newPlant)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"INSERT INTO ManufacturingPlants 
                                (PlantName, Location, ContactNumber, Email, Password) 
                                VALUES (@PlantName, @Location, @ContactNumber, @Email, @Password)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PlantName", newPlant.PlantName);
                    cmd.Parameters.AddWithValue("@Location", newPlant.Location);
                    cmd.Parameters.AddWithValue("@ContactNumber", newPlant.ContactNumber);
                    cmd.Parameters.AddWithValue("@Email", newPlant.Email);
                    cmd.Parameters.AddWithValue("@Password", newPlant.Password);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int UpdatePlant(ManufacturingPlant plant)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"UPDATE ManufacturingPlants 
                                 SET PlantName = @PlantName, Location = @Location, 
                                     ContactNumber = @ContactNumber, Email = @Email, 
                                     Password = @Password 
                                 WHERE PlantID = @PlantID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PlantID", plant.PlantID);
                    cmd.Parameters.AddWithValue("@PlantName", plant.PlantName);
                    cmd.Parameters.AddWithValue("@Location", plant.Location);
                    cmd.Parameters.AddWithValue("@ContactNumber", plant.ContactNumber);
                    cmd.Parameters.AddWithValue("@Email", plant.Email);
                    cmd.Parameters.AddWithValue("@Password", plant.Password);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public int DeletePlant(int PlantID)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = "DELETE FROM ManufacturingPlants WHERE PlantID = @PlantID";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@PlantID", PlantID);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ManufacturingPlant> GetAllPlants()
        {
            List<ManufacturingPlant> plantList = new List<ManufacturingPlant>();

            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = "SELECT * FROM ManufacturingPlants";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    ManufacturingPlant plant = new ManufacturingPlant
                    {
                        PlantID = Convert.ToInt32(row["PlantID"]),
                        PlantName = row["PlantName"].ToString(),
                        Location = row["Location"].ToString(),
                        ContactNumber = Convert.ToInt32(row["ContactNumber"]),
                        Email = row["Email"].ToString(),
                        Password = row["Password"].ToString()
                    };
                    plantList.Add(plant);
                }
            }
            return plantList;
        }

        public bool VerifyPlantLogin(string email, string password)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"SELECT COUNT(*) 
                                 FROM ManufacturingPlants 
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