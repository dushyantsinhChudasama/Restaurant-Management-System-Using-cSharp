using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_System.Models
{
    public partial class frmProductsAdd : Form
    {
        public frmProductsAdd()
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



        public int id = 0;
        public int cid = 0;

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string filePath;
        Byte[] imageByteArray;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png)|* .png; *.jpg"; 

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtIamge.Image = new Bitmap(filePath);
            }
        }

        private void frmProductsAdd_Load(object sender, EventArgs e)
        {
            //for combo box fill

            string qry = "select catID 'id', catName 'name' from category";

            MainClass.CBFill(qry, cbCategory);

            if(cid > 0 ) // for updating
            {
                cbCategory.SelectedValue = cid;
            }

            if(id > 0)
            {
                ForUploadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            connect();

            if (id == 0) // If id is 0, insert new category
            {
                //for Image

                Image temp = new Bitmap(txtIamge.Image);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageByteArray = ms.ToArray();

                SqlCommand cmd = new SqlCommand("insert into products_tbl (pName, pPrice, CategoryID, pImage) values (@pName, @pPrice, @CatID, @image)", con);
                cmd.Parameters.AddWithValue("@pName", txtName.Text);
                cmd.Parameters.AddWithValue("@pPrice", txtPrice.Text);
                cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(cbCategory.SelectedValue));
                cmd.Parameters.AddWithValue("@image", imageByteArray);

               

                cmd.ExecuteNonQuery();
                AddMessageBox.Show("Item Added Successfully...");
            }
            else // If id is not 0, update the existing category
            {

                //for Image

                Image temp = new Bitmap(txtIamge.Image);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageByteArray = ms.ToArray();


                SqlCommand cmd = new SqlCommand("update products_tbl set pName = @pName, pPrice = @pPrice, CategoryID = @CatID, pImage = @image where pID = @pID", con);
                cmd.Parameters.AddWithValue("@pName", txtName.Text);
                cmd.Parameters.AddWithValue("@pPrice", txtPrice.Text);
                cmd.Parameters.AddWithValue("@CatID", Convert.ToInt32(cbCategory.SelectedValue));
                cmd.Parameters.AddWithValue("@image", imageByteArray);
                cmd.Parameters.AddWithValue("@pID", id);
                cmd.ExecuteNonQuery();
                AddMessageBox.Show("Item Updated Successfully...");
            }

            con.Close(); // Close connection after performing the operation
            this.Close(); // Close the form after saving
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }

        //for uploading data
        private void ForUploadData()
        {
            connect();
            string qry = "select * from products_tbl where pID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["pName"].ToString();
                txtPrice.Text = dt.Rows[0]["pPrice"].ToString();

                Byte[] imageArray = (byte[])(dt.Rows[0]["pImage"]);
                byte[] imageByteArray = imageArray;
                txtIamge.Image = Image.FromStream(new MemoryStream(imageArray));
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
