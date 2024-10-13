using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows.Forms;

namespace Restaurant_Management_System
{
    //Checking User Authenntication
    class MainClass
    {
       public static String s = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\D.K CHUDASAMA\source\repos\Restaurant Management System\Restaurant Management System\RMS.mdf;Integrated Security=True";

       public static SqlConnection con = new SqlConnection(s);


        public static bool isValidUser(string user, string pass)
        {
            bool isvalid = false;

            string qry = "select * from users where userName = '"+user+"' and password = '"+pass+"'";
            SqlCommand cmd = new SqlCommand(qry, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if(dt.Rows.Count > 0)
            {
                isvalid = true;
            }

            return isvalid;
        }


        //for loading data from datbase

        public static void LoadData(string qry, DataGridView gv, ListBox lb)
        {

            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                for(int i = 0; i < lb.Items.Count; i++)
                {
                    string colName1 = ((DataGridViewColumn)lb.Items[i]).Name;

                    gv.Columns[colName1].DataPropertyName = dt.Columns[i].ToString();

                }

                gv.DataSource = dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("The error is " + e.ToString());
                con.Close();
            }
        }

        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;

            int count = 0;

            foreach(DataGridViewRow row in gv.Rows)
            {
                count++;

                row.Cells[0].Value = count;
            }
        }

        //for combo box fill

        public static void CBFill(string qry, ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }

        public static void CBFillForSuppliers(string qry, ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(qry, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cb.DisplayMember = "sname";  // Supplier name column
            cb.ValueMember = "sid";      // Supplier ID column
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        }
    }
}
