using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using CybersecurityChatbotGUI.Models;
using CybersecurityChatbotGUI.Services;

namespace CybersecurityChatbotGUI
{
    public partial class ChatbotForm
    {
        // Activity Log tab controls.
        private readonly TabPage tabActivity = new TabPage("Activity Log");
        private readonly ListBox lstActivity = new ListBox();
        private readonly Button btnRefreshActivity = new Button();
        private readonly Button btnShowMore = new Button();

        // Stores meaningful actions while Cybot is open.
        private readonly ActivityLogger activityLogger = new ActivityLogger();

        public void AddActivityTab()
        {
            tabActivity.BackColor = Color.FromArgb(35, 45, 60);

            TableLayoutPanel activityLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(20)
            };

            activityLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            activityLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            activityLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            Label lblActivityTitle = new Label
            {
                Dock = DockStyle.Fill,
                Text = "Recent Cybot Activity",
                ForeColor = Color.Cyan,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Displays the newest activity entries.
            lstActivity.Dock = DockStyle.Fill;
            lstActivity.Font = new Font("Segoe UI", 11);
            lstActivity.BackColor = Color.White;
            lstActivity.ForeColor = Color.Black;

            btnRefreshActivity.Text = "Refresh Recent 5";
            btnRefreshActivity.Width = 150;
            btnRefreshActivity.Height = 32;
            btnRefreshActivity.BackColor = Color.DodgerBlue;
            btnRefreshActivity.ForeColor = Color.White;
            btnRefreshActivity.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnRefreshActivity.Click += BtnRefreshActivity_Click;

            btnShowMore.Text = "Show More (10)";
            btnShowMore.Width = 150;
            btnShowMore.Height = 32;
            btnShowMore.BackColor = Color.DarkOrange;
            btnShowMore.ForeColor = Color.White;
            btnShowMore.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnShowMore.Click += BtnShowMore_Click;

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };

            buttonPanel.Controls.Add(btnRefreshActivity);
            buttonPanel.Controls.Add(btnShowMore);

            activityLayout.Controls.Add(lblActivityTitle, 0, 0);
            activityLayout.Controls.Add(lstActivity, 0, 1);
            activityLayout.Controls.Add(buttonPanel, 0, 2);

            tabActivity.Controls.Add(activityLayout);

            // Adds the tab beside Chat, Tasks, and Cyber Quiz.
            tabMain.TabPages.Add(tabActivity);
        }

        private void BtnRefreshActivity_Click(object? sender, EventArgs e)
        {
            DisplayRecentActivity(5);
        }

        private void BtnShowMore_Click(object? sender, EventArgs e)
        {
            DisplayRecentActivity(10);
        }

        public void DisplayRecentActivity(int count)
        {
            lstActivity.Items.Clear();

            List<ActivityEntry> recent =
                activityLogger.GetRecent(count);

            if (recent.Count == 0)
            {
                lstActivity.Items.Add("No activity recorded yet.");
                return;
            }

            foreach (ActivityEntry entry in recent)
            {
                lstActivity.Items.Add(entry.ToString());
            }
        }
    }
}