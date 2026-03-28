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
    public class TenderController
    {
        private string connectionString = "Server=LAPTOP-O6BJ19N8\\SQLEXPRESS02;Database=spcdb;Trusted_Connection=True;";
        public int InsertTender(Tender tender)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"
            INSERT INTO Tender (SupplierId, TenderDrugName, TenderAmount, TenderQuantity, TenderDate, TenderStatus) 
            VALUES (@SupplierId, @TenderDrugName, @TenderAmount, @TenderQuantity, @TenderDate, @TenderStatus)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@SupplierId", tender.SupplierId);
                    cmd.Parameters.AddWithValue("@TenderDrugName", tender.TenderDrugName); // <-- New parameter
                    cmd.Parameters.AddWithValue("@TenderAmount", tender.TenderAmount);
                    cmd.Parameters.AddWithValue("@TenderQuantity", tender.TenderQuantity);
                    cmd.Parameters.AddWithValue("@TenderDate", tender.TenderDate);
                    cmd.Parameters.AddWithValue("@TenderStatus", tender.TenderStatus);

                    con.Open();
                    return cmd.ExecuteNonQuery();  // Returns number of affected rows
                }
            }
        }

        // Get All Tenders
        public List<Tender> GetAllTenders()
        {
            List<Tender> tenderList = new List<Tender>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Tender";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    Tender tender = new Tender
                    {
                        TenderId = Convert.ToInt32(row["TenderId"]),
                        SupplierId = Convert.ToInt32(row["SupplierId"]),
                        TenderDrugName = row["TenderDrugName"].ToString(), 
                        TenderAmount = Convert.ToDecimal(row["TenderAmount"]),
                        TenderQuantity = Convert.ToInt32(row["TenderQuantity"]),
                        TenderDate = Convert.ToDateTime(row["TenderDate"]),
                        TenderStatus = row["TenderStatus"].ToString()
                    };
                    tenderList.Add(tender);
                }
            }

            return tenderList;
        }

        // Confirm Tender (Update Status)
        public int ConfirmTender(int tenderId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Tender SET TenderStatus = 'Confirmed' WHERE TenderId = @TenderId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TenderId", tenderId);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

    }
}