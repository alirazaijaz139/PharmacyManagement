using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.forms_Folder;

namespace WindowsFormsApp1.forms_Folder
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            // pnlContent khali karo
            pnlContent.Controls.Clear();

            // CustomerControl banao
            CustomerControl customer = new CustomerControl();

            // Content panel ka size do
            customer.Dock = DockStyle.Fill;

            // Panel mein add karo
            pnlContent.Controls.Add(customer);
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
            

            
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            // Same ForgotPasswordForm kholo
            ForgotPasswordForm forgotForm = new ForgotPasswordForm();
            forgotForm.ShowDialog();
        }

        private void btnMedicines_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Medicines button clicked");

            pnlContent.Controls.Clear();


            MedicineControl mc = new MedicineControl();
            mc.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(mc);
            mc.LoadMedicinesPublic();
        }

        private void btnInvoices_Click(object sender, EventArgs e)
        {
            

            pnlContent.Controls.Clear();

            WindowsFormsApp1.forms_Folder.BillingForm bill =
                new WindowsFormsApp1.forms_Folder.BillingForm();

            

            bill.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(bill);
            bill.BringToFront();
            //frm.Show();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();

            StockControl sc = new StockControl();
            sc.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(sc);
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();

            PaymentControl pc = new PaymentControl();
            pc.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(pc);
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear(); // purana remove

            ReportsControl rc = new ReportsControl();
            rc.Dock = DockStyle.Fill;   // full screen

            pnlContent.Controls.Add(rc); // show
        }
    }
    }
    
    

