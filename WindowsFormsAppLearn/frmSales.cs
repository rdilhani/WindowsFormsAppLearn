using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsAppLearn
{
    public partial class frmSales : Form
    {
        SqlConnection con = new DBConnection().getDBConnection();
        private ComboBox cmbCustomer;
        private ComboBox cmbItems;
        private TextBox txtQty;
        private DataGridView dataGridView1;
        private Label lblTotal;
        private Button btnAddItem;
        private Button btnCompleteSale;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
        List<SaleItem> saleItems = new List<SaleItem>();

        public frmSales()
        {
            InitializeComponent();
            LoadCustomers();
            LoadItems();
        }

        private void LoadCustomers()
        {
            // Populate customer ComboBox from DB
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT cusId, name FROM Customer", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                cmbCustomer.Items.Add(new CustomerComboItem
                {
                    Name = reader["name"].ToString(),
                    Id = reader["cusId"].ToString()
                });
            }
            con.Close();
        }

        private void LoadItems()
        {
            // Populate items ComboBox or DataGridView
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Code, Name, UnitPrice FROM Item", con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               // cmbItems.Items.Add(new { Text = reader["Name"].ToString(), Value = reader["Code"].ToString(), Price = reader["UnitPrice"] });
                 cmbItems.Items.Add(new ItemCombo
                 {
                     Name = reader["Name"].ToString(),
                     Code = reader["Code"].ToString(),
                     UnitPrice = reader["UnitPrice"].ToString(),

                 });

            }
            con.Close();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            // Add selected item to saleItems and update DataGridView
            var selected = (dynamic)cmbItems.SelectedItem;
            int qty = int.Parse(txtQty.Text);
            double price = Convert.ToDouble(selected.Price);

            saleItems.Add(new SaleItem
            {
                ItemCode = selected.Value,
                ItemName = selected.Text,
                Quantity = qty,
                UnitPrice = price
            });

            UpdateGrid();
        }

        private void UpdateGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = saleItems;
            lblTotal.Text = $"Total: {saleItems.Sum(i => i.SubTotal):C2}";
        }

        private void btnCompleteSale_Click(object sender, EventArgs e)
        {
            // Save sale and sale items to DB
            string saleID = Guid.NewGuid().ToString();
            string customerID = ((dynamic)cmbCustomer.SelectedItem).Id;
            double total = saleItems.Sum(i => i.SubTotal);

            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Sale (SaleID, CustomerID, Date, TotalAmount) VALUES (@SaleID, @CustomerID, @Date, @Total)", con);
            cmd.Parameters.AddWithValue("@SaleID", saleID);
            cmd.Parameters.AddWithValue("@CustomerID", customerID);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Total", total);
            cmd.ExecuteNonQuery();

            foreach (var item in saleItems)
            {
                SqlCommand itemCmd = new SqlCommand("INSERT INTO SaleItem (SaleID, ItemCode, Quantity, UnitPrice) VALUES (@SaleID, @ItemCode, @Qty, @Price)", con);
                itemCmd.Parameters.AddWithValue("@SaleID", saleID);
                itemCmd.Parameters.AddWithValue("@ItemCode", item.ItemCode);
                itemCmd.Parameters.AddWithValue("@Qty", item.Quantity);
                itemCmd.Parameters.AddWithValue("@Price", item.UnitPrice);
                itemCmd.ExecuteNonQuery();

                SqlCommand updateQty = new SqlCommand("UPDATE Item SET Qty = Qty - @Qty WHERE Code = @Code", con);
                updateQty.Parameters.AddWithValue("@Qty", item.Quantity);
                updateQty.Parameters.AddWithValue("@Code", item.ItemCode);
                updateQty.ExecuteNonQuery();
            }

            

            con.Close();

            MessageBox.Show("Sale completed!");
            saleItems.Clear();
            UpdateGrid();
        }

        private void InitializeComponent()
        {
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.cmbItems = new System.Windows.Forms.ComboBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnCompleteSale = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(174, 49);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(360, 37);
            this.cmbCustomer.TabIndex = 0;
            // 
            // cmbItems
            // 
            this.cmbItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbItems.FormattingEnabled = true;
            this.cmbItems.Location = new System.Drawing.Point(174, 106);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(360, 37);
            this.cmbItems.TabIndex = 0;
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.Location = new System.Drawing.Point(174, 168);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(236, 34);
            this.txtQty.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 229);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(566, 157);
            this.dataGridView1.TabIndex = 2;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblTotal.Location = new System.Drawing.Point(296, 407);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(117, 29);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Sub Total";
            // 
            // btnAddItem
            // 
            this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddItem.Location = new System.Drawing.Point(416, 167);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(118, 35);
            this.btnAddItem.TabIndex = 4;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnCompleteSale
            // 
            this.btnCompleteSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompleteSale.Location = new System.Drawing.Point(118, 456);
            this.btnCompleteSale.Name = "btnCompleteSale";
            this.btnCompleteSale.Size = new System.Drawing.Size(327, 46);
            this.btnCompleteSale.TabIndex = 5;
            this.btnCompleteSale.Text = "Complete Sale";
            this.btnCompleteSale.UseVisualStyleBackColor = true;
            this.btnCompleteSale.Click += new System.EventHandler(this.btnCompleteSale_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(27, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Quantity";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(27, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Item";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(27, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 29);
            this.label1.TabIndex = 8;
            this.label1.Text = "Customer";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(12, 407);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 29);
            this.label4.TabIndex = 6;
            this.label4.Text = "Total";
            // 
            // frmSales
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(50)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(590, 544);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCompleteSale);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.cmbItems);
            this.Controls.Add(this.cmbCustomer);
            this.Name = "frmSales";
            this.Load += new System.EventHandler(this.frmSales_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmSales_Load(object sender, EventArgs e)
        {

        }
    }
}
