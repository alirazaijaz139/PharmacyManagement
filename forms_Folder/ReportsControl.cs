using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WindowsFormsApp1.Database;

namespace WindowsFormsApp1.forms_Folder
{
    public partial class ReportsControl : UserControl
    {
        private ReportsRepository _repo = new ReportsRepository();
        public ReportsControl()
        {
            InitializeComponent();
        }
       

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadTotalSales();
            LoadTotalPayments();
            LoadTodaySales();
            LoadTopMedicine();
        }

        private void LoadTotalSales()
        {
            lblTotalSales.Text = _repo.GetTotalSales().ToString();
        }

        private void LoadTotalPayments()
        {
            lblTotalPayments.Text = _repo.GetTotalPayments().ToString();
        }

        private void LoadTodaySales()
        {
            lblTodaySales.Text = _repo.GetTodaySales().ToString();
        }

        private void LoadTopMedicine()
        {
            lblTopMedicine.Text = _repo.GetTopMedicine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            lblTotalSales.Text = "0.00";
            lblTotalPayments.Text = "0.00";
            lblTodaySales.Text = "0.00";

        }

        


    }
    }

