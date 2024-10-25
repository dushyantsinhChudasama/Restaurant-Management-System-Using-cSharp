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

namespace Restaurant_Management_System.Models
{
    public partial class frmCheckout : Form
    {
        public frmCheckout()
        {
            InitializeComponent();
        }

        public frmCheckout(int mainID)
        {
            InitializeComponent();
            MainID = mainID; // Store the MainID
        }

        private void frmCheckout_Load(object sender, EventArgs e)
        {
            LoadOrderDetails();
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


        private void txtRevieved_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double reciept = 0;
            double change = 0;

            double.TryParse(txtAmount.Text, out amt);
            double.TryParse(txtRevieved.Text, out reciept);

            change = Math.Abs(amt - reciept); //thos is to convert some value if it is negttive to positive

            txtChange.Text = change.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            connect();

            string qry = @"UPDATE tblMain_tbl 
                   SET total = @total, 
                       recieved = @received, 
                       change = @change, 
                       status = 'Paid' 
                   WHERE MainID = @MainID";

            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@total", txtAmount.Text);
            cmd.Parameters.AddWithValue("@received", txtRevieved.Text);
            cmd.Parameters.AddWithValue("@change", txtChange.Text);
            cmd.Parameters.AddWithValue("@MainID", MainID); // Use the passed MainID

            cmd.ExecuteNonQuery();

            frmCustName frm = new frmCustName(MainID);
            frm.ShowDialog();

            //AddMessageBox.Show("Order paid successfully!");
            this.Close(); // Close the checkout form

         
            LoadOrderDetails();

           //Code of cyrsral report for billing here

        }

        private void LoadOrderDetails()
        {
            connect();

            // Fetch order details using the passed MainID
            string qry = "SELECT * FROM tblMain_tbl WHERE MainID = @MainID";
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@MainID", MainID);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                txtAmount.Text = reader["total"].ToString();
            }
            reader.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
