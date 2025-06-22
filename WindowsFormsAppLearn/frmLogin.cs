using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsAppLearn
{
    public partial class frmLogin : Form
    {
        // Database Connection
        SqlConnection con = new DBConnection().getDBConnection();

        public frmLogin()
        {
            InitializeComponent();
        }

        //Validating all the feilds - confirms no empty feilds
        private bool isValidAll()
        {
            if (txtUsername.Text == "" || txtPassword.Text == "") return false;
            return true;

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
            if (isValidAll())
            {
                con.Open();
                string username = txtUsername.Text;
                string password = new Encrypt().encryptData(txtPassword.Text);
                SqlCommand cmd = new SqlCommand("select 1 from Login where username=@UN and password=@PW", con);
                cmd.Parameters.AddWithValue("@UN", username);
                cmd.Parameters.AddWithValue("@PW", password);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    new frmDashboard().Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Please enter valid user credentials...");
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Fill all Required Feilds!.", "Validation Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            // Check if the username and password fields are not empty
        
            if (isValidAll())
            {
                con.Open();
                string username = txtUsername.Text;
                string password = new Encrypt().encryptData(txtPassword.Text);
                SqlCommand cmd = new SqlCommand("insert into Login (username,password) select @UN, @PW where not exists (select username from Login where username=@UN)", con);

                cmd.Parameters.AddWithValue("@UN", username);
                cmd.Parameters.AddWithValue("@PW", password);
                int result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("user already exists.");
                }
                else
                {
                    MessageBox.Show("Signup successful");
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Fill all Required Feilds!.", "Validation Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
