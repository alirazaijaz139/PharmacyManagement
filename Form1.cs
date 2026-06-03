using System;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.forms_Folder;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Login Form - Entry point of application
    /// Sirf LoginService call karta hai - Abstraction
    /// Direct database access nahi 
    /// </summary>
    public partial class Form1 : Form
    {
        // LoginService - Business logic yahan nahi, service mein hai
        private readonly LoginService _loginService = new LoginService();
        public Form1()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;

        }

        /// Login button click - Service se login karta hai
        private void btnlogin_Click(object sender, EventArgs e)
        {
            User user;
            // LoginService se login karo
            string error = _loginService.Login(
                txtUsername.Text.Trim(),
                txtPassword.Text.Trim(),
                out user);

            if (error != null)
            {
                MessageBox.Show(error);
                txtPassword.Clear();
                return;
            }
            // Role check karo - Admin ya User
            if (user.Role == "admin")
                new AdminForm().Show();
            else
                new UserForm().Show();

            this.Hide();
        }


        /// Forgot Password link click - ForgotPasswordForm kholta hai

        private void lnkForgotPassword_Click(object sender, EventArgs e)
        {

            new ForgotPasswordForm().ShowDialog();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    }
   

