using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsAppLearn
{
    public partial class frmCustomer : Form
    {
        //Database connection (refer to DBConnection.cs file)
        SqlConnection con = new DBConnection().getDBConnection();

        //Customer Object (Refer to Customer.cs file)
        Customer customer = new Customer();


        public frmCustomer()
        {
            InitializeComponent();
        }

        //File Open dialog to select customer image
        private void picCustomer_Click(object sender, EventArgs e)
        {
            OpenFileDialog cusImage = new OpenFileDialog();
            if (cusImage.ShowDialog() == DialogResult.OK)
            {
                picCustomer.Image = new Bitmap(cusImage.FileName);
            }

        }

        private void reset()
        {
            //generater new customer id - Refer to generateID method in customer.cs file
            txtCustomerID.Text = customer.generateID();
            txtCustomerID.Enabled = false;
            btnUpdate.Enabled = false;
            btnUpdate.Visible = false;
            btnEdit.Enabled = true;
            btnEdit.Visible = true;

            txtName.Clear();
            rtxtAddress.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            dtpDOB.ResetText();
            picCustomer.Image = null;
        }
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            reset();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // customer related operations in OO style - Refer to addCustomer method in customer.cs file
            if (isValidAll())
            {
                customer = new Customer(txtCustomerID.Text, txtName.Text, rtxtAddress.Text, cmbGender.SelectedItem.ToString(), dtpDOB.Value.Date.ToString(), txtContact.Text, txtEmail.Text, convertImage());
                customer.addCustomer();
            }
            else
            {
                MessageBox.Show("Fill all Required Feilds!.", "Validation Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //convert customer image to byte[]
        private byte[] convertImage()
        {
            MemoryStream stream = new MemoryStream();
            if (!(picCustomer.Image == null))
            {
                picCustomer.Image.Save(stream, picCustomer.Image.RawFormat);
            }
            else
            {
                MessageBox.Show("Select a valid Customer Image");
            }
            return stream.GetBuffer();
        }

        //Validation ; You may use lables for error messages instead of message boxes
        private void txtName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                // MessageBox.Show("Customer name is required!");
                lblErrName.Text = "Error! Customer name is required";
                txtName.Focus();
            }
            else
            {
                lblErrName.Text = "";
            }
        }


        //Validation ; You may use lables for error messages instead of message boxes
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email is not valid.", "Validation Error",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // Helper method to validate email using System.Net.Mail.MailAddress 
        private bool IsValidEmail(string email)
        {
            try
            {
                // Try to create a new MailAddress object with the input email 
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                // If an exception is thrown, the email is not valid 
                return false;
            }
        }


        //Validation ; You may use lables for error messages instead of message boxes
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (!(Regex.IsMatch(txtName.Text, @"^[a-zA-Z]+$")))
            {

                MessageBox.Show("Name is not valid.", "Validation Error",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Validating all the feilds - confirms no empty feilds
        private bool isValidAll()
        {
            if (null == txtCustomerID.Text || txtName.Text == null || rtxtAddress.Text == null || cmbGender.SelectedItem == null || dtpDOB.Value == null || txtContact.Text == null || txtEmail.Text == null || convertImage() == null) return false;
            return true;

        }

        private void lblErrName_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtCustomerID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //Enable edit form and enable update button to updat records
        private void btnEdit_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            btnUpdate.Visible = true;
            btnEdit.Enabled = false;
            btnEdit.Visible = false;
            txtCustomerID.Enabled = true;
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {

            // customer related operations in OO style - Refer to updateCustomer method in customer.cs file
            if (isValidAll())
            {
                customer = new Customer(txtCustomerID.Text, txtName.Text, rtxtAddress.Text, cmbGender.SelectedItem.ToString(), dtpDOB.Value.Date.ToString(), txtContact.Text, txtEmail.Text, convertImage());
                customer.updateCustomer();
                reset();
            }
            else
            {
                MessageBox.Show("Fill all Required Feilds!.", "Validation Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            customer = new Customer(txtCustomerID.Text);
            customer.deleteCustomer();
            reset();
        }
    }
}
