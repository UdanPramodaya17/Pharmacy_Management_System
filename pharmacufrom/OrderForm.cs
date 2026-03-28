using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacufrom
{

    public partial class OrderForm : Form
    {

        ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();
        private string connectionString = "Server=LAPTOP-O6BJ19N8\\SQLEXPRESS02;Database=spcdb;Trusted_Connection=True;";

        public OrderForm()
        {
            InitializeComponent();
        }


        private void LoadOrders()
        {
            try
            {
                // Load orders via SOAP client
                var orders = soapClient.GetAllOrders().ToList();
                dataGridViewOrders.DataSource = orders; // Display orders in DataGridViewOrders
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load orders: " + ex.Message);
            }

            try
            {
                // Load stock data from SQL database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Stock"; // Change table name if needed

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewStock.DataSource = dataTable; // Display stock data in DataGridViewStock
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stock data: " + ex.Message);
            }





        }


        private void OrderForm_Load(object sender, EventArgs e)
        {
            LoadOrders();

        }

        private void txtStockId_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtStockId.Text, out int stockId))
            {
                // Fetch Drug Name
                string drugName = soapClient.GetDrugNameByStockId(stockId);
                txtDrugName.Text = drugName;

                // Fetch Price
                decimal price = soapClient.GetUnitPriceByStockId(stockId);
                txtUnitPrice.Text = price.ToString();

                CalculateTotalPrice();
            }
            else
            {
                txtDrugName.Clear();
                txtUnitPrice.Clear();
                txtTotalPrice.Clear();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void CalculateTotalPrice()
        {
            if (decimal.TryParse(txtUnitPrice.Text, out decimal unitPrice) &&
                int.TryParse(txtQuantity.Text, out int quantity))
            {
                txtTotalPrice.Text = (unitPrice * quantity).ToString();
            }
            else
            {
                txtTotalPrice.Text = "0";
            }
        }











        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int stockId = int.Parse(txtStockId.Text);
                string drugName = txtDrugName.Text;
                decimal unitPrice = decimal.Parse(txtUnitPrice.Text);
                int quantity = int.Parse(txtQuantity.Text);
                decimal totalPrice = decimal.Parse(txtTotalPrice.Text);

                // Check stock availability before adding the order
                if (!IsStockAvailable(stockId, quantity))
                {
                    MessageBox.Show("Not enough stock available for this order!");
                    return;
                }

                // Proceed to add order
                int result = soapClient.AddOrder(stockId, drugName, unitPrice, quantity, totalPrice);
                if (result > 0)
                {
                    // Update stock after order is confirmed
                    UpdateStockQuantity(stockId, quantity);
                    MessageBox.Show("Order Confirmed Successfully");
                    LoadOrders(); // Refresh order & stock data
                }
                else
                {
                    MessageBox.Show("Failed to Confirm Order");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private bool IsStockAvailable(int stockId, int orderQuantity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Quantity FROM Stock WHERE StockId = @StockId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StockId", stockId);

                    int availableStock = Convert.ToInt32(cmd.ExecuteScalar());
                    return orderQuantity <= availableStock;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking stock: " + ex.Message);
                return false;
            }
        }


        // Method to update stock quantity after an order is confirmed
        private void UpdateStockQuantity(int stockId, int orderQuantity)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Stock SET Quantity = Quantity - @OrderQuantity WHERE StockId = @StockId";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderQuantity", orderQuantity);
                    cmd.Parameters.AddWithValue("@StockId", stockId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating stock: " + ex.Message);
            }
        }



















        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = int.Parse(txtOrderId.Text);
                int result = soapClient.DeleteOrder(orderId);

                if (result > 0)
                {
                    MessageBox.Show("Order Deleted Successfully");
                    LoadOrders();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Failed to Delete Order");
                    LoadOrders();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewOrders.Rows[e.RowIndex];
                txtOrderId.Text = row.Cells["OrderId"].Value.ToString();
                txtStockId.Text = row.Cells["StockId"].Value.ToString();
                txtDrugName.Text = row.Cells["DrugName"].Value.ToString();
                txtUnitPrice.Text = row.Cells["UnitPrice"].Value.ToString();
                txtQuantity.Text = row.Cells["Quantity"].Value.ToString();
                txtTotalPrice.Text = row.Cells["TotalPrice"].Value.ToString();
            }
        }
        private void ClearInputs()
        {
            txtOrderId.Clear();
            txtStockId.Clear();
            txtDrugName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            txtTotalPrice.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtOrderId.Clear();
            txtStockId.Clear();
            txtDrugName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            txtTotalPrice.Clear();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            pharmacy form = new pharmacy();
            form.Show();
            this.Hide();
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }

    }

