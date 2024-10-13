using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Restaurant_Management_System.Models
{
    public partial class frmCategoryAdd : Form
    {
        public frmCategoryAdd()
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

        private void frmCategoryAdd_Load(object sender, EventArgs e)
        {
          
        }

        // Save button click event
        public void guna2Button1_Click(object sender, EventArgs e)
        {
            connect();

            if (id == 0) // If id is 0, insert new category
            {
                SqlCommand cmd = new SqlCommand("insert into category (catName) values (@catName)", con);
                cmd.Parameters.AddWithValue("@catName", txtName.Text);
                cmd.ExecuteNonQuery();
                AddMessageBox.Show("Category Added Successfully...");
            }
            else // If id is not 0, update the existing category
            {
                SqlCommand cmd = new SqlCommand("update category set catName = @catName where catID = @catID", con);
                cmd.Parameters.AddWithValue("@catName", txtName.Text);
                cmd.Parameters.AddWithValue("@catID", id);
                cmd.ExecuteNonQuery();
                AddMessageBox.Show("Category Updated Successfully...");
            }

            con.Close(); // Close connection after performing the operation
            this.Close(); // Close the form after saving
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close(); // Close button
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}