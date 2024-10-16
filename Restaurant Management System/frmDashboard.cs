using Restaurant_Management_System.views;
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
            comboBox1.SelectedIndex = 0;
            CalculateTotals();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "Today")
            {
                //for total sales
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

                //for total Expenses

                try
                {
                    string todayDate = DateTime.Today.ToString("yyyy-MM-dd");
                    connect();

                    // Query to get the sum of Price from inventory_tbl for today
                    string inventoryQuery = $"SELECT SUM(CAST(Price AS FLOAT)) FROM inventory_tbl WHERE CONVERT(date, dateAdded) = '{todayDate}'";
                    SqlCommand inventoryCmd = new SqlCommand(inventoryQuery, con);
                    var inventoryResult = inventoryCmd.ExecuteScalar();
                    double inventorySum = (inventoryResult == DBNull.Value) ? 0 : Convert.ToDouble(inventoryResult);

                    // Query to get the sum of sSalary from staff_tbl and divide by 30 for today's expenses
                    string staffQuery = "SELECT SUM(CAST(sSalary AS FLOAT)) FROM staff_tbl";
                    SqlCommand staffCmd = new SqlCommand(staffQuery, con);
                    var staffResult = staffCmd.ExecuteScalar();
                    double staffSum = (staffResult == DBNull.Value) ? 0 : Convert.ToDouble(staffResult) / 30;

                    // Calculate total expenses
                    double totalExpenses = inventorySum + staffSum;

                    lblExpense.Text = totalExpenses.ToString("N2"); // Display total expenses
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //for total Orders
                try
                {
                    // Store today's date in a variable
                    string todayDate = DateTime.Today.ToString("yyyy-MM-dd");

                    // Connect to the database
                    connect();

                    // Query to get the sum of the total column for today's date
                    string query = "SELECT COUNT(total) FROM tblMain_tbl WHERE CONVERT(date, aDate) = '" + todayDate + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    double totalSum = 0;

                    if (reader.Read() && reader[0] != DBNull.Value)
                    {
                        totalSum = Convert.ToDouble(reader[0]);
                    }

                    reader.Close(); // Close the reader after use

                    // Assign the total value directly to the label
                    lblOrders.Text = totalSum.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "This Week")
            {
                //for total sales of week
                try
                {
                    // Calculate start and end dates of the current week
                    DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(6);

                    connect(); // Connect to the database

                    // --- Total Sales for This Week ---
                    string salesQuery = $"SELECT SUM(total) FROM tblMain_tbl WHERE CONVERT(date, aDate) BETWEEN '{startOfWeek:yyyy-MM-dd}' AND '{endOfWeek:yyyy-MM-dd}'";
                    SqlCommand salesCmd = new SqlCommand(salesQuery, con);
                    SqlDataReader salesReader = salesCmd.ExecuteReader();

                    double totalSales = 0;
                    if (salesReader.Read() && salesReader[0] != DBNull.Value)
                    {
                        totalSales = Convert.ToDouble(salesReader[0]);
                    }
                    salesReader.Close(); // Close reader after use

                    lblTotal.Text = totalSales.ToString(); // Assign total sales to label


                    // --- Total Expenses for This Week ---
                    string expensesQuery = $"SELECT SUM(total) FROM tblMain_tbl WHERE CONVERT(date, aDate) BETWEEN '{startOfWeek:yyyy-MM-dd}' AND '{endOfWeek:yyyy-MM-dd}'";
                    SqlCommand expensesCmd = new SqlCommand(expensesQuery, con);
                    SqlDataReader expensesReader = expensesCmd.ExecuteReader();

                    double totalExpenses = 0;
                    if (expensesReader.Read() && expensesReader[0] != DBNull.Value)
                    {
                        totalExpenses = Convert.ToDouble(expensesReader[0]);
                    }
                    expensesReader.Close(); // Close reader after use

                    lblExpense.Text = totalExpenses.ToString(); // Assign total expenses to label


                    // --- Total Orders for This Week ---
                    string ordersQuery = $"SELECT COUNT(*) FROM tblMain_tbl WHERE CONVERT(date, aDate) BETWEEN '{startOfWeek:yyyy-MM-dd}' AND '{endOfWeek:yyyy-MM-dd}'";
                    SqlCommand ordersCmd = new SqlCommand(ordersQuery, con);
                    SqlDataReader ordersReader = ordersCmd.ExecuteReader();

                    double totalOrders = 0;
                    if (ordersReader.Read() && ordersReader[0] != DBNull.Value)
                    {
                        totalOrders = Convert.ToDouble(ordersReader[0]);
                    }
                    ordersReader.Close(); // Close reader after use

                    lblOrders.Text = totalOrders.ToString(); // Assign total orders to label
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //for total expenses of week
                try
                {
                    DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(6);
                    connect();

                    // Query to get the sum of Price from inventory_tbl for this week
                    string inventoryQuery = $"SELECT SUM(CAST(Price AS FLOAT)) FROM inventory_tbl WHERE CONVERT(date, dateAdded) BETWEEN '{startOfWeek:yyyy-MM-dd}' AND '{endOfWeek:yyyy-MM-dd}'";
                    SqlCommand inventoryCmd = new SqlCommand(inventoryQuery, con);
                    var inventoryResult = inventoryCmd.ExecuteScalar();
                    double inventorySum = (inventoryResult == DBNull.Value) ? 0 : Convert.ToDouble(inventoryResult);

                    // Query to get the sum of sSalary from staff_tbl and divide by 4 for weekly expenses
                    string staffQuery = "SELECT SUM(CAST(sSalary AS FLOAT)) FROM staff_tbl";
                    SqlCommand staffCmd = new SqlCommand(staffQuery, con);
                    var staffResult = staffCmd.ExecuteScalar();
                    double staffSum = (staffResult == DBNull.Value) ? 0 : Convert.ToDouble(staffResult) / 4;

                    // Calculate total expenses
                    double totalExpenses = inventorySum + staffSum;

                    lblExpense.Text = totalExpenses.ToString(); // Display total expenses
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //for total expenses of the week
                try
                {
                    // --- Orders for This Week ---
                    DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    DateTime endOfWeek = startOfWeek.AddDays(6);
                    connect();

                    string queryWeek = $"SELECT COUNT(total) FROM tblMain_tbl WHERE CONVERT(date, aDate) BETWEEN '{startOfWeek:yyyy-MM-dd}' AND '{endOfWeek:yyyy-MM-dd}'";
                    SqlCommand cmdWeek = new SqlCommand(queryWeek, con);
                    var resultWeek = cmdWeek.ExecuteScalar();

                    double totalOrdersWeek = (resultWeek == DBNull.Value) ? 0 : Convert.ToDouble(resultWeek);
                    lblOrders.Text = totalOrdersWeek.ToString(); // Display this week's orders
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }


            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "This Month")
            {
                //for total orders of the week
                try
                {
                    int year = DateTime.Today.Year;
                    int month = DateTime.Today.Month;

                    connect();
                    string query = "SELECT SUM(total) FROM tblMain_tbl WHERE YEAR(aDate) = " + year + " AND MONTH(aDate) = " + month;
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    double totalSum = 0;
                    if (reader.Read() && reader[0] != DBNull.Value)
                    {
                        totalSum = Convert.ToDouble(reader[0]);
                    }
                    reader.Close();

                    lblTotal.Text = totalSum.ToString(); // Directly assign value to label
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //for total expenses of the Month
                try
                {
                    int year = DateTime.Today.Year;
                    int month = DateTime.Today.Month;
                    connect();

                    // Query to get the sum of Price from inventory_tbl for this month
                    string inventoryQuery = $"SELECT SUM(CAST(Price AS FLOAT)) FROM inventory_tbl WHERE YEAR(dateAdded) = {year} AND MONTH(dateAdded) = {month}";
                    SqlCommand inventoryCmd = new SqlCommand(inventoryQuery, con);
                    var inventoryResult = inventoryCmd.ExecuteScalar();
                    double inventorySum = (inventoryResult == DBNull.Value) ? 0 : Convert.ToDouble(inventoryResult);

                    // Query to get the sum of sSalary from staff_tbl (no division for monthly expenses)
                    string staffQuery = "SELECT SUM(CAST(sSalary AS FLOAT)) FROM staff_tbl";
                    SqlCommand staffCmd = new SqlCommand(staffQuery, con);
                    var staffResult = staffCmd.ExecuteScalar();
                    double staffSum = (staffResult == DBNull.Value) ? 0 : Convert.ToDouble(staffResult);

                    // Calculate total expenses
                    double totalExpenses = inventorySum + staffSum;

                    lblExpense.Text = totalExpenses.ToString(); // Display total expenses
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //for total orders of the week
                try
                {
                    // --- Orders for This Month ---
                    int year = DateTime.Today.Year;
                    int month = DateTime.Today.Month;
                    connect();

                    string queryMonth = $"SELECT COUNT(total) FROM tblMain_tbl WHERE YEAR(aDate) = {year} AND MONTH(aDate) = {month}";
                    SqlCommand cmdMonth = new SqlCommand(queryMonth, con);
                    var resultMonth = cmdMonth.ExecuteScalar();

                    double totalOrdersMonth = (resultMonth == DBNull.Value) ? 0 : Convert.ToDouble(resultMonth);
                    lblOrders.Text = totalOrdersMonth.ToString(); // Display this month's orders
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void CalculateTotals()
        {
            //for total number of categories
            try
            {
                // Connect to the database
                connect();

                // Query to get the count of categories
                string query = "SELECT COUNT(catName) FROM category";

                SqlCommand cmd = new SqlCommand(query, con);
                double totalCategories = Convert.ToDouble(cmd.ExecuteScalar() ?? 0); // ExecuteScalar returns the first column of the first row or null

                // Assign the total value directly to the label for categories
                lblTotalCategories.Text = totalCategories.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //for total number of Menu Items
            try
            {
                // Connect to the database
                connect();

                // Query to get the count of menu items
                string query = "SELECT COUNT(pName) FROM products_tbl";

                SqlCommand cmd = new SqlCommand(query, con);
                double totalMenuItems = Convert.ToDouble(cmd.ExecuteScalar() ?? 0); // ExecuteScalar returns the first column of the first row or null

                // Assign the total value directly to the label for menu items
                lblTotalMenuItems.Text = totalMenuItems.ToString(); // Make sure you have lblTotalMenuItems
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //for total number of Staff Members
            try
            {
                // Connect to the database
                connect();

                // Query to get the count of staff members
                string query = "SELECT COUNT(sName) FROM staff_tbl";

                SqlCommand cmd = new SqlCommand(query, con);
                double totalStaffMembers = Convert.ToDouble(cmd.ExecuteScalar() ?? 0); // ExecuteScalar returns the first column of the first row or null

                // Assign the total value directly to the label for staff members
                lblTotalStaff.Text = totalStaffMembers.ToString(); // Make sure you have lblTotalStaffMembers
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
          
        }
    }
}
