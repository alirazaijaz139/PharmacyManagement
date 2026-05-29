using System;
using System.Windows.Forms;
using WindowsFormsApp1.Models;
using WindowsFormsApp1.Services;
using WindowsFormsApp1.forms_Folder;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly LoginService _loginService = new LoginService();
        public Form1()
        {
            InitializeComponent();

        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            User user;
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

            if (user.Role == "admin")
                new AdminForm().Show();
            else
                new UserForm().Show();

            this.Hide();
        }

        

        private void lnkForgotPassword_Click(object sender, EventArgs e)
        {

            new ForgotPasswordForm().ShowDialog();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    }
   

