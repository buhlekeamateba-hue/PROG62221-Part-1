using System.Drawing;
using CybersecurityChatbotGUI.Data;
using CybersecurityChatbotGUI.Models;

namespace CybersecurityChatbotGUI
{
    public partial class ChatbotForm : Form
    {
        // Main navigation tabs.
        private readonly TabControl tabMain = new TabControl();
        private readonly TabPage tabChat = new TabPage("Chat");
        private readonly TabPage tabTasks = new TabPage("Tasks");

        // Existing Part 2 chat controls.
        private readonly RichTextBox rtbChatHistory = new RichTextBox();
        private readonly TextBox txtUserInput = new TextBox();
        private readonly Button btnSend = new Button();
        private readonly Label lblBranding = new Label();
        private readonly Label lblStatus = new Label();

        // Part 3 Task Assistant controls.
        private readonly TextBox txtTaskTitle = new TextBox();
        private readonly TextBox txtTaskDescription = new TextBox();
        private readonly CheckBox chkUseReminder = new CheckBox();
        private readonly DateTimePicker dtpReminder = new DateTimePicker();
        private readonly Button btnAddTask = new Button();
        private readonly Button btnCompleteTask = new Button();
        private readonly Button btnDeleteTask = new Button();
        private readonly Button btnRefreshTasks = new Button();
        private readonly DataGridView dgvTasks = new DataGridView();
        private readonly Label lblTaskStatus = new Label();

        // Existing chatbot logic, voice greeting, and MySQL task repository.
        private readonly ChatBot cybot = new ChatBot();
        private readonly VoiceGreeting voiceGreeting = new VoiceGreeting();
        private readonly TaskRepository taskRepository = new TaskRepository();

        public ChatbotForm()
        {
            InitializeComponent();

            // Creates the final Part 3 GUI layout.
            BuildInterface();
            AddQuizTab();
            AddActivityTab();

            // Connects the Part 2 delegate to the chat display method.
            cybot.ResponseHandler = DisplayBotMessage;

            // Runs when the form has opened.
            Shown += ChatbotForm_Shown;
        }

        private void ChatbotForm_Shown(object? sender, EventArgs e)
        {
            voiceGreeting.PlayGreeting();

            cybot.BeginConversation();

            LoadTasks();

            // Records that Cybot has started.
            activityLogger.Log("Cybot application started");
            DisplayRecentActivity(5);
        }

