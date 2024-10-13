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
using System.IO;
using System.Collections;

namespace Restaurant_Management_System.Models
{
    public partial class frmPOS : Form
    {
        public frmPOS()
        {
            InitializeComponent();
        }

        public int MainID = 0;
        public int detaildID = 0;
        public string OrderType;

        //public static string s = @"data source=(localdb)\mssqllocaldb;attachdbfilename=c:\users\d.k chudasama\source\repos\restaurant management system\restaurant management system\rms.mdf;integrated security=true";

        //public static sqlconnection con;

        //public void connect()
        //{
        //    con = new sqlconnection(s);
        //    con.open();
        //}
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            ProductPanel.Controls.Clear();
            LoadProducts();
        }

        private void AddCategory()
        {
            string qry = "Select * from category";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CategoryPanel.Controls.Clear();

            if(dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                    b.FillColor = Color.FromArgb(50, 55, 89);
                    b.Size = new Size(186, 45);
                    b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                    b.Text = row["catName"].ToString();

                    //event for click
                    b.Click += new EventHandler(b_Click);

                    CategoryPanel.Controls.Add(b);
                }
            }
        }

        private void b_Click(object sender, EventArgs e)    
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            foreach (var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PCategory.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }

        private void AddItems(string id,String proID,  string name, string cat, string price, Image pimage)
        {
            var w = new ucProduct()
            {
                PName = name,
                PPrice = price,
                PCategory = cat,
                PImage = pimage,
                id = Convert.ToInt32(proID)
            };

            ProductPanel.Controls.Add(w);

            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    //here, it will check if the producct is already there then update quantity and price 
                    if (Convert.ToInt32(item.Cells["dgvproID"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;

                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                        double.Parse(item.Cells["dgvPrice"].Value.ToString());
                        return;
                    }
                }
                //this line will add new product in data grid view
                guna2DataGridView1.Rows.Add(new object[] { 0,0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice }); //here first 0 for srNo and second 0 for from id
                GetTotal();
            };
        }

        //Getting products for show from database

        private void LoadProducts()
        {
            string qry = "select * from products_tbl inner join category on catID = CategoryID";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow item in dt.Rows)
            {
                Byte[] imagearray = (byte[])item["pImage"];
                byte[] imagebytearray = imagearray;

                //this code will call above AddItems function to add items
                AddItems("0",item["pID"].ToString(), item["pName"].ToString(), item["catName"].ToString(),
                    item["pPrice"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            foreach(var item in ProductPanel.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.PName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //for Serial no in datagrid view

            int count = 0;

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;

                row.Cells[0].Value = count;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void GetTotal()
        {
            double tot = 0;

            lblTotal.Text = "";

            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                tot += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }

            lblTotal.Text = tot.ToString("N2");
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTotal.Text = "";
            guna2DataGridView1.Rows.Clear();
            MainID = 0;
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            OrderType = "Delivery";
        }

        private void btnTake_Click(object sender, EventArgs e)
        {
            OrderType = "Take Away";
        }

        private void btnDin_Click(object sender, EventArgs e)
        {
            OrderType = "Din in";
        }

        private void btnKOT_Click(object sender, EventArgs e)
        {
            // Save the data in database
            string qry = "";
            string qry2 = "";

            if(MainID == 0) //Insert
            {
                qry = @"Insert into tblMain_tbl Values (@aDate, @aTime, @status, @orderType, @total, @recieved, @change);
                        Select SCOPE_IDENTITY()"; //this line will get recent added id value
            }
            else //Update
            {
                qry = @"Update tblMain_tbl Set status = @status, total =  @total, recieved =  @recieved, change = @change where MainID = @ID)";
            }

            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            cmd.Parameters.AddWithValue("@ID", MainID);
            cmd.Parameters.AddWithValue("@aDate", Convert.ToDateTime(DateTime.Now.Date));
            cmd.Parameters.AddWithValue("@aTime", DateTime.Now.ToShortTimeString());
            cmd.Parameters.AddWithValue("@status", "Pending");
            cmd.Parameters.AddWithValue("@orderType", OrderType);
            cmd.Parameters.AddWithValue("@total", Convert.ToDouble(0));  // here the value is 0 becaues ve are saving data for temp value it wil be updated when the payment recieve
            cmd.Parameters.AddWithValue("@recieved", Convert.ToDouble(0));
            cmd.Parameters.AddWithValue("@change", Convert.ToDouble(0));

            if(MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
            if (MainID == 0) 
            {
                MainID = Convert.ToInt32(cmd.ExecuteScalar()); 
            }
            else
            {
                cmd.ExecuteNonQuery();
            }

            if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }

            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                detaildID = Convert.ToInt32(row.Cells["dgvId"].Value);

                if(detaildID == 0)
                {
                    qry2 = "Insert into Details_tbl values (@MainID, @proID, @qty, @price, @amount)";
                }
                else
                {
                    qry2 = "Update Details_tbl Set proID = @proID, qty = @qty, price = @price, amount = @amount where DetailID = @ID";

                }

                SqlCommand cmd2 = new SqlCommand(qry2, MainClass.con);
                cmd2.Parameters.AddWithValue("@ID", detaildID);
                cmd2.Parameters.AddWithValue("@MainID", MainID);
                cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvQty"].Value));
                cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));

                if (MainClass.con.State == ConnectionState.Closed) { MainClass.con.Open(); }
                cmd2.ExecuteNonQuery();
                if (MainClass.con.State == ConnectionState.Open) { MainClass.con.Close(); }

                guna2MessageDialog1.Show("Order Saved Successfully!");
                MainID = 0;
                detaildID = 0;
                guna2DataGridView1.Rows.Clear();
                lblTotal.Text = "";

            }

        }


    }
}
