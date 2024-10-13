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
    public partial class frmInventoryAdd : Form
    {
        public frmInventoryAdd()
        {
            InitializeComponent();
        }

        private void frmInventoryAdd_Load(object sender, EventArgs e)
        {
            string qry = "select catID 'id', catName 'name' from category";
            MainClass.CBFill(qry, cbCategory);

            if (cid > 0) // for updating
            {
                cbCategory.SelectedValue = cid;
            }

            // Fill the supplier combo box
            string qry2 = "select sID 'sid', sName 'sname' from supplier_tbl";
            MainClass.CBFillForSuppliers(qry2, cbSupplier); // Changed cbCategory to cbSupplier

            if (id > 0)
            {
                ForUploadData(); 
            }
        }

        public static String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\RMS.mdf;Integrated Security=True";

        public static SqlConnection con;

        void connect()
        {
            con = new SqlConnection(s);
            con.Open();
        }



        public int id = 0;
        public int cid = 0;

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            connect();

            if (id == 0) // If id is 0, insert new category
            {

                SqlCommand cmd = new SqlCommand("insert into inventory_tbl (itmName, CategoryID, SupplerID, Price) values (@itmName, @CatID, @supID, @Price)", con);
                cmd.Parameters.AddWithValue("@itmName", txtName.Text);
                cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(cbCategory.SelectedValue));
                cmd.Parameters.AddWithValue("@supID", Convert.ToInt32(cbSupplier.SelectedValue));
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);


                cmd.ExecuteNonQuery();
                AddMessageBox.Show("Supplier Added Successfully...");
            }
            else // If id is not 0, update the existing category
            {


                SqlCommand cmd = new SqlCommand("update inventory_tbl set itmName = @itmName, CategoryID = @CatID, SupplerID = @supID, Price = @Price where itemID = @iID", con);
                cmd.Parameters.AddWithValue("@itmName", txtName.Text);
                cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(cbCategory.SelectedValue));
                cmd.Parameters.AddWithValue("@supID", Convert.ToInt32(cbSupplier.SelectedValue));
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@iID", id);
                cmd.ExecuteNonQuery();
                AddMessageBox.Show("Supplier Updated Successfully...");
            }

            con.Close(); // Close connection after performing the operation
            this.Close(); // Close the form after saving
        }

        //for uploading data
        private void ForUploadData()
        {
            connect();
            string qry = "select * from inventory_tbl where itemID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["itmName"].ToString();

            }
        }


        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
