using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_System.Models
{
    public partial class frmBillList : Form
    {
        public frmBillList()
        {
            InitializeComponent();
        }

        private void frmBillList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public int MainID = 0;

        private void LoadData()
        {
            string qry = @"select MainID, status, orderType, total from tblMain_tbl where status = 'Pending'";
            ListBox lb = new ListBox();
            lb.Items.Add(dgvId);
            lb.Items.Add(dgvType);
            lb.Items.Add(dgvStatus);
            lb.Items.Add(dgvTotal);

            MainClass.LoadData(qry, guna2DataGridView1, lb);
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

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //connect();


            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvedit")
            {

                // Pass the category ID and name to frmCategoryAdd for editing
                MainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                this.Close();

            }
        }
    }
}