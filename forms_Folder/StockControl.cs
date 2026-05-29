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
using WindowsFormsApp1.Services;

namespace WindowsFormsApp1.forms_Folder
{
    public partial class StockControl : UserControl
    {
        private StockRepository _repo = new StockRepository();
        private readonly StockService _stockService = new StockService();
        public StockControl()
        {
            InitializeComponent();
        }

        private void StockControl_Load(object sender, EventArgs e)
        {

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetupGrid();
            LoadStock();
            LoadMedicinesToCombo(); // 👈 add karo

        }

        // 🔹 Columns banane ka code
        private void SetupGrid()
        {
            dgvStock.Columns.Clear();

            dgvStock.Columns.Add("ID", "ID");
            dgvStock.Columns.Add("Name", "Medicine Name");
            dgvStock.Columns.Add("Company", "Company");
            dgvStock.Columns.Add("Qty", "Stock");
        }

        // 🔹 Database se data lana
        private void LoadStock()
        {
            dgvStock.Rows.Clear();
            DataTable dt = _repo.GetAll();
            foreach (DataRow row in dt.Rows)
            {
                int qty = Convert.ToInt32(row["qty"]);
                int rowIndex = dgvStock.Rows.Add(
                    row["id"],
                    row["name"],
                    row["company"],
                    qty
                );
                if (qty < 10)
                {
                    dgvStock.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }



        private void LoadMedicinesToCombo()
        {
            DataTable dt = _repo.GetAll();
            cmbMedicine.DataSource = dt;
            cmbMedicine.DisplayMember = "name";
            cmbMedicine.ValueMember = "id";
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();

            foreach (DataGridViewRow row in dgvStock.Rows)
            {
                if (row.Cells["Name"].Value.ToString().ToLower().Contains(search))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }



        private void btnAddStock_Click(object sender, EventArgs e)
        {

            if (txtAddQty.Text == "")
            {
                MessageBox.Show("Enter Quantity");
                return;
            }

            int qty = Convert.ToInt32(txtAddQty.Text);
            int id = Convert.ToInt32(cmbMedicine.SelectedValue);

            string error = _stockService.AddStock(id, qty);
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            MessageBox.Show("Stock Added!");
            LoadStock();
            txtAddQty.Clear();
        }
    }
}

