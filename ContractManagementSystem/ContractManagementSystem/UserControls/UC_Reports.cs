using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ContractManagementSystem.UserControls
{
    public partial class UC_Reports : UserControl
    {

        SLRDbConnector.DbConnector db;
     
        public UC_Reports()
        {
            InitializeComponent();
            db = new SLRDbConnector.DbConnector();
        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void UC_Reports_Load(object sender, EventArgs e)
        {
            db.FillCombobox("select full_name from tblContractors", comboBox1);
            //bindComboBox();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name;
            name = (this.comboBox1.SelectedItem.ToString());
            txtName.Text = name;
            string id = db.getSingleValue("Select id FROM tblContractors WHERE full_name = '" + name + "'", out id, 0);
            int cid = Int32.Parse(id);

            string assignId = db.getSingleValue("SELECT is_assigned FROM tblWorks JOIN tblWorkAssigned ON tblWorks.id = tblWorkAssigned.work_id AND contractor_id = '" + cid + "'", out assignId, 0);
            
            if (assignId == null)
            {
                txtlocation.Text = "Not Assigned";
                labeltask.Text = "Not Applicable";
                labeldate.Text = "Not Applicable";
                labelAmt.Text = "Not Applicable";
                labelStatus.Text = "Not Applicable";
            }
            else
            {
                string loc = db.getSingleValue("SELECT location FROM tblWorks JOIN tblWorkAssigned ON tblWorks.id = tblWorkAssigned.work_id AND contractor_id = '" + cid + "'", out loc, 0);
                txtlocation.Text = loc;

                string task = db.getSingleValue("SELECT title FROM tblWorks JOIN tblWorkAssigned ON tblWorks.id = tblWorkAssigned.work_id AND contractor_id = '" + cid + "'", out task, 0);
                labeltask.Text = task;

                string dt = db.getSingleValue("SELECT assigned_date FROM tblWorks JOIN tblWorkAssigned ON tblWorks.id = tblWorkAssigned.work_id AND contractor_id = '" + cid + "'", out dt, 0);
                DateTime dateTime10 = Convert.ToDateTime(dt);
                labeldate.Text = dateTime10.ToShortDateString();

                string amt = db.getSingleValue("SELECT ts_amount FROM tblWorks JOIN tblWorkAssigned ON tblWorks.id = tblWorkAssigned.work_id AND contractor_id = '" + cid + "'", out amt, 0);
                labelAmt.Text = amt;

                //string work_done_amount = db.getSingleValue("SELECT work_done_amount FROM tblCalculation JOIN tblWorkAssigned ON tblCalculation.work_assign_id = tblWorkAssigned.id AND contractor_id = '" + cid + "'", out work_done_amount, 0);
                string work_to_be_done = db.getSingleValue("SELECT workToBeDone FROM tblCalculation JOIN tblWorkAssigned ON tblCalculation.work_assign_id = tblWorkAssigned.id AND contractor_id = '" + cid + "'", out work_to_be_done, 0);

                if (Int32.Parse(work_to_be_done) == 0)
                    labelStatus.Text = "Completed";
                else
                    labelStatus.Text = "In process";
            }
        }
    }
}
