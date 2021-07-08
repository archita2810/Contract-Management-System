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
    public partial class Form_Dashboard : Form
    {
        int PanelWidth;
        bool isCollapsed;

        public Form_Dashboard()
        {
            InitializeComponent();
            PanelWidth = panelLeft.Width;
            isCollapsed = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                panelLeft.Width = panelLeft.Width + 10;
                if (panelLeft.Width >= PanelWidth)
                {
                    timer1.Stop();
                    isCollapsed = false;
                    this.Refresh();
                }
            }
            else
            {
                panelLeft.Width = panelLeft.Width - 10;
                if (panelLeft.Width <= 59)
                {
                    timer1.Stop();
                    isCollapsed = true;
                    this.Refresh();
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnDashboard);
            UserControls.UC_Dashboard ud = new UserControls.UC_Dashboard();
            addControls(ud);
        }

        private void addControls(UserControl uc)
        {
            panelControls.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelControls.Controls.Add(uc);
            uc.BringToFront();
        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnManage);
            UserControls.UC_AssignedWorks uw = new UserControls.UC_AssignedWorks();
            addControls(uw);
        }

        private void btnContractors_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnContractors);
            UserControls.UC_Contractors uc = new UserControls.UC_Contractors();
            addControls(uc);
        }

        private void btnAssigned_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnAssigned);
            UserControls.UC_Works uw = new UserControls.UC_Works();
            addControls(uw);

        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnReports);
            UserControls.UC_Reports uw = new UserControls.UC_Reports();
            addControls(uw);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnSettings);
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnAnalytics);
            UserControls.UC_Analytics uw = new UserControls.UC_Analytics();
            addControls(uw);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        /*
        private void timerTimer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            labelTime.Text = dt.ToString("HH:MM:SS");
        }*/
    }
}
