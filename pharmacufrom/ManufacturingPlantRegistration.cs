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
    public partial class ManufacturingPlantRegistration : Form
    {

        ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();
        public ManufacturingPlantRegistration()
        {
            InitializeComponent();
            BindData();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ManufacturingPlantRegistration_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int result = soapClient.InsertNewPlantWeb(
                    textBox1.Text, // PlantName
                    textBox2.Text, // Location
                    int.Parse(textBox3.Text), // ContactNumber
                    textBox4.Text, // Email
                    textBox5.Text  // Password
                );

                MessageBox.Show(result > 0 ? "Manufacturing Plant inserted successfully." : "Failed to insert.");
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BindData()
        {
            using (SqlConnection con = new SqlConnection("Server=LAPTOP-O6BJ19N8\\SQLEXPRESS02;Database=spcdb;Trusted_Connection=True;"))
            {
                try
                {
                    con.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM ManufacturingPlants", con);
                    SqlDataAdapter sd = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int result = soapClient.UpdatePlantWeb(
                    int.Parse(textBox6.Text), // PlantID
                    textBox1.Text, // PlantName
                    textBox2.Text, // Location
                    int.Parse(textBox3.Text), // ContactNumber
                    textBox4.Text, // Email
                    textBox5.Text  // Password
                );

                MessageBox.Show(result > 0 ? "Manufacturing Plant updated successfully." : "Failed to update.");
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int plantID = int.Parse(textBox6.Text);
                int result = soapClient.DeletePlantWeb(plantID);

                MessageBox.Show(result > 0 ? "Manufacturing Plant deleted successfully." : "Failed to delete.");
                BindData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Server=LAPTOP-O6BJ19N8\\SQLEXPRESS02;Database=spcdb;Trusted_Connection=True;"))
                {
                    con.Open();

                    string query = @"SELECT * FROM ManufacturingPlants 
                             WHERE (@SearchText = '' OR PlantName LIKE @SearchText) 
                             AND (@PlantID = 0 OR PlantID = @PlantID)";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@SearchText", "%" + textBox7.Text.Trim() + "%");

                        int plantId = 0;
                        if (!string.IsNullOrWhiteSpace(textBox6.Text) && int.TryParse(textBox6.Text, out plantId))
                        {
                            command.Parameters.AddWithValue("@PlantID", plantId);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@PlantID", 0);
                        }

                        SqlDataAdapter sd = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
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
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox6.Text = row.Cells["PlantID"].Value.ToString();
                textBox1.Text = row.Cells["PlantName"].Value.ToString();
                textBox2.Text = row.Cells["Location"].Value.ToString();
                textBox3.Text = row.Cells["ContactNumber"].Value.ToString();
                textBox4.Text = row.Cells["Email"].Value.ToString();
                textBox5.Text = row.Cells["Password"].Value.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            dataGridView1.ClearSelection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SPC form = new SPC();
            form.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
