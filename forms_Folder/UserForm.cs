using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.forms_Folder
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            pnlUserContent.Controls.Clear();

            BillingForm bf = new BillingForm();
            bf.Dock = DockStyle.Fill;

            pnlUserContent.Controls.Add(bf);
        }

        private void btnMedicines_Click(object sender, EventArgs e)
        {
            pnlUserContent.Controls.Clear();

            MedicineControl mc = new MedicineControl();
            mc.Dock = DockStyle.Fill;

            //mc.SetUserMode();

            pnlUserContent.Controls.Add(mc);

            mc.LoadMedicinesPublic(); // 👈 important
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();

            this.Close();
        }
    }
}
