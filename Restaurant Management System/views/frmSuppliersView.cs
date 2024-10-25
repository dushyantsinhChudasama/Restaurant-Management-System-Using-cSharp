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
    public partial class frmSuppliersView : Form
    {
        public frmSuppliersView()
        {
            InitializeComponent();
        }

        private void frmSuppliersView_Load(object sender, EventArgs e)
        {
            GetData();
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
            string qry = "select sID, sName, sPhone, CategoryID, c.catName from supplier_tbl s inner join category c on c.catID = s.CategoryID";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvId);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPhone);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvcat);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            connect();


            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmSuppliersAdd frm = new frmSuppliersAdd();
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

                    SqlCommand cmd = new SqlCommand("delete from supplier_tbl where sID = @sID", con);
                    cmd.Parameters.AddWithValue("@sID", id);
                    cmd.ExecuteNonQuery();

                    DeletMessaegBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    DeletMessaegBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;

                    DeletMessaegBox.Show("Supplier Deleted Successfully...");

                    GetData();
                }

            }
        }

        private void btnAddSuppllier_Click(object sender, EventArgs e)
        {
            frmSuppliersAdd frm = new frmSuppliersAdd();
            frm.ShowDialog();
            GetData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string RptType = "Supplier";
            frmReportView frm = new frmReportView(RptType);
            frm.ShowDialog();
        }
    }
}
