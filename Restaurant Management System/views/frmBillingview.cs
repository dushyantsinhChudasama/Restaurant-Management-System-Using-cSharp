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

namespace Restaurant_Management_System.views
{
    public partial class frmBillingview : Form
    {
        public frmBillingview()
        {
            InitializeComponent();
        }

        private void guna2Separator1_Click(object sender, EventArgs e)
        {

        }

        public static String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\RMS.mdf;Integrated Security=True";

        public static SqlConnection con;

        void connect()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        //private void btnAddOrder_Click(object sender, EventArgs e)
        //{
        //    frmPOS frm = new frmPOS();
        //    frm.ShowDialog();
        //}

        private void GetOrders()
        {
            connect();

            flowLayoutPanel1.Controls.Clear();

            string qry1 = @"select * from tblMain_tbl where status = 'Complete'";
            SqlCommand cmd1 = new SqlCommand(qry1, con);
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            da.Fill(dt1);

            FlowLayoutPanel p1;

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                p1 = new FlowLayoutPanel();
                p1.AutoSize = true;
                p1.Width = 230;
                p1.Height = 350;
                p1.FlowDirection = FlowDirection.TopDown;
                p1.BorderStyle = BorderStyle.FixedSingle;
                p1.Margin = new Padding(10, 10, 10, 10);

                FlowLayoutPanel p2 = new FlowLayoutPanel();
                p2 = new FlowLayoutPanel();
                p2.BackColor = Color.FromArgb(90, 87, 255);
                p2.AutoSize = true;
                p2.Width = 230;
                p2.Height = 125;
                p2.FlowDirection = FlowDirection.TopDown;
                p2.Margin = new Padding(0, 0, 0, 0);

                Label lb1 = new Label();
                lb1.ForeColor = Color.White;
                lb1.Margin = new Padding(10, 10, 3, 0);
                lb1.AutoSize = true;

                Label lb2 = new Label();
                lb2.ForeColor = Color.White;
                lb2.Margin = new Padding(10, 5, 3, 0);
                lb2.AutoSize = true;

                Label lb3 = new Label();
                lb3.ForeColor = Color.White;
                lb3.Margin = new Padding(10, 5, 3, 0);
                lb3.AutoSize = true;

                Label lb4 = new Label();
                lb4.ForeColor = Color.White;
                lb4.Margin = new Padding(10, 5, 3, 0);
                lb4.AutoSize = true;

                lb1.Text = "Bill N0 : " + dt1.Rows[i]["MainID"].ToString();
                lb2.Text = "Billing Date : " + dt1.Rows[i]["aDate"].ToString();
                lb3.Text = "Bill Time : " + dt1.Rows[i]["aTime"].ToString();
                lb4.Text = "Bill Type : " + dt1.Rows[i]["orderType"].ToString();

                p2.Controls.Add(lb1);
                p2.Controls.Add(lb2);
                p2.Controls.Add(lb3);
                p2.Controls.Add(lb4);


                //displaying uppwr portion like, blno, time, data type
                p1.Controls.Add(p2);

                //adding products

                int mid = 0;
                mid = Convert.ToInt32(dt1.Rows[i]["MainID"].ToString());

                string qry2 = @"select * from tblMain_tbl m
                                inner join Details_tbl d on m.MainID = d.MainID
                                inner join products_tbl p on p.pID = d.proID
                                where m.MainID = '" + mid + "'";

                SqlCommand cmd2 = new SqlCommand(qry2, con);
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);

                //displaying items

                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    Label lb5 = new Label();
                    lb5.ForeColor = Color.Black;
                    lb5.Margin = new Padding(10, 5, 3, 2);
                    lb5.AutoSize = true;

                    int no = j + 1;

                    lb5.Text = "" + no + " " + dt2.Rows[j]["pName"].ToString() + " " + dt2.Rows[j]["qty"].ToString();

                    p1.Controls.Add(lb5);
                }

                //Button to complete the order status
                Guna.UI2.WinForms.Guna2Button b1 = new Guna.UI2.WinForms.Guna2Button();
                b1.AutoRoundedCorners = true;
                b1.Size = new Size(170, 35);
                b1.FillColor = Color.FromArgb(241, 85, 126);
                b1.Margin = new Padding(20, 5, 3, 10);
                b1.Text = "Fast Cash";
                b1.Tag = dt1.Rows[i]["MainID"].ToString(); //to store the ID of Order

                b1.Click += new EventHandler(b_click);

                //button to delete or cancel order

                Guna.UI2.WinForms.Guna2Button b2 = new Guna.UI2.WinForms.Guna2Button();
                b2.AutoRoundedCorners = true;
                b2.Size = new Size(170, 35);
                b2.FillColor = Color.FromArgb(241, 85, 126);
                b2.Margin = new Padding(20, 5, 3, 10);
                b2.Text = "Check Out";
                b2.Tag = dt1.Rows[i]["MainID"].ToString(); //to store the ID of Order

                b2.Click += new EventHandler(b2_click);


                p1.Controls.Add(b1);
                p1.Controls.Add(b2);

                flowLayoutPanel1.Controls.Add(p1);
            }
        }

        private void b2_click(object sender, EventArgs e) // Check Out
        {
            Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;
            int mainID = Convert.ToInt32(btn.Tag); // Get MainID from the button's Tag

            // Pass the MainID to frmCheckout via constructor
            frmCheckout frm = new frmCheckout(mainID);
            frm.ShowDialog(); // Open the checkout form as a dialog
        }


        private void b_click(object sender, EventArgs e) //fastcash
        {
                Guna.UI2.WinForms.Guna2Button btn = (Guna.UI2.WinForms.Guna2Button)sender;
                int mainID = Convert.ToInt32(btn.Tag); // Retrieve MainID from the button's Tag

                connect();

                //Retrieve the total amount from the database for this MainID
                string qry = "SELECT total FROM tblMain_tbl WHERE MainID = @MainID";
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@MainID", mainID);

                double totalAmount = (double)cmd.ExecuteScalar(); 

                if (totalAmount == 0)
                {
                    MessageBox.Show("Error retrieving the total amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Step 2: Update the status to 'Paid' and set received amount to total
                string updateQuery = @"UPDATE tblMain_tbl SET recieved = @received, status = 'Paid' WHERE MainID = @MainID";

                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@received", totalAmount); // Received amount is equal to total
                updateCmd.Parameters.AddWithValue("@MainID", mainID);

                int rowsAffected = updateCmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    frmCustName frm = new frmCustName(mainID);
                    frm.ShowDialog(); // Open the customer name form as a dialog
                    GetOrders(); // Refresh the orders view after payment
                }
                else
                {
                    MessageBox.Show("Failed to update the order status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //add code for crystal report here
            

        }

        private void frmBillingview_Load(object sender, EventArgs e)
        {
            GetOrders();
        }
    }
}
