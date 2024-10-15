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

namespace Restaurant_Management_System
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
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

        private void RevenuePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExpensePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Today")
            {
                try
                {
                    // Store today's date in a variable
                    string todayDate = DateTime.Today.ToString("yyyy-MM-dd");

                    // Connect to the database
                    connect();

                    // Query to get the sum of the total column for today's date
                    string query = "SELECT SUM(total) FROM tblMain_tbl WHERE CONVERT(date, aDate) = '" + todayDate + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    double totalSum = 0;

                    if (reader.Read() && reader[0] != DBNull.Value)
                    {
                        totalSum = Convert.ToDouble(reader[0]);
                    }

                    reader.Close(); // Close the reader after use

                    // Assign the total value directly to the label
                    lblTotal.Text = totalSum.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    }
}
