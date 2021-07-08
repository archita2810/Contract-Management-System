using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContractManagementSystem
{
    public partial class Form1 : Form
    {
        SLRDbConnector.DbConnector db;

        public Form1()
        {
            InitializeComponent();
            db = new SLRDbConnector.DbConnector();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (isFormValid())
            {
                if (checkLogin())
                {
                    using (Forms.Form_Dashboard fd = new Forms.Form_Dashboard())
                    {
                        fd.ShowDialog();
                    }
                }
            }
        }

        private bool checkLogin()
        {
            string username = db.getSingleValue("select UserName from tblUsers where UserName = '"+txtUserName.Text+"' and Password = '"+txtPassword.Text+"'",out username, 0);
            if (username == null)
            {
                MessageBox.Show("User Name or Password is Incorrect","Incorrect Details",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }else
            {
                return true;
            }
        }

        private bool isFormValid()
        {
            if (txtUserName.Text.ToString().Trim() == string.Empty || txtPassword.Text.ToString().Trim() == string.Empty)
            {
                MessageBox.Show("Required Fields are Empty","Please Fill All Required Fields..!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }else
            {
                return true;
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender,e);
            }
        }
    }
}
