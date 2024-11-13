using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Restaurant_Management_System.Models
{
    public partial class frmReciept : Form
    {
        public frmReciept(int mainID)
        {
            InitializeComponent();
            MainID = mainID; // Set MainID for this form
        }

        private CrystalDecisions.CrystalReports.Engine.ReportDocument cr = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

        static string Crypath = "";

        int MainID;

        public static String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\RMS.mdf;Integrated Security=True";
        public static SqlConnection con;

        void connect()
        {
            con = new SqlConnection(s);
            con.Open();
        }

        void Bill()
        {
            connect();

            SqlDataAdapter da;
            DataSet ds = new DataSet();
            string query = @"
                            SELECT 
                                m.*, 
                                d.qty, 
                                d.price, 
                                d.amount, 
                                p.pName AS product_name
                            FROM 
                                tblMain_tbl m
                            LEFT JOIN 
                                Details_tbl d ON m.MainID = d.MainID
                            LEFT JOIN 
                                products_tbl p ON d.proID = p.pID
                            WHERE 
                                m.MainID = @MainID";

            da = new SqlDataAdapter(query, con);
            da.SelectCommand.Parameters.AddWithValue("@MainID", MainID); // Use parameterized query
            da.Fill(ds);

            // Corrected XML file path
            string xml = @"C:/Users/D.K CHUDASAMA/source/repos/Restaurant Management System/Restaurant Management System/Reports/data.xml";
            ds.WriteXmlSchema(xml);

            // Corrected Crystal Report path
            Crypath = @"C:/Users/D.K CHUDASAMA/source/repos/Restaurant Management System/Restaurant Management System/Reports/rptBill.rpt";
            cr.Load(Crypath);
            cr.SetDataSource(ds);
            cr.Database.Tables[0].SetDataSource(ds);
            cr.Refresh();
            crystalReportViewer1.ReportSource = cr;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            Bill();
        }
    }
}
