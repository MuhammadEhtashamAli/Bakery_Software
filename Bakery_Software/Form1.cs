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

namespace Bakery_Software
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.Green;
            radioButton2.ForeColor = Color.Red;

            cmb_items.Items.Clear();
            cmb_items.Items.Add("BAISON LADDOS");
            cmb_items.Items.Add("BAISON PATISA");
            cmb_items.Items.Add("BADAM BARFI");
            cmb_items.Items.Add("BADAM HALWA");
            cmb_items.Items.Add("BARFEE");
            cmb_items.Items.Add("BLACK JAMAN");
            
          
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.Red;
            radioButton2.ForeColor = Color.Green;

            cmb_items.Items.Clear();
            cmb_items.Items.Add("Chips Potato plain");
            cmb_items.Items.Add("Chips Potato Plain Crincal");
            cmb_items.Items.Add("Daal Chana Spicy");
            cmb_items.Items.Add("Dhai Bondi Baison");
            cmb_items.Items.Add("Finger Chips Black Paper");
            cmb_items.Items.Add("Ghatia Special");
            cmb_items.Items.Add("Mix Nimco");
            
        }

        private void cmb_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_items.SelectedItem.ToString() == "BAISON LADDOS")
            { txt_price.Text = "500"; }
            else if (cmb_items.SelectedItem.ToString() == "BAISON PATISA")
            { txt_price.Text = "550"; }
            else if (cmb_items.SelectedItem.ToString() == "BADAM BARFI")
            { txt_price.Text = "700"; }
            else if (cmb_items.SelectedItem.ToString() == "BADAM HALWA")
            { txt_price.Text = "900"; }
            else if (cmb_items.SelectedItem.ToString() == "BARFEE")
            { txt_price.Text = "800"; }
            else if (cmb_items.SelectedItem.ToString() == "BLACK JAMAN")
            { txt_price.Text = "650"; }
            else if (cmb_items.SelectedItem.ToString() == "Chips Potato plain")
            { txt_price.Text = "300"; }
            else if (cmb_items.SelectedItem.ToString() == "Chips Potato Plain Crincal")
            { txt_price.Text = "320"; }
            else if (cmb_items.SelectedItem.ToString() == "Daal Chana Spicy")
            { txt_price.Text = "200"; }
            else if (cmb_items.SelectedItem.ToString() == "Dhai Bondi Baison")
            { txt_price.Text = "250"; }
            else if (cmb_items.SelectedItem.ToString() == "Finger Chips Black Paper")
            { txt_price.Text = "300"; }
            else if (cmb_items.SelectedItem.ToString() == "Ghatia Special")
            { txt_price.Text = "400"; }
            else if (cmb_items.SelectedItem.ToString() == "Mix Nimco")
            { txt_price.Text = "500"; }
           
            else
            { txt_price.Text = "0"; }


            txt_total.Text = "";
            txt_qty.Text = "";
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            if (txt_qty.Text.Length>0)
            {
                txt_total.Text = (Convert.ToInt16(txt_qty.Text) * Convert.ToInt16(txt_price.Text)).ToString();
            }
            }

        private void button1_Click(object sender, EventArgs e)
        {

            string[] arr = new string[4];
            arr[0] = cmb_items.SelectedItem.ToString();
            arr[1] = txt_price.Text;
            arr[2] = txt_qty.Text;
            arr[3] = txt_total.Text;

            ListViewItem lvi = new ListViewItem(arr);           
            listView1.Items.Add(lvi);


            txt_sub.Text = (Convert.ToInt16(txt_sub.Text) + Convert.ToInt16(txt_total.Text)).ToString();

        }

        private void button5_Click(object sender, EventArgs e)        {
          
            if (listView1.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView1.Items.Count ; i++)
                {
                    if (listView1.Items[i].Selected)
                    {
                        txt_sub.Text = (Convert.ToInt16(txt_sub.Text) - Convert.ToInt16(listView1.Items[i].SubItems[3].Text)).ToString();
                        listView1.Items[i].Remove();


                    }
                }
            }
        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            if (txt_discount.Text.Length > 0)
            {
                txt_net.Text = (Convert.ToInt16(txt_sub.Text) - Convert.ToInt16(txt_discount.Text)).ToString();
            }
        }

        private void txt_paid_TextChanged(object sender, EventArgs e)
        {
            if (txt_discount.Text.Length > 0)
            {
                txt_balance.Text = (Convert.ToInt16(txt_net.Text) - Convert.ToInt16(txt_paid.Text)).ToString();
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {

                try
                {
                    string ConnectionString = "Integrated Security=SSPI; Persist Security Info=False; Initial Catalog=AB_Inventory_DB; Data Source=DESKTOP-ILCCKNC\\SQLEXPRESS";

                    SqlConnection connection = new SqlConnection(ConnectionString);
                    SqlCommand command = connection.CreateCommand();

            connection.Open();

         command.CommandText = "Insert into Test_Invoice_Master (InvoiceDate, Sub_Total, Discount, Net_Amount, Paid_Amount) values " +
                       " ( getdate() , " + txt_sub.Text + " ," + txt_discount.Text + " , " + txt_net.Text + ", " + txt_paid.Text + ")  select scope_identity() ";

                    string InvoiceID = command.ExecuteScalar().ToString();

                    foreach (ListViewItem ListItem in listView1.Items)
                    {

                    command.CommandText = "Insert into Test_Invoice_Detail (MasterID, ItemName, ItemPrice, ItemQtty, ItemTotal ) values   " +
               " ('" + InvoiceID + "', '" + ListItem.SubItems[0].Text + "', '" + ListItem.SubItems[1].Text + "', '" + ListItem.SubItems[2].Text + "' , " + ListItem.SubItems[3].Text + ")";
                   
                        command.ExecuteNonQuery();

                    }
                    connection.Close();
                    MessageBox.Show("Sale Created Successfully, with Invoice # " + InvoiceID);
                    new Report.PrintInvoiceForm(InvoiceID).Show();
                  


                }
                catch (Exception ee)
                {               
                    MessageBox.Show("Sale Not Created, Error!");
                }

            }
            else
            {
                MessageBox.Show("Must Add an Item in the List");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //new Report.PrintInvoiceForm().Show();
        }
    }
}
