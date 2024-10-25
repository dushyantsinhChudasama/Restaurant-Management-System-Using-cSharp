using Restaurant_Management_System.Models;
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

namespace Restaurant_Management_System.views
{
    public partial class frmProductview : Form
    {
        public frmProductview()
        {
            InitializeComponent();
        }

        public static String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\RMS.mdf;Integrated Security=True";

        public static SqlConnection con;

        void connect()
        {
            con = new SqlConnection(s);
            con.Open();
        }


        public void GetData()
        {
            string qry = "select pid, pName, pPrice, CategoryID, c.catName from products_tbl p inner join category c on c.catID = p.CategoryID";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvId);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPrice);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvcat);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void frmProductview_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            connect();


            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmProductsAdd frm = new frmProductsAdd();
                // Pass the category ID and name to frmCategoryAdd for editing
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.cid = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvcatID"].Value);

                // Show the form as a dialog
                frm.ShowDialog();

                // Refresh the grid after editing
                GetData();
            }


            else if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdelete")
            {
                DeletMessaegBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;

                if (DeletMessaegBox.Show("Are you sure you wnat to delete?") == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);

                    SqlCommand cmd = new SqlCommand("delete from products_tbl where pID = @pID", con);
                    cmd.Parameters.AddWithValue("@pID", id);
                    cmd.ExecuteNonQuery();

                    DeletMessaegBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    DeletMessaegBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;

                    DeletMessaegBox.Show("Product Deleted Successfully...");

                    GetData();
                }

            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            frmProductsAdd frm = new frmProductsAdd();
            frm.ShowDialog();
            GetData();
        }

        private void brnReport_Click(object sender, EventArgs e)
        {
            string RptType = "Items";
            frmReportView frm = new frmReportView(RptType);
            frm.ShowDialog();
        }
    }
}
