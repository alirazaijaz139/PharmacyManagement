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
    public partial class MedicineControl : UserControl
    {
        private MedicineRepository _repo = new MedicineRepository();
        private readonly MedicineValidator _validator = new MedicineValidator();
        public MedicineControl()
        {
            InitializeComponent();

           // dgvMedicines.AutoGenerateColumns = false;

           // SetupGrid();      // 👈 yahan
            



        }
       /* public void SetUserMode()
        {
            btnAdd.Visible = false;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
        }*/
        public void LoadMedicinesPublic()
        {
            //SetupGrid();
            LoadMedicines();
        }

        protected override void OnLoad(EventArgs e)
        {
          base.OnLoad(e);

                // ✅ pehle columns
        LoadMedicines(); // ✅ phir data
         }


                /*               private void SetupGrid()
                                {
                                    if (dgvMedicines.Columns.Count > 0)
                                        return;

                                    dgvMedicines.Columns.Add("ID", "ID");
                                    dgvMedicines.Columns.Add("Name", "Medicine Name");
                                    dgvMedicines.Columns.Add("Company", "Company");
                                    dgvMedicines.Columns.Add("Price", "Price");
                                    dgvMedicines.Columns.Add("Qty", "Quantity");


                                }*/

        private void LoadMedicines()
        {
            dgvMedicines.DataSource = _repo.GetAll();
        }
        
    


    private void btnAdd_Click(object sender, EventArgs e)
       {
            var m = new Medicine
            {
                Name = txtMedicineName.Text,
                Company = txtCompany.Text,
                Price = decimal.TryParse(txtPrice.Text, out decimal p) ? p : 0,
                Qty = int.TryParse(txtQty.Text, out int q) ? q : 0
            };

            string error = _validator.Validate(m);
            if (error != null)
            {
                MessageBox.Show(error);
                return;
            }

            _repo.Add(m);
            MessageBox.Show("Medicine added!");
            LoadMedicines();
            ClearFields();



        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvMedicines.SelectedRows[0].Cells["id"].Value);

            var m = new Medicine
            {
                Id = id,
                Name = txtMedicineName.Text,
                Company = txtCompany.Text,
                Price = Convert.ToDecimal(txtPrice.Text),
                Qty = Convert.ToInt32(txtQty.Text)
            };
            _repo.Update(m);
            MessageBox.Show("Updated!");
            LoadMedicines();


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMedicines.SelectedRows.Count == 0) return;
            int id = Convert.ToInt32(dgvMedicines.SelectedRows[0].Cells["id"].Value);
            _repo.Delete(id);
            MessageBox.Show("Deleted!");
            LoadMedicines();
        }




        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtMedicineName.Clear();
            txtCompany.Clear();
            txtPrice.Clear();
            txtQty.Clear();
        }

        private void dgvMedicines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim().ToLower();

            // ✅ agar empty hai to sab show karo
            if (search == "")
            {
                foreach (DataGridViewRow row in dgvMedicines.Rows)
                {
                    row.Visible = true;
                }
                return;
            }

            foreach (DataGridViewRow row in dgvMedicines.Rows)
            {
                if (row.Cells["Name"].Value.ToString().ToLower().Contains(search) ||
                    row.Cells["Company"].Value.ToString().ToLower().Contains(search))
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }
        

        private void dgvMedicines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtMedicineName.Text = dgvMedicines.Rows[e.RowIndex]
                    .Cells["Name"].Value.ToString();

                txtCompany.Text = dgvMedicines.Rows[e.RowIndex]
                    .Cells["Company"].Value.ToString();

                txtPrice.Text = dgvMedicines.Rows[e.RowIndex]
                    .Cells["Price"].Value.ToString();

                txtQty.Text = dgvMedicines.Rows[e.RowIndex]
                    .Cells["Qty"].Value.ToString();
            }
        }
    }
}

