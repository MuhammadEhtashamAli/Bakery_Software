using Microsoft.Reporting.WinForms;
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

namespace Bakery_Software.Report
{
    public partial class PrintInvoiceForm : Form
    {
        string _invoice_id;
        public PrintInvoiceForm(string _id)
        {
            _invoice_id = _id;

            InitializeComponent();
        }

        private void PrintInvoiceForm_Load(object sender, EventArgs e)
        {
            string ConnectionString = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=AB_Inventory_DB; Data Source=DESKTOP-ILCCKNC\\SQLEXPRESS";
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select * from View_All_Bill_Test where InvoiceID = '" + _invoice_id + "'", con);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, "DataTable_Invoice");

            ReportDataSource datasource = new ReportDataSource("DataSet_Report", ds.Tables[0]);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(datasource);
            this.reportViewer1.RefreshReport();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ConnectionString = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=AB_Inventory_DB; Data Source=DESKTOP-ILCCKNC\\SQLEXPRESS";
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlDataAdapter da = new SqlDataAdapter("select * from View_All_Bill_Test where InvoiceID = '" + txt_Invoice_ID.Text + "'", con);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, "DataTable_Invoice");
            
            ReportDataSource datasource = new ReportDataSource("DataSet_Report", ds.Tables[0]);

            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(datasource);
            this.reportViewer1.RefreshReport();
        }
    }
}
