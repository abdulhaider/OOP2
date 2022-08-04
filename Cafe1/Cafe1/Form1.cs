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
    public partial class Form1 : Form
    {
        DataAccess da = new DataAccess();
        public Form1()
        {
            InitializeComponent();
        }

        private void blogin_Click(object sender, EventArgs e)
        {
            var ds = da.ExecuteQuery("select * from UserInfo where name ='" + this.txtusername.Text + "' and password = '" + this.txtpassword.Text + "';");
            if (ds.Tables[0].Rows.Count == 1)
            {
                

                string userName = ds.Tables[0].Rows[0][1].ToString();

                if (ds.Tables[0].Rows[0][4].ToString() == "Manager")
                {
                    welcome wl = new welcome();
                    this.Hide();
                    wl.Show();
                }
                else if (ds.Tables[0].Rows[0][4].ToString() == "Sales")
                {
                    Sales sl = new Sales();
                    this.Hide();
                    sl.Show();
                }
            }
            else
            {
                MessageBox.Show("Login Denied");
            }
        }
        }
    }

