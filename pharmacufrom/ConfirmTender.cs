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
    public partial class ConfirmTender : Form
    {
        ServiceReference1.WebService1SoapClient soapClient = new ServiceReference1.WebService1SoapClient();

        public ConfirmTender()
        {
            InitializeComponent();
            LoadTenders();
        }

        private void LoadTenders()
        {
            Tender[] tenders = soapClient.GetTenders();
            dataGridView1.DataSource = tenders.ToList();
        }


        private void ConfirmTender_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int tenderId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TenderId"].Value);

                int result = soapClient.ConfirmTender(tenderId);

                MessageBox.Show(result > 0 ? "Tender confirmed successfully." : "Tender confirmation failed.");
                LoadTenders();
            }
            else
            {
                MessageBox.Show("Please select a tender to confirm.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SPC form = new SPC ();
            form.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
    }

