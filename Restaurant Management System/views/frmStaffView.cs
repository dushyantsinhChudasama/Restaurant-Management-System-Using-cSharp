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

namespace Restaurant_Management_System
{
    public partial class frmStaffView : Form
    {
        public frmStaffView()
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
            string qry = "select * from staff_tbl";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvId);
            lb.Items.Add(dgvName);
            lb.Items.Add(dgvPhone);
            lb.Items.Add(dgvRole);
            lb.Items.Add(dgvSalary);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
        }

        private void frmStaffView_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnAddStaff_Click(object sender, EventArgs e)
        {
            frmStaffAdd frm = new frmStaffAdd();
            frm.ShowDialog();
            GetData();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            connect();


            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {
                frmStaffAdd frm = new frmStaffAdd();
                // Pass the category ID and name to frmCategoryAdd for editing
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtName.Text = guna2DataGridView1.CurrentRow.Cells["dgvName"].Value.ToString();
                frm.txtPhone.Text = guna2DataGridView1.CurrentRow.Cells["dgvPhone"].Value.ToString();
                string role = guna2DataGridView1.CurrentRow.Cells["dgvRole"].Value.ToString();
                frm.cbRole.SelectedValue = role;
                frm.txtSalary.Text = guna2DataGridView1.CurrentRow.Cells["dgvSalary"].Value.ToString();

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

                    SqlCommand cmd = new SqlCommand("delete from staff_tbl where staffID = @sID", con);
                    cmd.Parameters.AddWithValue("@sID", id);
                    cmd.ExecuteNonQuery();

                    DeletMessaegBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    DeletMessaegBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;

                    DeletMessaegBox.Show("Staff Deleted Successfully...");

                    GetData();
                }

            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
