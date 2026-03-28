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
    public partial class SupplierFrom : Form

    {
        ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();


        public SupplierFrom()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void SupplierFrom_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)

        {
            try
            {
                int Id = int.Parse(textBox10.Text);
                string name = textBox1.Text;
                string addres = textBox2.Text;
                string email = textBox3.Text;
                int contact_number = int.Parse(textBox4.Text);
                string date = textBox5.Text;

                var result = soapClient.InsertsupplierWeb (Id, name, addres, email, contact_number, date);
                if (result == 1)
                {
                    MessageBox.Show("New Supplier is Added");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.ToString());
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = int.Parse(textBox10.Text);
                string name = textBox1.Text;
                string addres = textBox2.Text;
                string email = textBox3.Text;
                int contact_number = int.Parse(textBox4.Text);
                string date = textBox5.Text;

                var result = soapClient.UpdateSupplierWeb(Id, name, addres, email, contact_number, date);
                if (result == 1)
                {
                    MessageBox.Show("Supplier information updated successfully.");
                }
                else
                {
                    MessageBox.Show("Supplier update failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }


        }
















        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox0_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = int.Parse(textBox10.Text);
                var result = soapClient.DeleteSupplierWeb (Id);
                if (result == 1)
                {
                    MessageBox.Show("Supplier deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Supplier deletion failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int Id = int.Parse(textBox10.Text);
                var supplier = soapClient.SearchSupplierWeb(Id);
                if (supplier != null)
                {
                    textBox1.Text = supplier.name;
                    textBox2.Text = supplier.address;
                    textBox3.Text = supplier.email;
                    textBox4.Text = supplier.contact_number.ToString();
                    textBox5.Text = supplier.Password;
                }
                else
                {
                    MessageBox.Show("Supplier not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SPC form = new SPC();
            form.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
