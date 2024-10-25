using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Restaurant_Management_System.views
{
    public partial class frmReportView : Form
    {
        public frmReportView(string RptType)
        {
            InitializeComponent();
            Report = RptType;
            LoadReport(); // Display the report on form load
        }

        private ReportDocument cr = new ReportDocument();
        static string Crypath = "";
        string Report;

        public static string s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\RMS.mdf;Integrated Security=True";
        public static SqlConnection con;

        void connect()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        private void LoadReport()
        {
            connect();
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string query = "";
            string xmlPath = ""; // Variable to hold XML file path

            // Switch case to load different reports based on Report type
            switch (Report)
            {
                case "Category":
                    query = "SELECT * FROM category"; // Replace with actual category table name
                    Crypath = @"C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\Reports\rptCategory.rpt";
                    xmlPath = @"C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\Reports\CategoryData.xml";
                    break;

                case "Items":
                    query = "select pid, pName, pPrice, pImage, c.catName from products_tbl p inner join category c on c.catID = p.CategoryID"; // Replace with actual items table name
                    Crypath = @"C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\Reports\rptItems.rpt";
                    xmlPath = @"C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\Reports\ItemsDataImage.xml";
                    break;

                case "Staff":
                    query = "SELECT * FROM staff_tbl"; // Replace with actual staff table name
                    Crypath = @"C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\Reports\rptStaff.rpt";
                    xmlPath = @"C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\Reports\StaffData.xml";
                    break;

                case "Supplier":
                    query = "select sID, sName, sPhone, CategoryID, c.catName from supplier_tbl s inner join category c on c.catID = s.CategoryID"; // Replace with actual suppliers table name
                    Crypath = @"C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\Reports\rptSuppliers.rpt";
                    xmlPath = @"C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\Reports\SuppliersData.xml";
                    break;

                default:
                    MessageBox.Show("Invalid report type specified.");
                    return;
            }

            // Execute query and fill dataset
            da = new SqlDataAdapter(query, con);
            da.Fill(ds);

            // Generate XML schema and data file
            ds.WriteXmlSchema(xmlPath);

            // Load the report
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            crystalReportViewer1.ReportSource = cr;
            crystalReportViewer1.Refresh();

            // Close the database connection after filling the data
            con.Close();
        }

        private void frmReportView_FormClosing(object sender, FormClosingEventArgs e)
        {
            cr.Dispose(); // Dispose of the report document to release resources
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
