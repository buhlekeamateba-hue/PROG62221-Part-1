namespace CybersecurityChatbotGUI
{

        public partial class ChatbotForm : Form
        {
            // GUI controls.
            private readonly RichTextBox rtbChatHistory = new RichTextBox();
            private readonly TextBox txtUserInput = new TextBox();
            private readonly Button btnSend = new Button();
            private readonly Label lblBranding = new Label();
            private readonly Label lblStatus = new Label();

            // Main chatbot logic and Part 1 voice greeting.
            private readonly ChatBot cybot = new ChatBot();
            private readonly VoiceGreeting voiceGreeting = new VoiceGreeting();

            public ChatbotForm()
            {
                InitializeComponent();

                // Build the GUI layout using code.
                BuildInterface();

                // Connect Cybot's delegate to the method that displays replies.
                cybot.ResponseHandler = DisplayBotMessage;

                // Play the greeting after the form is visible.
                Shown += ChatbotForm_Shown;
            }

            private void ChatbotForm_Shown(object? sender, EventArgs e)
            {
                // Plays the WAV greeting from Assets/greeting.wav.
                voiceGreeting.PlayGreeting();

                // Starts Cybot's first GUI message.
                cybot.BeginConversation();
            }

            private void BuildInterface()
            {
                // Set up the main form.
                Text = "Cybot - Cybersecurity Awareness Chatbot";
                StartPosition = FormStartPosition.CenterScreen;
                MinimumSize = new Size(750, 600);
                Size = new Size(900, 700);
                BackColor = Color.FromArgb(20, 30, 45);

                // Remove the empty default controls created by the template.
                Controls.Clear();

                // Main layout has branding, chat history, typing area, and status.
                TableLayoutPanel layout = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 4,
                    Padding = new Padding(15)
                };

                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 125));
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                // ASCII-style branding that carries Part 1 identity into the GUI.
                lblBranding.Dock = DockStyle.Fill;
                lblBranding.TextAlign = ContentAlignment.MiddleCenter;
                lblBranding.ForeColor = Color.Cyan;
                lblBranding.Font = new Font("Consolas", 11, FontStyle.Bold);
                lblBranding.Text =
                "+--------------------------------------------------+\n" +
                "|       CYBOT - CYBERSECURITY AWARENESS BOT        |\n" +
                "|                  STAY SAFE ONLINE                |\n" +
                "+--------------------------------------------------+\n";

                // Chat history shows user and Cybot messages.
                rtbChatHistory.Dock = DockStyle.Fill;
                rtbChatHistory.ReadOnly = true;
                rtbChatHistory.TabStop = false;
                rtbChatHistory.BackColor = Color.FromArgb(35, 45, 60);
                rtbChatHistory.ForeColor = Color.White;
                rtbChatHistory.Font = new Font("Segoe UI", 11);
                rtbChatHistory.BorderStyle = BorderStyle.FixedSingle;

                // Panel holds the input box and Send button.
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

                // Gives the user useful guidance.
                lblStatus.Dock = DockStyle.Fill;
                lblStatus.Text = "Ask Cybot about passwords, phishing, scams, privacy, malware, or safe browsing.";
                lblStatus.ForeColor = Color.LightGray;
                lblStatus.Font = new Font("Segoe UI", 9);
                lblStatus.TextAlign = ContentAlignment.MiddleCenter;

                layout.Controls.Add(lblBranding, 0, 0);
                layout.Controls.Add(rtbChatHistory, 0, 1);
                layout.Controls.Add(inputPanel, 0, 2);
                layout.Controls.Add(lblStatus, 0, 3);

                Controls.Add(layout);
            }

            private void BtnSend_Click(object? sender, EventArgs e)
            {
                SendUserMessage();
            }

            private void TxtUserInput_KeyDown(object? sender, KeyEventArgs e)
            {
                // Lets Enter send the message as well as the Send button.
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SendUserMessage();
                }
            }

            private void SendUserMessage()
            {
                string userMessage = txtUserInput.Text.Trim();

                // Blank input is safely handled without a crash.
                if (string.IsNullOrWhiteSpace(userMessage))
                {
                    DisplayBotMessage("Please type a question or message first.");
                    return;
                }

                // Show the user's message before Cybot processes it.
                DisplayUserMessage(userMessage);

                txtUserInput.Clear();
                txtUserInput.Focus();

                // Send the message to the separate chatbot logic class.
                cybot.ProcessInput(userMessage);
            }

            private void DisplayUserMessage(string message)
            {
                rtbChatHistory.SelectionColor = Color.LightSkyBlue;
                rtbChatHistory.AppendText($"You > {message}{Environment.NewLine}{Environment.NewLine}");
                rtbChatHistory.ScrollToCaret();
            }

            private void DisplayBotMessage(string message)
            {
                rtbChatHistory.SelectionColor = Color.LightGreen;
                rtbChatHistory.AppendText($"Cybot > {message}{Environment.NewLine}{Environment.NewLine}");
                rtbChatHistory.ScrollToCaret();
            }
        }
    }