        private void BuildInterface()
        {
        Text = "Cybot - Cybersecurity Awareness Chatbot";
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(900, 650);
            Size = new Size(1100, 750);
            BackColor = Color.FromArgb(20, 30, 45);

            // Removes the empty default designer controls.
            Controls.Clear();

            TableLayoutPanel rootLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(15)
            };

            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 110));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

            // Cybot branding from Parts 1 and 2.
            lblBranding.Dock = DockStyle.Fill;
            lblBranding.TextAlign = ContentAlignment.MiddleCenter;
            lblBranding.ForeColor = Color.Cyan;
            lblBranding.Font = new Font("Consolas", 11, FontStyle.Bold);
            lblBranding.Text =
                "+--------------------------------------------------+\n" +
                "|       CYBOT - CYBERSECURITY AWARENESS BOT        |\n" +
                "|                  STAY SAFE ONLINE                |\n" +
                "+--------------------------------------------------+";

            // Main tab control contains Chat and Tasks.
            tabMain.Dock = DockStyle.Fill;
            tabMain.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            BuildChatTab();
            BuildTasksTab();

            tabMain.TabPages.Add(tabChat);
            tabMain.TabPages.Add(tabTasks);

            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Text = "Cybot: Your Cybersecurity Awareness Assistant";
            lblStatus.ForeColor = Color.LightGray;
            lblStatus.Font = new Font("Segoe UI", 9);
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            rootLayout.Controls.Add(lblBranding, 0, 0);
            rootLayout.Controls.Add(tabMain, 0, 1);
            rootLayout.Controls.Add(lblStatus, 0, 2);

            Controls.Add(rootLayout);
        }

        private void BuildChatTab()
        {
            tabChat.BackColor = Color.FromArgb(20, 30, 45);

            TableLayoutPanel chatLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(10)
            };

            chatLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            chatLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));

            // Chat history shows both user and Cybot messages.
            rtbChatHistory.Dock = DockStyle.Fill;
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.TabStop = false;
            rtbChatHistory.BackColor = Color.FromArgb(35, 45, 60);
            rtbChatHistory.ForeColor = Color.White;
            rtbChatHistory.Font = new Font("Segoe UI", 11);
            rtbChatHistory.BorderStyle = BorderStyle.FixedSingle;

            Panel inputPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            txtUserInput.Dock = DockStyle.Fill;
            txtUserInput.Font = new Font("Segoe UI", 11);
            txtUserInput.PlaceholderText = "Type a message for Cybot...";
            txtUserInput.KeyDown += TxtUserInput_KeyDown;

            btnSend.Dock = DockStyle.Right;
            btnSend.Width = 110;
            btnSend.Text = "Send";
            btnSend.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnSend.BackColor = Color.Cyan;
            btnSend.ForeColor = Color.Black;
            btnSend.Click += BtnSend_Click;

            inputPanel.Controls.Add(txtUserInput);
            inputPanel.Controls.Add(btnSend);

            chatLayout.Controls.Add(rtbChatHistory, 0, 0);
            chatLayout.Controls.Add(inputPanel, 0, 1);

            tabChat.Controls.Add(chatLayout);
        }

        private void BuildTasksTab()
        {
            tabTasks.BackColor = Color.FromArgb(35, 45, 60);

            TableLayoutPanel taskLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(15)
            };

            taskLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130));
            taskLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            taskLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            taskLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
            taskLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            taskLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            taskLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            taskLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

            Label lblTaskTitle = CreateTaskLabel("Task Title:");
            Label lblTaskDescription = CreateTaskLabel("Description:");
            Label lblReminder = CreateTaskLabel("Reminder:");

            txtTaskTitle.Dock = DockStyle.Fill;
            txtTaskTitle.Font = new Font("Segoe UI", 10);

            txtTaskDescription.Dock = DockStyle.Fill;
            txtTaskDescription.Multiline = true;
            txtTaskDescription.ScrollBars = ScrollBars.Vertical;
            txtTaskDescription.Font = new Font("Segoe UI", 10);

            chkUseReminder.Text = "Set reminder";
            chkUseReminder.ForeColor = Color.White;
            chkUseReminder.AutoSize = true;
            chkUseReminder.CheckedChanged += ChkUseReminder_CheckedChanged;

            dtpReminder.Format = DateTimePickerFormat.Custom;
            dtpReminder.CustomFormat = "dd MMM yyyy HH:mm";
            dtpReminder.Width = 210;
            dtpReminder.Enabled = false;
            dtpReminder.Value = DateTime.Now.AddDays(1);

            FlowLayoutPanel reminderPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };

            reminderPanel.Controls.Add(chkUseReminder);
            reminderPanel.Controls.Add(dtpReminder);

            // Task action buttons.
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };

            SetupTaskButton(btnAddTask, "Add Task", Color.MediumSeaGreen);
            SetupTaskButton(btnCompleteTask, "Complete Selected", Color.DodgerBlue);
            SetupTaskButton(btnDeleteTask, "Delete Selected", Color.IndianRed);
            SetupTaskButton(btnRefreshTasks, "Refresh Tasks", Color.DarkOrange);

            btnAddTask.Click += BtnAddTask_Click;
            btnCompleteTask.Click += BtnCompleteTask_Click;
            btnDeleteTask.Click += BtnDeleteTask_Click;
            btnRefreshTasks.Click += BtnRefreshTasks_Click;

            buttonPanel.Controls.Add(btnAddTask);
            buttonPanel.Controls.Add(btnCompleteTask);
            buttonPanel.Controls.Add(btnDeleteTask);
            buttonPanel.Controls.Add(btnRefreshTasks);

            // Shows all saved MySQL tasks.
            dgvTasks.Dock = DockStyle.Fill;
            dgvTasks.ReadOnly = true;
            dgvTasks.AllowUserToAddRows = false;
            dgvTasks.AllowUserToDeleteRows = false;
            dgvTasks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTasks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTasks.MultiSelect = false;
            dgvTasks.BackgroundColor = Color.White;
            dgvTasks.RowHeadersVisible = false;

            lblTaskStatus.Dock = DockStyle.Fill;
            lblTaskStatus.ForeColor = Color.LightGray;
            lblTaskStatus.Font = new Font("Segoe UI", 9);
            lblTaskStatus.TextAlign = ContentAlignment.MiddleLeft;

            taskLayout.Controls.Add(lblTaskTitle, 0, 0);
            taskLayout.Controls.Add(txtTaskTitle, 1, 0);
            taskLayout.Controls.Add(lblTaskDescription, 0, 1);
            taskLayout.Controls.Add(txtTaskDescription, 1, 1);
            taskLayout.Controls.Add(lblReminder, 0, 2);
            taskLayout.Controls.Add(reminderPanel, 1, 2);
            taskLayout.Controls.Add(buttonPanel, 0, 3);
            taskLayout.SetColumnSpan(buttonPanel, 2);
            taskLayout.Controls.Add(dgvTasks, 0, 4);
            taskLayout.SetColumnSpan(dgvTasks, 2);
            taskLayout.Controls.Add(lblTaskStatus, 0, 5);
            taskLayout.SetColumnSpan(lblTaskStatus, 2);

            tabTasks.Controls.Add(taskLayout);
        }

        private Label CreateTaskLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private void SetupTaskButton(Button button, string text, Color colour)
        {
            button.Text = text;
            button.AutoSize = true;
            button.Height = 32;
            button.BackColor = colour;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            button.FlatStyle = FlatStyle.Flat;
        }

        private void ChkUseReminder_CheckedChanged(object? sender, EventArgs e)
        {
            // Enables the date picker only when a reminder is wanted.
            dtpReminder.Enabled = chkUseReminder.Checked;
        }

        private void BtnAddTask_Click(object? sender, EventArgs e)
        {
            string title = txtTaskTitle.Text.Trim();
            string description = txtTaskDescription.Text.Trim();

            // Validates task details before saving to MySQL.
            if (string.IsNullOrWhiteSpace(title) ||
                string.IsNullOrWhiteSpace(description))
            {
                SetTaskStatus("Please enter both a task title and a description.", Color.Gold);
                return;
            }

            CyberTask task = new CyberTask
            {
                Title = title,
                Description = description,
                ReminderDate = chkUseReminder.Checked ? dtpReminder.Value : null,
                IsCompleted = false
            };

            try
            {
                int taskId = taskRepository.AddTask(task);
                activityLogger.Log("Task added: " + task.Title);

                if (task.ReminderDate.HasValue)
                {
                    activityLogger.Log(
                        "Reminder set: " + task.Title +
                        " - " + task.ReminderDate.Value.ToShortDateString());
                }

                DisplayRecentActivity(5);

                SetTaskStatus($"Task added successfully. MySQL task ID: {taskId}", Color.LightGreen);

                // Clears the form after a successful insert.
                txtTaskTitle.Clear();
                txtTaskDescription.Clear();
                chkUseReminder.Checked = false;

                LoadTasks();
            }
            catch (Exception ex)
            {
                SetTaskStatus("Could not save task: " + ex.Message, Color.Salmon);
            }
        }

        private void BtnCompleteTask_Click(object? sender, EventArgs e)
        {
            CyberTask? selectedTask = GetSelectedTask();

            if (selectedTask == null)
            {
                SetTaskStatus("Please select a task to mark as completed.", Color.Gold);
                return;
            }

            try
            {
                taskRepository.MarkTaskCompleted(selectedTask.TaskId);
                activityLogger.Log("Task completed: " + selectedTask.Title);
                DisplayRecentActivity(5);

                SetTaskStatus(
                    $"Task completed: {selectedTask.Title}",
                    Color.LightGreen);

                LoadTasks();
            }
            catch (Exception ex)
            {
                SetTaskStatus("Could not complete task: " + ex.Message, Color.Salmon);
            }
        }

        private void BtnDeleteTask_Click(object? sender, EventArgs e)
        {
            CyberTask? selectedTask = GetSelectedTask();

            if (selectedTask == null)
            {
                SetTaskStatus("Please select a task to delete.", Color.Gold);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Delete the task: {selectedTask.Title}?",
                "Delete Task",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                taskRepository.DeleteTask(selectedTask.TaskId);
                activityLogger.Log("Task deleted: " + selectedTask.Title);
                DisplayRecentActivity(5);

                SetTaskStatus(
                    $"Task deleted: {selectedTask.Title}",
                    Color.LightGreen);

                LoadTasks();
            }
            catch (Exception ex)
            {
                SetTaskStatus("Could not delete task: " + ex.Message, Color.Salmon);
            }
        }

        private void BtnRefreshTasks_Click(object? sender, EventArgs e)
        {
            LoadTasks();
        }

        private void LoadTasks()
        {
            try
            {
                // Reloads the grid from MySQL after every CRUD action.
                dgvTasks.DataSource = null;
                dgvTasks.DataSource = taskRepository.GetAllTasks();

                if (dgvTasks.Columns["TaskId"] != null)
                {
                    dgvTasks.Columns["TaskId"].Visible = false;
                }

                if (dgvTasks.Columns["Title"] != null)
                {
                    dgvTasks.Columns["Title"].HeaderText = "Task";
                }

                if (dgvTasks.Columns["Description"] != null)
                {
                    dgvTasks.Columns["Description"].HeaderText = "Description";
                }

                if (dgvTasks.Columns["ReminderDate"] != null)
                {
                    dgvTasks.Columns["ReminderDate"].HeaderText = "Reminder";
                }

                if (dgvTasks.Columns["IsCompleted"] != null)
                {
                    dgvTasks.Columns["IsCompleted"].HeaderText = "Completed";
                }

                SetTaskStatus("Tasks loaded from MySQL.", Color.LightGreen);
            }
            catch (Exception ex)
            {
                // The app stays open when MySQL is unavailable or credentials are wrong.
                SetTaskStatus("Could not load tasks: " + ex.Message, Color.Salmon);
            }
        }

        private CyberTask? GetSelectedTask()
        {
            if (dgvTasks.CurrentRow == null)
            {
                return null;
            }

            return dgvTasks.CurrentRow.DataBoundItem as CyberTask;
        }

        private void SetTaskStatus(string message, Color colour)
        {
            lblTaskStatus.Text = message;
            lblTaskStatus.ForeColor = colour;
        }

        private void BtnSend_Click(object? sender, EventArgs e)
        {
            SendUserMessage();
        }

        private void TxtUserInput_KeyDown(object? sender, KeyEventArgs e)
        {
            // Pressing Enter sends the message.
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendUserMessage();
            }
        }

        private void SendUserMessage()
        {
            string userMessage = txtUserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userMessage))
            {
                DisplayBotMessage("Please type a question or message first.");
                return;
            }

            // Shows the message in the chat history.
            DisplayUserMessage(userMessage);

            txtUserInput.Clear();
            txtUserInput.Focus();

            // Handles Part 3 commands before normal Part 2 chatbot questions.
            if (TryHandlePart3Command(userMessage))
            {
                return;
            }

            // Keeps all previous Part 2 cybersecurity chat features working.
            cybot.ProcessInput(userMessage);
        }

        private void DisplayUserMessage(string message)
        {
            rtbChatHistory.SelectionColor = Color.LightSkyBlue;
            rtbChatHistory.AppendText(
                $"You > {message}{Environment.NewLine}{Environment.NewLine}");

            rtbChatHistory.ScrollToCaret();
        }

        private void DisplayBotMessage(string message)
        {
            rtbChatHistory.SelectionColor = Color.LightGreen;
            rtbChatHistory.AppendText(
                $"Cybot > {message}{Environment.NewLine}{Environment.NewLine}");

            rtbChatHistory.ScrollToCaret();
        }
    }
}