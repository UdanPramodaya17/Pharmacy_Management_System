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
    public partial class PharmacyRegistration : Form
    {
        ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();
        public PharmacyRegistration()
        {
            InitializeComponent();
            BindData();
            dataGridView1.CellClick += dataGridView1_CellContentClick;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int result = soapClient.InsertNewPharmacyWeb(
                    textBox1.Text,
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text,
                    textBox6.Text
                );

                MessageBox.Show(result > 0 ? "Pharmacy inserted successfully." : "Failed to insert pharmacy.");
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
                    SqlCommand command = new SqlCommand("SELECT * FROM PharmacyRegistration", con);
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

        private void PharmacyRegistration_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int result = soapClient.UpdatePharmacyWeb(
                    int.Parse(textBox8.Text),
                    textBox1.Text,
                    textBox2.Text,
                    textBox3.Text,
                    textBox4.Text,
                    textBox5.Text,
                    textBox6.Text
                );

                MessageBox.Show(result > 0 ? "Pharmacy updated successfully." : "Failed to update pharmacy.");
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
                int PharmacyID = int.Parse(textBox8.Text);
                int result = soapClient.DeletePharmacyWeb(PharmacyID);

                MessageBox.Show(result > 0 ? "Pharmacy deleted successfully." : "Failed to delete pharmacy.");
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

                    string query = @"SELECT * FROM PharmacyRegistration 
                             WHERE (@SearchText = '' OR PharmacyName LIKE @SearchText) 
                             AND (@PharmacyID = 0 OR PharmacyID = @PharmacyID)";

                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@SearchText", "%" + textBox7.Text.Trim() + "%");

                        int pharmacyId = 0;
                        if (!string.IsNullOrWhiteSpace(textBox8.Text) && int.TryParse(textBox8.Text, out pharmacyId))
                        {
                            command.Parameters.AddWithValue("@PharmacyID", pharmacyId);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@PharmacyID", 0);
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

                textBox8.Text = row.Cells["PharmacyID"].Value.ToString();
                textBox1.Text = row.Cells["PharmacyName"].Value.ToString();
                textBox2.Text = row.Cells["Address"].Value.ToString();
                textBox3.Text = row.Cells["PhoneNumber"].Value.ToString();
                textBox4.Text = row.Cells["Username"].Value.ToString();
                textBox5.Text = row.Cells["Password"].Value.ToString();
                textBox6.Text = row.Cells["VerifyPassword"].Value.ToString();
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
            textBox8.Clear();

            dataGridView1.ClearSelection();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
           SPC form = new SPC();
            form.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
