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
    public partial class UC_Works : UserControl
    {
        SLRDbConnector.DbConnector db;
        public UC_Works()
        {
            InitializeComponent();
            db = new SLRDbConnector.DbConnector();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Forms.Form_AddWork fw = new Forms.Form_AddWork())
            {
                fw.ShowDialog();
                this.OnLoad(e);
            }
        }

        private void UC_Works_Load(object sender, EventArgs e)
        {
            db.fillDataGridView("select * from tblWorks where is_assigned = 0",dataGridView1);
            label3.Text = dataGridView1.Rows.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (workId != null)
            {
                using (Forms.Form_AssignWork fw = new Forms.Form_AssignWork())
                {
                    fw.workId = workId;
                    fw.workTitle = title;
                    fw.TsAmount = TsAmount;
                    fw.ShowDialog();
                    this.OnLoad(e);
                }
            }
        }

        private string workId, title, TsAmount;

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string query = "";
            if (cmbSearchBy.SelectedItem.ToString().Equals("Title"))
            {
                query = "select * from tblWorks where title = '" + txtSearch.Text + "'";
            }
            else if (cmbSearchBy.SelectedItem.ToString().Equals("Location"))
            {
                query = "select * from tblWorks where location like '%" + txtSearch.Text + "%'";
            }
           

            db.fillDataGridView(query, dataGridView1);

            if (txtSearch.Text == "")
            {
                this.OnLoad(e);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow item in dataGridView1.SelectedRows)
            {
                workId = item.Cells[0].Value.ToString();
                title = item.Cells[1].Value.ToString();
                TsAmount = item.Cells[4].Value.ToString();
            }
        }
    }
}
