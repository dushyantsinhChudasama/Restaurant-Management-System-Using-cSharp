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
    public partial class frmPOSSpecialOrder : Form
    {
        public frmPOSSpecialOrder()
        {
            InitializeComponent();
        }

        private void frmPOSSpecialOrder_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
            AddCategory();

            dateTimePicker1.CustomFormat = " "; // Blank format
            dateTimePicker1.Format = DateTimePickerFormat.Custom;

            ProductPanel.Controls.Clear();
            LoadProducts();
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

        //private void frmPOS_Load(object sender, EventArgs e)
        //{
        //    guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;
        //    AddCategory();

        //    ProductPanel.Controls.Clear();
        //    LoadProducts();
        //}

        private void AddCategory()
        {
            Guna.UI2.WinForms.Guna2Button allCategoriesButton = new Guna.UI2.WinForms.Guna2Button
            {
                Text = "All Categories",
                FillColor = Color.FromArgb(50, 55, 89),
                Size = new Size(186, 45),
                ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton
            };

            // Event for click - show all products
            allCategoriesButton.Click += (sender, e) =>
            {
                foreach (Control item in ProductPanel.Controls)
                {
                    if (item is ucProduct pro)
                    {
                        pro.Visible = true; // Show all products
                    }
                }
            };


            string qry = "Select * from category";
            SqlCommand cmd = new SqlCommand(qry, MainClass.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            CategoryPanel.Controls.Clear();

            // Add the "All Categories" button to the CategoryPanel
            CategoryPanel.Controls.Add(allCategoriesButton);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
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

        private void AddItems(string id, String proID, string name, string cat, string price, Image pimage)
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
                guna2DataGridView1.Rows.Add(new object[] { 0, 0, wdg.id, wdg.PName, 1, wdg.PPrice, wdg.PPrice }); //here first 0 for srNo and second 0 for from id
                GetTotal();
                SrNO();
            };
        }

        private void SrNO()
        {
                int count = 1; // Start from 1

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    row.Cells["dgvSno"].Value = count; // Ensure the column name is correct
                    count++;
                }

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
                // byte[] imagebytearray = imagearray;

                //this code will call above AddItems function to add items
                AddItems("0", item["pID"].ToString(), item["pName"].ToString(), item["catName"].ToString(),
                    item["pPrice"].ToString(), Image.FromStream(new MemoryStream(imagearray)));
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in ProductPanel.Controls)
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
            if (OrderType == null)
            {
                guna2MessageDialog1.Show("Please select Order Type!!");
                return;
            }

            if (guna2DataGridView1.Rows.Count == 0)
            {
                guna2MessageDialog1.Show("No items to save!");
                return;
            }

            // Calculate the actual total
            double actualTotal = 0.0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                actualTotal += Convert.ToDouble(row.Cells["dgvAmount"].Value);
            }

            string qryMain;
            string qryDetail;

            // Get the date selected in the DateTimePicker (without time)
            DateTime selectedDate = dateTimePicker1.Value.Date; // Get only the date part

            // Determine if we are inserting or updating the main order
            if (MainID == 0) // Insert
            {
                qryMain = @"INSERT INTO tblMain_tbl (aDate, aTime, status, orderType, total, recieved, change, isSpecial)
                    VALUES (@aDate, @aTime, @status, @orderType, @total, @recieved, @change, @isSpecial);
                    SELECT SCOPE_IDENTITY();";  // Get the new ID
            }
            else // Update
            {
                qryMain = @"UPDATE tblMain_tbl 
                    SET status = @status, total = @total, recieved = @recieved, change = @change 
                    WHERE MainID = @ID;";
            }

            try
            {
                SqlConnection con = MainClass.con;
                if (con.State == ConnectionState.Closed) con.Open();

                // Save or update the main order details
                SqlCommand cmdMain = new SqlCommand(qryMain, con);
                cmdMain.Parameters.AddWithValue("@ID", MainID);
                cmdMain.Parameters.AddWithValue("@aDate", selectedDate);  // Pass only the date part here
                cmdMain.Parameters.AddWithValue("@aTime", DateTime.Now.ToShortTimeString());
                cmdMain.Parameters.AddWithValue("@status", "Pending");
                cmdMain.Parameters.AddWithValue("@orderType", OrderType);
                cmdMain.Parameters.AddWithValue("@total", actualTotal);  // Use the calculated total
                cmdMain.Parameters.AddWithValue("@recieved", 0.0);
                cmdMain.Parameters.AddWithValue("@change", 0.0);
                cmdMain.Parameters.AddWithValue("@isSpecial", 1);

                if (MainID == 0) // Insert and get the new MainID
                {
                    MainID = Convert.ToInt32(cmdMain.ExecuteScalar());
                }
                else // Update existing order
                {
                    cmdMain.ExecuteNonQuery();
                }

                // Loop through all rows in DataGridView to insert or update details
                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {
                    int detailID = Convert.ToInt32(row.Cells["dgvId"].Value);

                    if (detailID == 0)
                    {
                        qryDetail = @"INSERT INTO Details_tbl (MainID, proID, qty, price, amount)
                              VALUES (@MainID, @proID, @qty, @price, @amount);";
                    }
                    else
                    {
                        qryDetail = @"UPDATE Details_tbl 
                              SET proID = @proID, qty = @qty, price = @price, amount = @amount 
                              WHERE DetailID = @ID;";
                    }

                    SqlCommand cmdDetail = new SqlCommand(qryDetail, con);
                    cmdDetail.Parameters.AddWithValue("@ID", detailID);
                    cmdDetail.Parameters.AddWithValue("@MainID", MainID);
                    cmdDetail.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproID"].Value));
                    cmdDetail.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvQty"].Value));
                    cmdDetail.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvPrice"].Value));
                    cmdDetail.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));

                    cmdDetail.ExecuteNonQuery();
                }

                // Show success message and clear the form
                guna2MessageDialog1.Show("Order Saved Successfully!");
                ResetForm();
            }
            catch (Exception ex)
            {
                guna2MessageDialog1.Show($"Error: {ex.Message}");
            }
        }




        private void ResetForm()
        {
            // Reset the form to its initial state
            MainID = 0;
            detaildID = 0;
            lblTotal.Text = "";
            guna2DataGridView1.Rows.Clear();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "MM/dd/yyyy";


        }

        private void ClearDateTimePicker()
        {
            dateTimePicker1.CustomFormat = " "; // Reset to blank format
        }

        private void btnNew_Click_1(object sender, EventArgs e)
        {
            MainID = 0;
            detaildID = 0;
            lblTotal.Text = "";
            guna2DataGridView1.Rows.Clear();

            // Clear DateTimePicker
            ClearDateTimePicker();
        }
    }
}
