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
    public partial class SPC : Form
    {
        public SPC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SupplierFrom form = new SupplierFrom();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drugfrom form = new drugfrom();
            form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PharmacyRegistration form = new PharmacyRegistration();
            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ConfirmTender form = new ConfirmTender();
            form.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login form = new Login();
            form.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            WarehouseStaffRegistration form = new WarehouseStaffRegistration();
            form.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
           ManufacturingPlantRegistration form = new ManufacturingPlantRegistration();
            form.Show();
            this.Hide();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void SPC_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            SPCOrderForm  form = new SPCOrderForm();
            form.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SupplierView form = new SupplierView();
            form.Show();
            this.Hide();
        }
    }
}
