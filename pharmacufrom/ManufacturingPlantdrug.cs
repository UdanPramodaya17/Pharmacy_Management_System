using pharmacufrom.ServiceReference1;
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
    public partial class ManufacturingPlantdrug : Form
    {
        ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();
        public ManufacturingPlantdrug()
        {
            InitializeComponent();
            BindData();  // Load data into DataGridView
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void ManufacturingPlantdrug_Load(object sender, EventArgs e)
        {

        }

        private void buttonAddStock_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if any required fields are empty
                if (string.IsNullOrWhiteSpace(textBoxDrugName.Text) ||
                    string.IsNullOrWhiteSpace(textBoxManufacturer.Text) ||
                    string.IsNullOrWhiteSpace(textBoxAddedBy.Text) ||
                    //string.IsNullOrWhiteSpace(textBoxQuantity.Text) ||
                    string.IsNullOrWhiteSpace(textBoxPrice.Text))
                {
                    MessageBox.Show("All fields are required.");
                    //return;
                }

                // Validate numeric fields
                if (!int.TryParse(textBoxQuantity.Text, out int quantity) ||
                    !decimal.TryParse(textBoxPrice.Text, out decimal price))
                {
                    MessageBox.Show("Please enter valid numeric values for quantity and price.");
                    return;
                }

                // Call the InsertNewsstock WebMethod to insert stock into the database
                int result = soapClient.InsertNewsstock(
                    textBoxDrugName.Text,    // DrugName
                    quantity,                // Quantity
                    price,                   // Price
                    textBoxManufacturer.Text, // Manufacturer
                    textBoxAddedBy.Text      // AddedBy
                );

                // Check the result to determine success or failure
                if (result > 0)
                {
                    MessageBox.Show("Stock added successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to add stock.");
                }

                // Re-bind data to DataGridView to refresh the list of stocks
                BindData();
            }
            catch (Exception ex)
            {
                // Log the error and show a message
                Console.WriteLine("Error adding stock: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BindData()
        {
            try
            {
                // Get the stock data (assuming GetStockData returns an array of Stock objects)
                Stock[] stockData = soapClient.GetStockData();

                // Convert Stock array to DataTable
                DataTable dt = new DataTable();
                dt.Columns.Add("StockId", typeof(int));
                dt.Columns.Add("DrugName", typeof(string));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("Manufacturer", typeof(string));
                dt.Columns.Add("AddedBy", typeof(string));
                dt.Columns.Add("AddedDate", typeof(DateTime));

                // Populate the DataTable with data from the Stock array
                foreach (var stock in stockData)
                {
                    dt.Rows.Add(stock.StockId, stock.DrugName, stock.Quantity, stock.Price, stock.Manufacturer, stock.AddedBy, stock.AddedDate);
                }

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                int result = soapClient.UpdateStockWeb(
                    int.Parse(stockid.Text),
                    textBoxDrugName.Text,
                    int.Parse(textBoxQuantity.Text),
                    decimal.Parse(textBoxPrice.Text),
                    textBoxManufacturer.Text,
                    textBoxAddedBy.Text
                );

                MessageBox.Show(result > 0 ? "Stock updated successfully." : "Failed to update stock.");
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)  // Ensure a valid row is clicked
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Populate textboxes with row data
                stockid.Text = row.Cells["StockId"].Value.ToString();
                textBoxDrugName.Text = row.Cells["DrugName"].Value.ToString();
                textBoxQuantity.Text = row.Cells["Quantity"].Value.ToString();
                textBoxPrice.Text = row.Cells["Price"].Value.ToString();
                textBoxManufacturer.Text = row.Cells["Manufacturer"].Value.ToString();
                textBoxAddedBy.Text = row.Cells["AddedBy"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Server=LAPTOP-O6BJ19N8\\SQLEXPRESS02;Database=spcdb;Trusted_Connection=True;"))
                {
                    con.Open();

                    // Search query with LIKE for partial matching and exact match for StockId
                    string query = "SELECT * FROM Stock WHERE (DrugName LIKE @SearchText OR @SearchText = '') AND (StockId = @StockId OR @StockId = 0)";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        // Parameters for searching DrugName or StockId
                        command.Parameters.AddWithValue("@SearchText", "%" + textBoxDrugName1.Text + "%");
                        command.Parameters.AddWithValue("@StockId", string.IsNullOrWhiteSpace(stockid.Text) ? 0 : int.Parse(stockid.Text));  // If StockId is empty, use 0

                        SqlDataAdapter sd = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);

                        dataGridView1.DataSource = dt;  // Display the search results in the DataGridView
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            stockid.Clear();
            textBoxDrugName.Clear();
            textBoxQuantity.Clear();
            textBoxPrice.Clear();
            textBoxManufacturer.Clear();

            // Reset ComboBox
            textBoxAddedBy.SelectedIndex = -1; // Clears selection
            textBoxAddedBy.Text = ""; // Clears text (if editable)


            // Optional: Reset DataGridView selection
            dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManufacturingPlant form = new ManufacturingPlant();
            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void stockid_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }


}
