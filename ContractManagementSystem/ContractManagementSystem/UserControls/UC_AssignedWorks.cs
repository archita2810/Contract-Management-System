using ContractManagementSystem.Forms;
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
    public partial class UC_AssignedWorks : UserControl
    {
        SLRDbConnector.DbConnector db;
        public UC_AssignedWorks()
        {
            InitializeComponent();
            db = new SLRDbConnector.DbConnector();
        }

        private void UC_AssignedWorks_Load(object sender, EventArgs e)
        {
            db.fillDataGridView("select wa.id,w.title as 'Work Title', c.full_name as 'Contractor Name', wa.ca_cost as 'CA Cost', wa.assigned_date as 'Assigned Date',wa.year from tblWorkAssigned as wa inner join tblWorks as w on wa.work_id = w.id inner join tblContractors as c ON wa.contractor_id = c.id where w.is_assigned = 1 and wa.is_completed = 0", dataGridView1);
            label3.Text = dataGridView1.Rows.Count.ToString();
        }

        string workAssignId, title, contractorName, CACost;

        private void button3_Click(object sender, EventArgs e)
        {
             db.fillDataGridView("select wa.id,w.title as 'Work Title', c.full_name as 'Contractor Name', wa.ca_cost as 'CA Cost', wa.assigned_date as 'Assigned Date',wa.year from tblWorkAssigned as wa inner join tblWorks as w on wa.work_id = w.id inner join tblContractors as c ON wa.contractor_id = c.id where w.is_assigned = 1 and wa.is_completed = 1", dataGridView1);
            label3.Text = dataGridView1.Rows.Count.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string query = "";
            if (comboBox1.SelectedItem.ToString().Equals("Work title"))
            {
                query = "select wa.id,w.title as 'Work Title', c.full_name as 'Contractor Name', wa.ca_cost as 'CA Cost', wa.assigned_date as 'Assigned Date',wa.year from tblWorkAssigned as wa inner join tblWorks as w on wa.work_id = w.id inner join tblContractors as c ON wa.contractor_id = c.id where w.title like '%" + txtSearch.Text + "%'";
            }
            else if (comboBox1.SelectedItem.ToString().Equals("Contractor name"))
            {
                query = "select wa.id,w.title as 'Work Title', c.full_name as 'Contractor Name', wa.ca_cost as 'CA Cost', wa.assigned_date as 'Assigned Date',wa.year from tblWorkAssigned as wa inner join tblWorks as w on wa.work_id = w.id inner join tblContractors as c ON wa.contractor_id = c.id where c.full_name like '%" + txtSearch.Text + "%'";

            }

            db.fillDataGridView(query, dataGridView1);

            if (txtSearch.Text == "")
            {
                this.OnLoad(e);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (workAssignId != null)
            {
                using (Forms.Form_AddWorkDone plz = new Forms.Form_AddWorkDone())
                {
                    plz.WorkTitle = title;
                    plz.ContractorName = contractorName;
                    plz.CACost = CACost;
                    plz.WorkAssignId = workAssignId;
                    plz.ShowDialog();
                    this.OnLoad(e);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                workAssignId = item.Cells[0].Value.ToString();
                title = item.Cells[1].Value.ToString();
                contractorName = item.Cells[2].Value.ToString();
                CACost = item.Cells[3].Value.ToString();
            }
        }
    }
}
