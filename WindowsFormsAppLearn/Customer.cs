using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsAppLearn
{

    internal class Customer
    {
        SqlConnection con = new DBConnection().getDBConnection();
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string dateOfBirth { get; set; }
        public byte[] image { get; set; }

        public Customer() { }
        public Customer(string id) {
            this.id = id;
        }
        public Customer(string id, string name, string address, string gender, string dob, string contact, string email, byte[] image) {
            this.id = id;
            this.name = name;
            this.address = address;
            this.gender = gender;
            this.email = email;
            this.phone = contact;
            this.dateOfBirth = dob;
            this.image = image;
        }
        public string generateID() {

            string cusID = "0000";
            con.Open();
            SqlCommand cmd = new SqlCommand("select cusId from customer", con);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    cusID = reader["cusId"].ToString();
                }
                int id = Convert.ToInt32(cusID.Substring(3));
                id = id + 1;
                String nextCusId = "CUS" + id.ToString("D4");
                con.Close();
                return nextCusId;
            }
            
        }
        public void addCustomer() {

            if (!isAvailableCustomer(this))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into customer values (@Id,@Name,@Address,@Gender,@Dob,@Contact,@Email,@Image)", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Dob", dateOfBirth);
                cmd.Parameters.AddWithValue("@Contact", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Image", image);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data inserted successfully");
            }
            else {
                MessageBox.Show("this customer already available");
            }

        }

        public void updateCustomer()
        {

            if (isAvailableCustomer(this))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update customer set name=@Name,address=@Address,gender=@Gender,dob=@Dob,contact=@Contact,email=@Email,image=@Image where cusId=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@Dob", dateOfBirth);
                cmd.Parameters.AddWithValue("@Contact", phone);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Image", image);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Data updated successfully");
            }
            else
            {
                MessageBox.Show("This is a new Customer! Add the record first.");
            }

        }

        public void deleteCustomer()
        {

            if (isAvailableCustomer(this))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete customer where cusId=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Record deleted successfully");
            }
            else
            {
                MessageBox.Show("This is a new Customer! Add the record first.");
            }

        }

        public bool isAvailableCustomer(Customer c)
        {
            bool isAvailable = false;
            con.Open();
            SqlCommand cmd = new SqlCommand("select 1 from customer where cusId=@ID",con);
            cmd.Parameters.AddWithValue("@Id",c.id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                isAvailable= true;
            }
            con.Close() ;
            return isAvailable;
        }

    }
}
