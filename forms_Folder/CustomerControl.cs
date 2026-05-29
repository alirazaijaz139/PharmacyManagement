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
using WindowsFormsApp1.Validators;

namespace WindowsFormsApp1.forms_Folder
{
    
    public partial class CustomerControl : UserControl
    {
        private CustomerRepository _repo = new CustomerRepository();
        private readonly CustomerValidator _validator = new CustomerValidator();
        public CustomerControl()
        {
            InitializeComponent();




        }
        
    protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SetupDataGridView();
            LoadCustomers();
        }


        // DataGridView columns set karo
        private void SetupDataGridView()
        {
            if (dgvCustomers.Columns.Count > 0) return;

          
            dgvCustomers.Columns.Add("ID", "ID");
            dgvCustomers.Columns.Add("Name", "Customer Name");
            dgvCustomers.Columns.Add("Account", "Account Number");
            dgvCustomers.Columns.Add("Phone", "Phone");
            dgvCustomers.Columns.Add("Address", "Address");
            dgvCustomers.Columns.Add("ShopName", "Shop Name");


        }


        private void LoadCustomers()
        {

            dgvCustomers.Rows.Clear();
            var dt = _repo.GetAll();
            foreach (DataRow row in dt.Rows)
            {
                dgvCustomers.Rows.Add(
                    row["id"].ToString(),
                    row["name"].ToString(),
                    row["account"].ToString(),
                    row["phone"].ToString(),
                    row["address"].ToString(),
                    row["shop_name"].ToString()
                );
            }
        }
            
        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var c = new Customer
            {
                Name = txtCustomerName.Text,

                Account = txtAccountNumber.Text,

                Phone = txtPhone.Text,

                Address = txtAddress.Text,

                ShopName = txtShopName.Text
            };

            string error = _validator.Validate(c);
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            _repo.Add(c);
            MessageBox.Show("Customer Added!");
            LoadCustomers();
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (dgvCustomers.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["ID"].Value);

            var c = new Customer
            {
                Id = id,
                Name = txtCustomerName.Text,
                Account = txtAccountNumber.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text,
                ShopName = txtShopName.Text
            };
            _repo.Update(c);
            MessageBox.Show("Updated!");
            LoadCustomers();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["ID"].Value);
            _repo.Delete(id);
            MessageBox.Show("Deleted!");
            LoadCustomers();
        }
        

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            txtCustomerName.Clear();
            txtAccountNumber.Clear();
            txtPhone.Clear();
            txtShopName.Clear();
            txtAddress.Clear();
        }
        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtCustomerName.Text = dgvCustomers.Rows[e.RowIndex]
                    .Cells["Name"].Value.ToString();
                txtAccountNumber.Text = dgvCustomers.Rows[e.RowIndex]
                    .Cells["Account"].Value.ToString();
                txtPhone.Text = dgvCustomers.Rows[e.RowIndex]
                    .Cells["Phone"].Value.ToString();
                txtAddress.Text = dgvCustomers.Rows[e.RowIndex]
                    .Cells["Address"].Value.ToString();
                txtShopName.Text = dgvCustomers.Rows[e.RowIndex]
                    .Cells["ShopName"].Value.ToString();
            }
        }

        private void lblSearch_Click(object sender, EventArgs e)
        {

        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower();

            foreach (DataGridViewRow row in dgvCustomers.Rows)
            {
                // Name ya Account number mein search karo
                if (row.Cells["Name"].Value.ToString().ToLower()
                    .Contains(searchText) ||
                    row.Cells["Account"].Value.ToString().ToLower()
                    .Contains(searchText))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void dgvCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
    

