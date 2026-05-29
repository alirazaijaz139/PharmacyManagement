using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Database;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1.forms_Folder
{
    public partial class PaymentControl : UserControl
    {
        private PaymentRepository _repo = new PaymentRepository();
        public PaymentControl()
        {
            InitializeComponent();
            cmbStatus.Items.Add("Paid");
            cmbStatus.Items.Add("Unpaid");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadSales();
            LoadPayments();
        }
        private void LoadSales()
        {
            DataTable dt = _repo.GetSalesWithCustomers();
            cmbSale.DataSource = dt;
            cmbSale.DisplayMember = "name";
            cmbSale.ValueMember = "id";
        }
        
        private void LoadPayments()
        {
            dgvPayments.Rows.Clear();
            DataTable dt = _repo.GetAllPayments();
            foreach (DataRow row in dt.Rows)
            {
                dgvPayments.Rows.Add(
                    row["id"],
                    row["name"],
                    row["amount"],
                    row["status"],
                    row["date"]
                );
            }



        }
            
        

        private void btnSavePayment_Click(object sender, EventArgs e)
        {
            if (txtAmount.Text == "" || cmbStatus.Text == "")
            {
                MessageBox.Show("Fill all fields");
                return;
            }

            try
            {
                int saleId = Convert.ToInt32(cmbSale.SelectedValue);

                if (_repo.PaymentExists(saleId))
                {
                    MessageBox.Show("Payment already done!");
                    return;
                }

                _repo.AddPayment(saleId, Convert.ToDecimal(txtAmount.Text), cmbStatus.Text);
                MessageBox.Show("Payment Saved!");
                LoadPayments();
                txtAmount.Clear();
                cmbStatus.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void cmbSale_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cmbSale.SelectedValue == null) return;

      
            if (cmbSale.SelectedValue is DataRowView) return;

            int saleId = Convert.ToInt32(cmbSale.SelectedValue.ToString());
            decimal total = _repo.GetSaleTotal(saleId);
            if (total > 0)
                txtAmount.Text = total.ToString();
        }
            }
}
   


   