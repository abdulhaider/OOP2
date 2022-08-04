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
    public partial class dashboard : Form
    {

        private DataAccess Da { get; set; }
        public dashboard()
        {
            InitializeComponent();
            this.Da = new DataAccess();

            this.PopulateGridView();
        }

        private void PopulateGridView(string sql = "select * from items;")
        {
            var ds = this.Da.ExecuteQuery(sql);

            this.dgvItems.AutoGenerateColumns = false;
            this.dgvItems.DataSource = ds.Tables[0];
        }

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
            this.PopulateGridView();
        }

        private void txtAutoSearch_TextChanged(object sender, EventArgs e)
        {
            var sql = "select * from items where name like '" + this.txtAutoSearch.Text + "%';";
            this.PopulateGridView(sql);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var sql = @"insert into items values('" + this.txtId.Text + "', '" + this.txtName.Text + "', '" + this.cmbCategory.Text + "','" + this.txtPrice.Text + "');";
                var rowCount = this.Da.ExecuteDMLQuery(sql);

                if (rowCount == 1)
                {
                    MessageBox.Show("Item added successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to add item.");
                }
                this.PopulateGridView();
            }
            catch (Exception exc)
            {
                MessageBox.Show("An error has occured, please try again later. Error msg: " + exc.Message);
            }

            clearAll();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 fs = new Form1();
            this.Hide();
            fs.Show();
        }

        public void clearAll()
        {
            this.txtId.Clear();
            this.txtName.Clear();
            this.txtPrice.Clear();
            this.txtAutoSearch.Clear();
            this.cmbCategory.SelectedIndex = -1;


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var query = "select * from items where Iid = '" + this.txtId.Text + "';";
                var ds = this.Da.ExecuteQuery(query);

                if (ds.Tables[0].Rows.Count == 1)
                {
                    //update
                    var sql = @"update items
                                set name = '" + this.txtName.Text + @"',
                                category = '" + this.cmbCategory.Text + @"',
                                price = '" + this.txtPrice.Text + @"',
                                where Iid = '" + this.txtId.Text + "';";
                    int count = this.Da.ExecuteDMLQuery(sql);

                    if (count == 1)
                        MessageBox.Show("Data updated successfully");
                    else
                        MessageBox.Show("Data updating failed");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("An error has occured: " + exc.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                var id = this.dgvItems.CurrentRow.Cells[0].Value.ToString();
                var name = this.dgvItems.CurrentRow.Cells[1].Value.ToString();

                var sql = "delete from items where Iid = '" + id + "';";
                int count = this.Da.ExecuteDMLQuery(sql);

                if (count == 1)
                {
                    MessageBox.Show(name + " has been deleted successfully");
                    
                }


                else
                    MessageBox.Show("Data deletion failed");

                
                this.PopulateGridView();

            }
            catch (Exception exc)
            {
                MessageBox.Show("An error has occured: " + exc.Message);
            }
        }


    }
}

