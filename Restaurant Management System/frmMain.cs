using Restaurant_Management_System.views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_System
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        //Mthod for adding controls(for buttons) in Main form

        public void AddControl(Form f)
        {
            CenterPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            CenterPanel.Controls.Add(f);
            f.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //here the timer will get current time and other like lblday will get current day
            timer1.Start();
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblTime.Text = DateTime.Now.ToShortTimeString();
            lblDay.Text = DateTime.Now.ToString("dddd");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            AddControl(new frmProductview());
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            
        }

        //this is timer that will get current time
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Start();
            lblTime.Text = DateTime.Now.ToShortTimeString();
        }

        private void btnDashbaord_Click(object sender, EventArgs e)
        {
            //here the add control method will be called and then the object of form will passed to the method
            AddControl(new frmDashboard());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin lfrm = new frmLogin();
            this.Close();
            lfrm.Show();
        }

        public void btnCategory_Click(object sender, EventArgs e)
        {
            AddControl(new frmCategoryView());
        }

        private void btnTables_Click(object sender, EventArgs e)
        {
            AddControl(new frmTable());
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControl(new frmStaffView());
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            AddControl(new frmSuppliersView());
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            AddControl(new frmInventoryView());
        }

        private void btnOrderList_Click(object sender, EventArgs e)
        {
            AddControl(new frmOrderView());
        }

        private void btnSpecialOrders_Click(object sender, EventArgs e)
        {
            AddControl(new frmSpecialOrderView());
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            AddControl(new frmBillingview());
        }
    }
}
