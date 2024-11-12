using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppLearn
{
    public partial class frmDashboard : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int left, int top, int right, int bottom, int width, int height);
        public frmDashboard()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 40, 40));
        }

        //loading forms to topPanel
        private void loadForm(Form frm) {
            if (this.pnlTop.Controls.Count > 0)
            {
                this.pnlTop.Controls.RemoveAt(0);
            }
            frm.TopLevel = false;
            frm.Dock = DockStyle.Fill;
            pnlTop.Controls.Add(frm);
            frm.Show();
        } 

        private void btnItems_Click(object sender, EventArgs e)
        {
          //  loadForm(new frmItems());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
           // loadForm(new frmCustomers());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
