using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppLearn
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        //Hardcoded login: Allow system login for username:Admin and password:123
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            if (username == "Admin" && password == "123") {
                MessageBox.Show("Login Successful!");              
            }
            else
            {
                MessageBox.Show("Invaid Username or Password!");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void picHide_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void picHide_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        
        private void frmLogin_Load(object sender, EventArgs e)
        {

            /*
             * setting active control.
             In the design veiw of frmLogin, Use,
             view->Tab Order
             set tab order appropriately
            */
            this.ActiveControl = txtUsername; 
        }
    }
}
