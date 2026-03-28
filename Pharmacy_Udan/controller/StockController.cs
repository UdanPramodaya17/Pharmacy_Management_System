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
    public class StockController
    {
        public int InsertNewsstock(Stock stock)
        {
            try
            {
                using (SqlConnection newCon = DataAccessLayer.CreateConnection())
                {
                    string insertStockQuery = @"
                INSERT INTO Stock (DrugName, Quantity, Price, Manufacturer, AddedBy, AddedDate) 
                VALUES (@DrugName, @Quantity, @Price, @Manufacturer, @AddedBy, @AddedDate)";  // Removed StockId

                    SqlCommand newcom = new SqlCommand(insertStockQuery, newCon);

                    // Correct parameters without StockId
                    newcom.Parameters.AddWithValue("@DrugName", stock.DrugName);
                    newcom.Parameters.AddWithValue("@Quantity", stock.Quantity);
                    newcom.Parameters.AddWithValue("@Price", stock.Price);
                    newcom.Parameters.AddWithValue("@Manufacturer", stock.Manufacturer);
                    newcom.Parameters.AddWithValue("@AddedBy", stock.AddedBy);
                    newcom.Parameters.AddWithValue("@AddedDate", stock.AddedDate);

                    newCon.Open();
                    return newcom.ExecuteNonQuery();  // Executes the insert
                }
            }
            catch (SqlException ex)
            {
                // Log or handle exception as needed
                Console.WriteLine("Error: " + ex.Message);
                return -1;  // Return a failure code
            }
        }

        // Update Stock
        public int UpdateStock(Stock stock)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = @"UPDATE Stock 
                                SET DrugName = @DrugName, Quantity = @Quantity, Price = @Price, 
                                    Manufacturer = @Manufacturer, AddedBy = @AddedBy, AddedDate = @AddedDate
                                WHERE StockId = @StockId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StockId", stock.StockId);
                    cmd.Parameters.AddWithValue("@DrugName", stock.DrugName);
                    cmd.Parameters.AddWithValue("@Quantity", stock.Quantity);
                    cmd.Parameters.AddWithValue("@Price", stock.Price);
                    cmd.Parameters.AddWithValue("@Manufacturer", stock.Manufacturer);
                    cmd.Parameters.AddWithValue("@AddedBy", stock.AddedBy);
                    cmd.Parameters.AddWithValue("@AddedDate", stock.AddedDate);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Delete Stock
        public int DeleteStock(int stockId)
        {
            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = "DELETE FROM Stock WHERE StockId = @StockId";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@StockId", stockId);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Get All Stock
        public List<Stock> GetAllStock()
        {
            List<Stock> stockList = new List<Stock>();

            using (SqlConnection con = DataAccessLayer.CreateConnection())
            {
                string query = "SELECT * FROM Stock";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Convert DataTable to List<Stock>
                foreach (DataRow row in dt.Rows)
                {
                    Stock stock = new Stock
                    {
                        StockId = Convert.ToInt32(row["StockId"]),
                        DrugName = row["DrugName"].ToString(),
                        Quantity = Convert.ToInt32(row["Quantity"]),
                        Price = Convert.ToDecimal(row["Price"]),
                        Manufacturer = row["Manufacturer"].ToString(),
                        AddedBy = row["AddedBy"].ToString(),
                        // Handle DBNull for AddedDate
                        AddedDate = row["AddedDate"] != DBNull.Value ? Convert.ToDateTime(row["AddedDate"]) : DateTime.MinValue
                    };
                    stockList.Add(stock);
                }
            }

            return stockList;
        }
    }
}