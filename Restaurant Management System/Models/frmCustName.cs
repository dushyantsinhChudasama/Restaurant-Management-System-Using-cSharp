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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;


namespace Restaurant_Management_System.Models
{
    public partial class frmCustName : Form
    {
        public frmCustName(int mainID)
        {
            InitializeComponent();
            MainID = mainID; // Set MainID for this form
            GetAmount();
        }

        public static String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\RMS.mdf;Integrated Security=True";
        public static SqlConnection con;

        public int id = 0; // This will track whether we are inserting or updating

        void connect()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        public double amt;
        public int MainID = 0;

        private void frmCustName_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        static string Crypath = "";


        private void btnSave_Click(object sender, EventArgs e)
        {
            connect();

            if(string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                AddMessageBox.Show("Please Enter Customer Name");
            }

            string qry = @"UPDATE tblMain_tbl 
                   SET customer_name = @customer_name 
                   WHERE MainID = @MainID";

            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@customer_name", txtCustomerName.Text);
            cmd.Parameters.AddWithValue("@MainID", MainID); // Use the passed MainID

            cmd.ExecuteNonQuery();

            AddMessageBox.Show("Order Paid Successfully");
            this.Close();

            //printing 
            frmReciept frm = new frmReciept(MainID);
            frm.Show();

        }

        private void GetAmount()
        {
            connect();

            // Corrected SQL query with parameterized query
            string qry = "SELECT total FROM tblMain_tbl WHERE MainID = @MainID";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@MainID", MainID);

            // ExecuteScalar to retrieve a single value
            object result = cmd.ExecuteScalar();

            if (result != null && double.TryParse(result.ToString(), out double amt))
            {
                lblTotal.Text = amt.ToString("F2"); // Format to two decimal places
            }
            else
            {
                lblTotal.Text = "0.00"; // Set default if no result found
            }
        }

    }
}
