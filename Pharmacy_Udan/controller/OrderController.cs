using Pharmacy_Udan.data;
using Pharmacy_Udan.module;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Pharmacy_Udan.controller
{
    public class OrderController
    {
        private string connectionString = "Server=LAPTOP-O6BJ19N8\\SQLEXPRESS02;Database=spcdb;Trusted_Connection=True;";
        public int AddOrder(Order order)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Orders (StockId, DrugName, UnitPrice, Quantity, TotalPrice)
                                 VALUES (@StockId, @DrugName, @UnitPrice, @Quantity, @TotalPrice)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StockId", order.StockId);
                    cmd.Parameters.AddWithValue("@DrugName", order.DrugName);
                    cmd.Parameters.AddWithValue("@UnitPrice", order.UnitPrice);
                    cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                    cmd.Parameters.AddWithValue("@TotalPrice", order.TotalPrice);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return -1;
            }
        }

        public decimal GetUnitPrice(int stockId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Price FROM Stock WHERE StockId = @StockId ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StockId", stockId);               
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToDecimal(result) : 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching price: " + ex.Message);
                return 0;
            }
        }

        public string GetDrugName(int stockId)
        {
            string drugName = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT DrugName FROM Stock WHERE StockId = @StockId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@StockId", stockId);

                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    drugName = result.ToString();
                }
            }
            return drugName;
        }

        public int DeleteOrder(int orderId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Orders WHERE OrderId = @OrderId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT OrderId, StockId, DrugName, UnitPrice, Quantity, TotalPrice, OrderDate FROM Orders";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            OrderId = reader["OrderId"] != DBNull.Value ? Convert.ToInt32(reader["OrderId"]) : 0,
                            StockId = reader["StockId"] != DBNull.Value ? Convert.ToInt32(reader["StockId"]) : 0,
                            DrugName = reader["DrugName"] != DBNull.Value ? reader["DrugName"].ToString() : string.Empty,
                            UnitPrice = reader["UnitPrice"] != DBNull.Value ? Convert.ToDecimal(reader["UnitPrice"]) : 0,
                            Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0,
                            TotalPrice = reader["TotalPrice"] != DBNull.Value ? Convert.ToDecimal(reader["TotalPrice"]) : 0,
                            OrderDate = reader["OrderDate"] != DBNull.Value ? Convert.ToDateTime(reader["OrderDate"]) : DateTime.MinValue
                        };

                        orders.Add(order);
                    }
                }
            }
            return orders;
        }




    }
}