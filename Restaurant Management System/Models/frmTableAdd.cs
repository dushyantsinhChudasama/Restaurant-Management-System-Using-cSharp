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
    public partial class frmTableAdd : Form
    {
        public frmTableAdd()
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            connect();

            if (id == 0) // If id is 0, insert new category
            {
                SqlCommand cmd = new SqlCommand("insert into tables_tbl (tname, tcapicity) values (@tname, @tcapicity)", con);
                cmd.Parameters.AddWithValue("@tname", txtName.Text);
                cmd.Parameters.AddWithValue("@tcapicity", txtcapicity.Text);
                cmd.ExecuteNonQuery();
                AddTableBox.Show("Table Added Successfully...");
            }
            else // If id is not 0, update the existing category
            {
                SqlCommand cmd = new SqlCommand("update tables_tbl set tname = @tname, tcapicity = @tcapicity where tid = @tid", con);
                cmd.Parameters.AddWithValue("@tname", txtName.Text);
                cmd.Parameters.AddWithValue("@tcapicity", txtcapicity.Text);
                cmd.Parameters.AddWithValue("@tid", id);
                cmd.ExecuteNonQuery();
                AddTableBox.Show("Table Updated Successfully...");
            }

            con.Close(); // Close connection after performing the operation
            this.Close(); // Close the form after saving
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
