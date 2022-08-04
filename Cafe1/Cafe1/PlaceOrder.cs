using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe1
{
    public partial class PlaceOrder : Form
    {
        private DataAccess Da { get; set; }
        public PlaceOrder()
        {
            InitializeComponent();
            this.Da = new DataAccess();

            this.PopulateGridView();
        }

        private void PopulateGridView(string sql = "select * from items;")
        {
            var ds = this.Da.ExecuteQuery(sql);

            this.dgvPlaceO.AutoGenerateColumns = false;
            this.dgvPlaceO.DataSource = ds.Tables[0];
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sql = "select * from items where category = '" + this.txtsearch.Text + "';";
            this.PopulateGridView(sql);
        }

        private void txtQuantityUpDown_ValueChanged(object sender, EventArgs e)
        {
            Int64 quan = Int64.Parse(txtQuantityUpDown.Value.ToString());
            Int64 price = Int64.Parse(txtprice.Text);
            txttotal.Text = (quan * price).ToString();
        }

        protected int n, total = 0;
        int amount;

       

        private void dgvCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                amount = int.Parse(dgvCart.Rows[e.RowIndex].Cells[3].Value.ToString());
            }
            catch { }
        }

        private void btnremovecart_Click(object sender, EventArgs e)
        {
            try
            {
                dgvCart.Rows.RemoveAt(this.dgvCart.SelectedRows[0].Index);
            }
            catch { }
            total -= amount;
            labeltotalamount.Text = "Tk. " + total;
        }

        private void baddCart_Click(object sender, EventArgs e)
        {
            if (txttotal.Text != "0" && txttotal.Text != "")
            {
                n = dgvCart.Rows.Add();
                dgvCart.Rows[n].Cells[0].Value = txtItemName.Text;
                dgvCart.Rows[n].Cells[1].Value = txtprice.Text;
                dgvCart.Rows[n].Cells[2].Value = txtQuantityUpDown.Text;
                dgvCart.Rows[n].Cells[3].Value = txttotal.Text;

                total = total + int.Parse(txttotal.Text);
                labeltotalamount.Text = "Tk. " + total;
            }
            else
            {
                MessageBox.Show("Minimum quantity needs to be 1");
            }

            clearAll();
        }

        private void btnbill_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Total amount payable is " + total);
            clearAll();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 fs = new Form1();
            this.Hide();
            fs.Show();
        }

        private void CatLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Categories are: Drinks, Bakery and Fast Food.");
        }

        public void clearAll()
        {
            this.txtItemName.Clear();
            this.txtprice.Clear();
            this.txttotal.Clear();
            this.txtsearch.Clear();
           

        }



    }
}
