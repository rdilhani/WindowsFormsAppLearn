using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppLearn
{
    public partial class frmItem : Form
    {
        SqlConnection con = new DBConnection().getDBConnection();  
        public frmItem()
        {
            InitializeComponent();
        }

        private void frmItem_Load(object sender, EventArgs e)
        {
            string itemCode = "ITM0000";

            con.Open();
            SqlCommand cmd = new SqlCommand("select code from item", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                itemCode = reader["code"].ToString();
            }

            int newCode = int.Parse(itemCode.Substring(3));
            newCode = newCode + 1;
            itemCode = "ITM" + newCode.ToString("D4");

            txtItemCode.Text = itemCode;
            con.Close();
            DisplayGrid(); //calling DisplayGrid method
        }

        private void DisplayGrid() { 
        con.Open();
        SqlCommand cmd = new SqlCommand("select Code, Name,Description,Qty,UnitPrice from item",con);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridItems.DataSource = dataTable;
        con.Close();
        }

        private void picItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog= new OpenFileDialog();
            if(dialog.ShowDialog()==DialogResult.OK)
            {
                picItem.Image=new Bitmap(dialog.FileName);
            }
        }

        public byte[] convertImage() {
            Image img = picItem.Image;
            MemoryStream stream = new MemoryStream();
            if (img != null)
            {
                img.Save(stream, img.RawFormat);
            }
            return stream.GetBuffer();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into item values (@Code,@Name,@Description,@UnitPrice,@Qty,@Image)", con);
                cmd.Parameters.AddWithValue("@Code", txtItemCode.Text);
                cmd.Parameters.AddWithValue("@Name", txtItemName.Text);
                cmd.Parameters.AddWithValue("@Description", rtxtDescription.Text);
                cmd.Parameters.AddWithValue("@UnitPrice", double.Parse(txtUnitPrice.Text));
                cmd.Parameters.AddWithValue("@Qty", int.Parse(txtQty.Text));
                cmd.Parameters.AddWithValue("@Image", convertImage());
                cmd.ExecuteNonQuery();
                con.Close();
                

                MessageBox.Show("Item Added Successfully!");
                DisplayGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update item set Name=@Name,Description=@Description,UnitPrice=@UnitPrice,Qty=@Qty,Image=@Image)", con);
                cmd.Parameters.AddWithValue("@Code", txtItemCode.Text);
                cmd.Parameters.AddWithValue("@Name", txtItemName.Text);
                cmd.Parameters.AddWithValue("@Description", rtxtDescription.Text);
                cmd.Parameters.AddWithValue("@UnitPrice", double.Parse(txtUnitPrice.Text));
                cmd.Parameters.AddWithValue("@Qty", int.Parse(txtQty.Text));
                cmd.Parameters.AddWithValue("@Image", convertImage());
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Item Updated Successfully!");
                DisplayGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {          
            con.Open();
            SqlCommand cmd = new SqlCommand("Delete item where Code=@Code", con);
            cmd.Parameters.AddWithValue("@Code", txtItemCode.Text);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void dataGridItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    txtItemCode.Text = dataGridItems.SelectedRows[0].Cells[0].Value.ToString();
                    txtItemName.Text = dataGridItems.SelectedRows[0].Cells[1].Value.ToString();
                    rtxtDescription.Text = dataGridItems.SelectedRows[0].Cells[2].Value.ToString();
                    txtQty.Text = dataGridItems.SelectedRows[0].Cells[3].Value.ToString();
                    txtUnitPrice.Text = dataGridItems.SelectedRows[0].Cells[4].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
