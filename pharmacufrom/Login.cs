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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Initialize the web service client
                ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();

                // Get input values
                string supplierName = textBoxSupplierName.Text;
                string Supplierpass = textBoxSupplierpass.Text;

                // Call the web service method
                bool isLoginSuccessful = soapClient.SupplierLoginWeb(supplierName, Supplierpass);

                if (isLoginSuccessful)
                {
                    MessageBox.Show("Login successful!");

                    // Navigate to SupplierForm
                    SupplierMain form = new SupplierMain();
                    form.Show();
                    this.Hide();
                    // Optionally hide or close the current form
                    this.Hide();  // Hides the login form
                }
                else
                {
                    MessageBox.Show("Login failed! Supplier name or password date is incorrect.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
        }

        private void textBoxSupplierpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Hardcoded username and password
            string correctUsername = "SPC";
            string correctPassword = "1717spc";

            // Get values entered by the user
            string enteredUsername = textBoxUsername.Text;
            string enteredPassword = textBoxPassword.Text;

            // Check if the entered username and password match the hardcoded ones
            if (enteredUsername == correctUsername && enteredPassword == correctPassword)
            {
                MessageBox.Show("Login successful!");
                // Proceed to next form or show main application content
                // For example: this.Hide(); new MainForm().Show();
                SPC form = new SPC();
                form.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();

                string username = textBoxPharmacyName.Text.Trim();
                string password = textBoxPharmacyPass.Text.Trim();

                bool isLoginSuccessful = soapClient.PharmacyLoginWeb(username, password);

                if (isLoginSuccessful)
                {
                    MessageBox.Show("Login successful!");
                    pharmacy form = new pharmacy();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login failed! Incorrect pharmacy name or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxSupplierName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBoxSupplierName.Clear();
            textBoxSupplierpass.Clear();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();

                string email = textBoxPlantName.Text.Trim();
                string password = textBoxPlantPass.Text.Trim();

                bool isLoginSuccessful = soapClient.PlantLoginWeb(email, password);

                if (isLoginSuccessful)
                {
                    MessageBox.Show("Login successful!");
                    ManufacturingPlant form = new ManufacturingPlant();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login failed! Incorrect ManufacturingPlant name or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();

                string email = textBoxStaffName.Text.Trim();
                string password = textBoxStaffPass.Text.Trim();

                bool isLoginSuccessful = soapClient.WarehouseStaffLoginWeb(email, password);

                if (isLoginSuccessful)
                {
                    MessageBox.Show("Login successful!");
                    WarehouseStaff form = new WarehouseStaff();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login failed! Incorrect WarehouseStaffLogin name or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            textBoxUsername.Clear();
            textBoxPassword.Clear();
        }

        private void textBoxUsername_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBoxPharmacyName.Clear();
            textBoxPharmacyPass.Clear();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBoxPlantName.Clear();
            textBoxPlantPass.Clear();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBoxStaffName.Clear();
            textBoxStaffPass.Clear();

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }
    }
}
