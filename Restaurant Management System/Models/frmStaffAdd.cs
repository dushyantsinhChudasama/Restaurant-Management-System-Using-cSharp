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

namespace Restaurant_Management_System.Models
{
    public partial class frmStaffAdd : Form
    {
        public frmStaffAdd()
        {
            InitializeComponent();
        }


        public static String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\RMS.mdf;Integrated Security=True";
        public static SqlConnection con;

        public int id = 0; // This will track whether we are inserting or updating

        void connect()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        private void frmStaffAdd_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            connect();

            if (id == 0) // If id is 0, insert new category
            {
                SqlCommand cmd = new SqlCommand("insert into staff_tbl (sName, sPhone, sRole, sSalary) values (@sName, @sPhone, @sRole, @sSalary)", con);
                cmd.Parameters.AddWithValue("@sName", txtName.Text);
                cmd.Parameters.AddWithValue("@sPhone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@sRole", cbRole.Text);
                cmd.Parameters.AddWithValue("@sSalary", txtSalary.Text);

                cmd.ExecuteNonQuery();
                AddMessageBox.Show("Staff Added Successfully...");
            }
            else // If id is not 0, update the existing category
            {
                SqlCommand cmd = new SqlCommand("update staff_tbl set sName = @sName, sPhone = @sPhone, sRole = @sRole, sSalary = @sSalary where staffID = @sID", con);
                cmd.Parameters.AddWithValue("@sName", txtName.Text);
                cmd.Parameters.AddWithValue("@sPhone", txtPhone.Text);
                cmd.Parameters.AddWithValue(@"sRole", cbRole.Text);
                cmd.Parameters.AddWithValue("@sSalary", txtSalary.Text);
                cmd.Parameters.AddWithValue("@sID", id);
                cmd.ExecuteNonQuery();
                AddMessageBox.Show("Staff Updated Successfully...");
            }

            con.Close(); // Close connection after performing the operation
            this.Close(); // Close the form after saving
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
