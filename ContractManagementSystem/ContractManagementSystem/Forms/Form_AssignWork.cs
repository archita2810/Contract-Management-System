using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContractManagementSystem.Forms
{ 
    public partial class Form_AssignWork : Form
    {

        public string workId { get; set; }
        public string workTitle { get; set; }
        public string TsAmount { get; set; }


        SLRDbConnector.DbConnector db;


        public Form_AssignWork()
        {
            InitializeComponent();
            db = new SLRDbConnector.DbConnector();
        }

        private void cmbContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string address;
            db.getSingleValue("select address from tblContractors where full_name = '"+cmbContractor.SelectedItem.ToString()+"'",out address,0);
            txtAddress.Text = address;
        }

        private void Form_AssignWork_Load(object sender, EventArgs e)
        {
            db.FillCombobox("select full_name from tblContractors",cmbContractor);
            txtWorkTitle.Text = workTitle;
            txtTsAmount.Text = TsAmount;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string ContractorId;
        private void cmbContractor_Leave(object sender, EventArgs e)
        {
            db.getSingleValue("select id from tblContractors where full_name = '"+cmbContractor.Text+"' and address = '"+txtAddress.Text+"'",out ContractorId,0);
            if (ContractorId == null)
            {
                MessageBox.Show("Contractor Not Found.. Please add a Contractor First","Contractor Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isFormValid())
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to Assign this Work?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                   
                    db.performCRUD("insert into tblWorkAssigned(work_id,contractor_id,ca_cost,assigned_date,year) values('"+workId+"','"+ContractorId+ "','" + txtCACost.Text + "','" + this.txtAssignDate.Value.ToString("MM-dd-yyyy") + "','" + cmbYear.Text + "')");
                    
                 

                    string workAssignId;
                    db.getSingleValue("select max(id) from tblWorkAssigned",out workAssignId,0);

                    db.performCRUD("insert into tblCalculation(work_assign_id,work_done_amount,payable_amount,workToBeDone,paid_amount) values('"+workAssignId+ "',0,0,'" + txtCACost.Text + "',0)");
                   

                    db.performCRUD("update tblWorks set is_assigned = 1 where id = '"+workId+"'");

                    MessageBox.Show("Work Assigned Successfully.. :)","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);

                    this.Dispose();
                }
            }
        }

        private bool isFormValid()
        {
            if (ContractorId == null)
            {
                MessageBox.Show("Contractor Not Found.. Please add a Contractor First", "Contractor Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (cmbContractor.Text.Trim() == string.Empty || cmbYear.Text.Trim() == string.Empty || txtCACost.Text.Trim() == string.Empty || txtAssignDate.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill All Required Fields", "Required Fields are Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
                return true;
        }

        private void txtAddress_Click(object sender, EventArgs e)
        {

        }
    }
}
