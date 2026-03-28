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
    public partial class WarehouseStaffRegistration : Form
    {
        ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();

        public WarehouseStaffRegistration()
        {
            InitializeComponent();
            BindData();
            dataGridView1.CellClick += dataGridView1_CellContentClick;

        }

        private void WarehouseStaffRegistration_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int result = soapClient.InsertNewStaffWeb(
                    textBox1.Text,  // Full Name
                    textBox2.Text,  // Position
                    int.Parse(textBox3.Text),  // Contact Number
                    textBox4.Text,  // Email
                    textBox5.Text   // Password
                );

                MessageBox.Show(result > 0 ? "Staff inserted successfully." : "Failed to insert staff.");
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
                    SqlCommand command = new SqlCommand("SELECT * FROM WarehouseStaff", con);
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
                int result = soapClient.UpdateStaffWeb(
                    int.Parse(textBox6.Text),  // Staff ID
                    textBox1.Text,  // Full Name
                    textBox2.Text,  // Position
                    int.Parse(textBox3.Text),  // Contact Number
                    textBox4.Text,  // Email
                    textBox5.Text   // Password
                );

                MessageBox.Show(result > 0 ? "Staff updated successfully." : "Failed to update staff.");
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
                int staffID = int.Parse(textBox6.Text);
                int result = soapClient.DeleteStaffWeb(staffID);

                MessageBox.Show(result > 0 ? "Staff deleted successfully." : "Failed to delete staff.");
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

                    string query = @"SELECT * FROM WarehouseStaff 
                             WHERE (@SearchText = '' OR FullName LIKE @SearchText) 
                             AND (@StaffID = 0 OR StaffID = @StaffID)";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@SearchText", "%" + textBox7.Text.Trim() + "%");

                        int staffId = 0;
                        if (!string.IsNullOrWhiteSpace(textBox6.Text) && int.TryParse(textBox6.Text, out staffId))
                        {
                            command.Parameters.AddWithValue("@StaffID", staffId);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@StaffID", 0);
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

                textBox6.Text = row.Cells["StaffID"].Value.ToString();
                textBox1.Text = row.Cells["FullName"].Value.ToString();
                textBox2.Text = row.Cells["Position"].Value.ToString();
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            SPC form = new SPC();
            form.Show();
            this.Hide();
        }
    }
}
