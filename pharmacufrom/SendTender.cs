using pharmacufrom.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pharmacufrom
{
    public partial class SendTender : Form
    {
        ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();
        public SendTender()
        {
            InitializeComponent();
            LoadTenders();
        }

        private void LoadTenders()
        {
            Tender[] tenders = soapClient.GetTenders();
            dataGridView1.DataSource = tenders.ToList();
        }

        private void SendTender_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int supplierId = int.Parse(txtSupplierId.Text);
                string tenderDrugName = txtTenderDrugName.Text;  // Get drug name from UI
                decimal tenderAmount = decimal.Parse(txtTenderAmount.Text);
                int tenderQuantity = int.Parse(txtTenderQuantity.Text);
                string tenderDate = DateTime.Now.ToString("yyyy-MM-dd");

                int result = soapClient.InsertTender(supplierId, tenderDrugName, tenderAmount, tenderQuantity, tenderDate);

                MessageBox.Show(result > 0 ? "Tender submitted successfully." : "Tender submission failed.");
                LoadTenders();  // Refresh tender list
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SupplierMain form = new SupplierMain();
            form.Show();
            this.Hide();
        }

        private void txtSupplierId_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
