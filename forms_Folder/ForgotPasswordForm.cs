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
    public partial class ForgotPasswordForm : Form
    {

        public ForgotPasswordForm()
        {
            InitializeComponent();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            if (txtUsername.Text.Trim() == "" ||
                txtNewPassword.Text.Trim() == "" ||
                txtConfirmPassword.Text.Trim() == "")
            {
                MessageBox.Show("All fields are required!");
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Passwords do not match!");
                return;
            }

            try
            {
                UserRepository repo = new UserRepository();

                // Check user exists
                if (!repo.UsernameExists(txtUsername.Text.Trim()))
                {
                    MessageBox.Show("Username not found!");
                    return;
                }

                // Reset password
                int rows = repo.ResetPassword(
                    txtUsername.Text.Trim(),
                    txtNewPassword.Text.Trim()
                );

                if (rows > 0)
                {
                    MessageBox.Show("Password reset successful!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Reset failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
    }



