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
    public partial class frmInventoryView : Form
    {
        public frmInventoryView()
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
            string qry = "SELECT i.itemID, i.itmName, i.CategoryID, c.catName, i.SupplerID, s.sName, i.dateAdded, i.Price FROM inventory_tbl i INNER JOIN category c ON c.catID = i.CategoryID INNER JOIN supplier_tbl s ON s.sID = i.SupplerID;";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvId);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvcat);
            lb.Items.Add(dgvsupID);
            lb.Items.Add(dgvSupplier);
            lb.Items.Add(dgvDate);
            lb.Items.Add(dgvPrice);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void frmInventoryView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            connect();


            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmInventoryAdd frm = new frmInventoryAdd();
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

                    SqlCommand cmd = new SqlCommand("delete from inventory_tbl where itemID = @iID", con);
                    cmd.Parameters.AddWithValue("@iID", id);
                    cmd.ExecuteNonQuery();

                    DeletMessaegBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    DeletMessaegBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;

                    DeletMessaegBox.Show("Item Deleted Successfully...");

                    GetData();
                }

            }
        }

        private void btnAddInventory_Click(object sender, EventArgs e)
        {
            frmInventoryAdd frm = new frmInventoryAdd();
            frm.ShowDialog();
            GetData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
