using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppLearn
{
    public partial class frmLogin : Form
    {
       // Database Connection
        SqlConnection con=new DBConnection().getDBConnection();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void picHide_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void picHide_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            con.Open();
            string username = txtUsername.Text;
            string password = new Encrypt().encryptData(txtPassword.Text);
            SqlCommand cmd = new SqlCommand("select 1 from Login where username=@UN and password=@PW", con);
            cmd.Parameters.AddWithValue("@UN", username);
            cmd.Parameters.AddWithValue("@PW",password);
           
            SqlDataReader reader=cmd.ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("login successful!");       
            }
            else {
                MessageBox.Show("Please enter valid details...");
            }
            con.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            con.Open();
            string username = txtUsername.Text;
            string password = new Encrypt().encryptData(txtPassword.Text);
            SqlCommand cmd = new SqlCommand("insert into Login (username,password) select @UN, @PW where not exists (select username from Login where username=@UN)", con);

            cmd.Parameters.AddWithValue("@UN", username);
            cmd.Parameters.AddWithValue("@PW", password);
            int result=cmd.ExecuteNonQuery();

            if (result == 0)
            {
                MessageBox.Show("user already exists.");
            }
            else {
                MessageBox.Show("Signup successful");
            }
            con.Close();
        }
    }
}
