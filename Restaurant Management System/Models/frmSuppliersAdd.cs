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
    public partial class frmSuppliersAdd : Form
    {
        public frmSuppliersAdd()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

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

        private void frmSuppliersAdd_Load(object sender, EventArgs e)
        {
            //for combo box fill

            string qry = "select catID 'id', catName 'name' from category";

            MainClass.CBFill(qry, cbCategory);

            if (cid > 0) // for updating
            {
                cbCategory.SelectedValue = cid;
            }

            if (id > 0)
            {
                ForUploadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            connect();

            if (id == 0) // If id is 0, insert new category
            {

                SqlCommand cmd = new SqlCommand("insert into supplier_tbl (sName, sPhone, CategoryID) values (@sName, @sPhone, @CatID)", con);
                cmd.Parameters.AddWithValue("@sName", txtName.Text);
                cmd.Parameters.AddWithValue("@sPhone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(cbCategory.SelectedValue));



                cmd.ExecuteNonQuery();
                AddMessageBox.Show("Supplier Added Successfully...");
            }
            else // If id is not 0, update the existing category
            {


                SqlCommand cmd = new SqlCommand("update supplier_tbl set sName = @sName, sPhone = @sPhone, CategoryID = @CatID where sID = @sID", con);
                cmd.Parameters.AddWithValue("@sName", txtName.Text);
                cmd.Parameters.AddWithValue("@sPhone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(cbCategory.SelectedValue));
                cmd.Parameters.AddWithValue("@sID", id);
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
            string qry = "select * from supplier_tbl where sID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["sName"].ToString();
                txtPhone.Text = dt.Rows[0]["sPhone"].ToString();

            }
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
