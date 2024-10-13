using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Restaurant_Management_System.Models;
    
namespace Restaurant_Management_System.views
{
    public partial class frmTable : Form
    {
        public frmTable()
        {
            InitializeComponent();
        }

        private void frmTable_Load(object sender, EventArgs e)
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
            string qry = "select * from tables_tbl";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvId);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvCapicity);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            frmCategoryAdd frm = new frmCategoryAdd();
            frm.ShowDialog();
            GetData();
        }

        private void btnAddTable_Click_1(object sender, EventArgs e)
        {
            frmTableAdd frm = new frmTableAdd();
            frm.ShowDialog();
            GetData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            connect();


            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmTableAdd frm = new frmTableAdd();
                // Pass the category ID and name to frmCategoryAdd for editing
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = guna2DataGridView1.CurrentRow.Cells["dgvName"].Value.ToString();
                frm.txtcapicity.Text = guna2DataGridView1.CurrentRow.Cells["dgvCapicity"].Value.ToString();
                // Show the form as a dialog
                frm.ShowDialog();

                // Refresh the grid after editing
                GetData();
            }


            else if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvdelete")
            {
                DeMessaegBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;

                if (DeMessaegBox.Show("Are you sure you wnat to delete?") == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);

                    SqlCommand cmd = new SqlCommand("delete from tables_tbl where tid = @tid", con);
                    cmd.Parameters.AddWithValue("@tid", id);
                    cmd.ExecuteNonQuery();

                    DeMessaegBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    DeMessaegBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;

                    DeMessaegBox.Show("Category Deleted Successfully...");

                    GetData();
                }
            }


        }

        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }


    }
}
