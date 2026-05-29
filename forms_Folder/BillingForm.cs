
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
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.forms_Folder
{
   
    public partial class BillingForm : UserControl
    {
        private CustomerRepository _customerRepo = new CustomerRepository();
        private MedicineRepository _medicineRepo = new MedicineRepository();
        string currentCustomer = "";
        private readonly BillingService _billingService = new BillingService();

        public BillingForm()
        {
            InitializeComponent();
            
        }

        private void BillingForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadMedicines();
        }
        private void LoadCustomers()
        {
            DataTable dt = _customerRepo.GetAll();
            cmbCustomer.DataSource = dt;
            cmbCustomer.DisplayMember = "name";
            cmbCustomer.ValueMember = "id";
        }
        private void LoadMedicines()
        {
            DataTable dt = _medicineRepo.GetAll();
            cmbMedicine.DataSource = dt;
            cmbMedicine.DisplayMember = "name";
            cmbMedicine.ValueMember = "id";
        }
        

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            // first customer set
            if (currentCustomer == "")
            {
                currentCustomer = cmbCustomer.Text;
            }

            // different customer check
            if (cmbCustomer.Text != currentCustomer)
            {
                MessageBox.Show(
                    "First current bill save/clear karo!",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            
            if (txtQty.Text == "")
            {
                MessageBox.Show("Enter Quantity");
                return;
            }

            int qty = Convert.ToInt32(txtQty.Text);

            DataRowView medicineRow = (DataRowView)cmbMedicine.SelectedItem;

            // MYSQL SE ID
            int id = Convert.ToInt32(medicineRow["id"]);

            string name = medicineRow["name"].ToString();

            decimal price = Convert.ToDecimal(medicineRow["price"]);
            int stock = Convert.ToInt32(medicineRow["qty"]);

            if (qty > stock)
            {
                MessageBox.Show("Insufficient Stock");
                return;
            }

            decimal subtotal = price * qty;

            // ADD ROW
            dgvBill.Rows.Add(id, name, price, qty, subtotal);

            CalculateTotal();
            // int sr = dgvBill.Rows.Count + 1;


          
        }
        private void CalculateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvBill.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Subtotal"].Value);
            }

            lblTotal.Text = total.ToString(); // 👈 yahan set hota hai
        }

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            try
            {
                int customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                decimal total = Convert.ToDecimal(lblTotal.Text);

                var items = new List<SaleItem>();
                foreach (DataGridViewRow row in dgvBill.Rows)
                {
                    items.Add(new SaleItem
                    {
                        MedicineId = Convert.ToInt32(row.Cells[0].Value),
                        Price = Convert.ToDecimal(row.Cells[2].Value),
                        Qty = Convert.ToInt32(row.Cells[3].Value),
                        Subtotal = Convert.ToDecimal(row.Cells[4].Value)
                    });
                }

                string error = _billingService.SaveBill(customerId, total, items);
                if (error != null)
                {
                    MessageBox.Show(error);
                    return;
                }

                MessageBox.Show("Bill Saved Successfully!");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


    

        private void ClearBill()
        {
            dgvBill.Rows.Clear();
            lblTotal.Text = "0";

            currentCustomer = "";

            cmbCustomer.SelectedIndex = -1;
            cmbMedicine.SelectedIndex = -1;

            txtQty.Clear();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Font titleFont = new Font("Arial", 24, FontStyle.Bold);
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 11);
            Font totalFont = new Font("Arial", 14, FontStyle.Bold);
            Font footerFont = new Font("Arial", 10, FontStyle.Italic);

            int startX = 40;
            int startY = 40;
            int y = startY;

            // ===== TITLE =====
            e.Graphics.DrawString(
                "PHARMACY MANAGEMENT SYSTEM",
                titleFont,
                Brushes.DarkBlue,
                startX + 120,
                y);

            y += 50;

            // ===== BILL TITLE =====
            e.Graphics.DrawString(
                "CUSTOMER BILL",
                new Font("Arial", 16, FontStyle.Bold),
                Brushes.Black,
                startX + 250,
                y);

            y += 40;

            // ===== DATE =====
            e.Graphics.DrawString(
                "Date: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm tt"),
                bodyFont,
                Brushes.Black,
                startX,
                y);

            y += 30;
           

            e.Graphics.DrawString(
                "Customer Name : " + cmbCustomer.Text,
                bodyFont,
                Brushes.Black,
                startX,
                y);
            y += 25;

            // SHOP NAME
            string shopName = "";

            if (cmbCustomer.SelectedItem != null)
            {
                DataRowView customerRow = (DataRowView)cmbCustomer.SelectedItem;
                shopName = customerRow["shop_name"].ToString();
            }
            

            e.Graphics.DrawString(
                "Shop Name : " + shopName,
                bodyFont,
                Brushes.Black,
                startX,
                y);

            y += 35;

            // ===== LINE =====
            e.Graphics.DrawLine(Pens.Black, startX, y, 760, y);

            y += 20;

            // ===== TABLE HEADER =====
            e.Graphics.FillRectangle(Brushes.LightGray, startX, y, 720, 35);

            e.Graphics.DrawRectangle(Pens.Black, startX, y, 720, 35);

            e.Graphics.DrawString("ID", headerFont, Brushes.Black, startX + 10, y + 8);

            e.Graphics.DrawString("Medicine Name", headerFont, Brushes.Black, startX + 80, y + 8);

            e.Graphics.DrawString("Price", headerFont, Brushes.Black, startX + 350, y + 8);

            e.Graphics.DrawString("Qty", headerFont, Brushes.Black, startX + 480, y + 8);

            e.Graphics.DrawString("Subtotal", headerFont, Brushes.Black, startX + 590, y + 8);

            y += 35;

            // ===== TABLE ROWS =====
            foreach (DataGridViewRow row in dgvBill.Rows)
            {
                if (row.Cells["ID"].Value != null)
                {
                    e.Graphics.DrawRectangle(Pens.Black, startX, y, 720, 30);

                    // ID
                    e.Graphics.DrawString(
                        row.Cells["ID"].Value.ToString(),
                        bodyFont,
                        Brushes.Black,
                        startX + 10,
                        y + 5);

                    // Medicine Name
                    e.Graphics.DrawString(
                        row.Cells["MedicineName"].Value.ToString(),
                        bodyFont,
                        Brushes.Black,
                        startX + 80,
                        y + 5);

                    // Price
                    e.Graphics.DrawString(
                        row.Cells["Price"].Value.ToString(),
                        bodyFont,
                        Brushes.Black,
                        startX + 350,
                        y + 5);

                    // Qty
                    e.Graphics.DrawString(
                        row.Cells["QTY"].Value.ToString(),
                        bodyFont,
                        Brushes.Black,
                        startX + 480,
                        y + 5);

                    // Subtotal
                    e.Graphics.DrawString(
                        row.Cells["Subtotal"].Value.ToString(),
                        bodyFont,
                        Brushes.Black,
                        startX + 590,
                        y + 5);

                    y += 30;
                }
            }

            y += 30;

            // ===== TOTAL =====
            e.Graphics.FillRectangle(Brushes.LightYellow, startX + 450, y, 270, 40);

            e.Graphics.DrawRectangle(Pens.Black, startX + 450, y, 270, 40);

            e.Graphics.DrawString(
                "TOTAL : Rs. " + lblTotal.Text,
                totalFont,
                Brushes.DarkRed,
                startX + 470,
                y + 8);

            y += 70;

            // ===== FOOTER LINE =====
            e.Graphics.DrawLine(Pens.Gray, startX, y, 760, y);

            y += 20;

            // ===== THANK YOU =====
            e.Graphics.DrawString(
                "Thank You For Visiting Our Pharmacy!",
                footerFont,
                Brushes.DarkGreen,
                startX + 200,
                y);

            y += 25;

            e.Graphics.DrawString(
                "PHARMACY MANAGEMENT SYSTEM",
                footerFont,
                Brushes.Gray,
                startX + 240,
                y);
        }

        private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ClearBill();
        }
    }
   }
    
    

