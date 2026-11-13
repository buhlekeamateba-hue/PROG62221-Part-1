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
        // Quiz tab and controls.
        private readonly TabPage tabQuiz = new TabPage("Cyber Quiz");
        private readonly Label lblQuestionNumber = new Label();
        private readonly Label lblQuestion = new Label();
        private readonly RadioButton rdoA = new RadioButton();
        private readonly RadioButton rdoB = new RadioButton();
        private readonly RadioButton rdoC = new RadioButton();
        private readonly RadioButton rdoD = new RadioButton();
        private readonly Button btnStartQuiz = new Button();
        private readonly Button btnSubmitAnswer = new Button();
        private readonly Button btnNextQuestion = new Button();
        private readonly Label lblQuizFeedback = new Label();
        private readonly Label lblQuizScore = new Label();

        // Controls the 12 quiz questions and score.
        private readonly QuizManager quizManager = new QuizManager();

        public void AddQuizTab()
        {
            tabQuiz.BackColor = Color.FromArgb(35, 45, 60);

            TableLayoutPanel quizLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 9,
                Padding = new Padding(25)
            };

            quizLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            quizLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));
            quizLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            quizLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            quizLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            quizLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));
            quizLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            quizLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            quizLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));

            // Shows question number, for example Question 1 of 12.
            lblQuestionNumber.Dock = DockStyle.Fill;
            lblQuestionNumber.ForeColor = Color.Cyan;
            lblQuestionNumber.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblQuestionNumber.TextAlign = ContentAlignment.MiddleCenter;
            lblQuestionNumber.Text = "Cybersecurity Quiz";

            // Shows one question at a time.
            lblQuestion.Dock = DockStyle.Fill;
            lblQuestion.ForeColor = Color.White;
            lblQuestion.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblQuestion.TextAlign = ContentAlignment.MiddleCenter;
            lblQuestion.Text = "Click Start Quiz to begin.";

            SetupQuizRadioButton(rdoA);
            SetupQuizRadioButton(rdoB);
            SetupQuizRadioButton(rdoC);
            SetupQuizRadioButton(rdoD);

            // Quiz buttons.
            btnStartQuiz.Text = "Start Quiz";
            btnStartQuiz.Width = 120;
            btnStartQuiz.Height = 32;
            btnStartQuiz.BackColor = Color.MediumSeaGreen;
            btnStartQuiz.ForeColor = Color.White;
            btnStartQuiz.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnStartQuiz.Click += BtnStartQuiz_Click;

            btnSubmitAnswer.Text = "Submit Answer";
            btnSubmitAnswer.Width = 140;
            btnSubmitAnswer.Height = 32;
            btnSubmitAnswer.BackColor = Color.DodgerBlue;
            btnSubmitAnswer.ForeColor = Color.White;
            btnSubmitAnswer.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnSubmitAnswer.Enabled = false;
            btnSubmitAnswer.Click += BtnSubmitAnswer_Click;

            btnNextQuestion.Text = "Next Question";
            btnNextQuestion.Width = 140;
            btnNextQuestion.Height = 32;
            btnNextQuestion.BackColor = Color.DarkOrange;
            btnNextQuestion.ForeColor = Color.White;
            btnNextQuestion.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnNextQuestion.Enabled = false;
            btnNextQuestion.Click += BtnNextQuestion_Click;

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight
            };

            buttonPanel.Controls.Add(btnStartQuiz);
            buttonPanel.Controls.Add(btnSubmitAnswer);
            buttonPanel.Controls.Add(btnNextQuestion);

            // Shows immediate correct or incorrect feedback.
            lblQuizFeedback.Dock = DockStyle.Fill;
            lblQuizFeedback.ForeColor = Color.White;
            lblQuizFeedback.Font = new Font("Segoe UI", 10);
            lblQuizFeedback.TextAlign = ContentAlignment.MiddleCenter;

            // Shows the running score.
            lblQuizScore.Dock = DockStyle.Fill;
            lblQuizScore.ForeColor = Color.LightGreen;
            lblQuizScore.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblQuizScore.TextAlign = ContentAlignment.MiddleCenter;
            lblQuizScore.Text = "Score: 0/12";

            quizLayout.Controls.Add(lblQuestionNumber, 0, 0);
            quizLayout.Controls.Add(lblQuestion, 0, 1);
            quizLayout.Controls.Add(rdoA, 0, 2);
            quizLayout.Controls.Add(rdoB, 0, 3);
            quizLayout.Controls.Add(rdoC, 0, 4);
            quizLayout.Controls.Add(rdoD, 0, 5);
            quizLayout.Controls.Add(buttonPanel, 0, 6);
            quizLayout.Controls.Add(lblQuizFeedback, 0, 7);
            quizLayout.Controls.Add(lblQuizScore, 0, 8);

            tabQuiz.Controls.Add(quizLayout);

            // Adds the tab beside Chat and Tasks.
            tabMain.TabPages.Add(tabQuiz);
        }

        private void SetupQuizRadioButton(RadioButton radioButton)
        {
            radioButton.Dock = DockStyle.Fill;
            radioButton.ForeColor = Color.White;
            radioButton.Font = new Font("Segoe UI", 10);
            radioButton.TextAlign = ContentAlignment.MiddleLeft;
            radioButton.AutoSize = false;
        }

        private void BtnStartQuiz_Click(object? sender, EventArgs e)
        {
            // Starts from question 1 and resets the score.
            quizManager.StartQuiz();

            lblQuizFeedback.Text = "";
            btnSubmitAnswer.Enabled = true;
            btnNextQuestion.Enabled = false;

            DisplayCurrentQuestion();
        }

        private void DisplayCurrentQuestion()
        {
            QuizQuestion question = quizManager.CurrentQuestion;

            lblQuestionNumber.Text =
                $"Question {quizManager.CurrentIndex + 1} of {quizManager.TotalQuestions}";

            lblQuestion.Text = question.QuestionText;

            RadioButton[] buttons = { rdoA, rdoB, rdoC, rdoD };

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Checked = false;

                // Shows only two options for True/False questions.
                if (i < question.Options.Count)
                {
                    buttons[i].Text = question.Options[i];
                    buttons[i].Visible = true;
                }
                else
                {
                    buttons[i].Visible = false;
                }
            }

            lblQuizScore.Text =
                $"Score: {quizManager.Score}/{quizManager.TotalQuestions}";
        }

        private int GetSelectedQuizOption()
        {
            if (rdoA.Checked) return 0;
            if (rdoB.Checked) return 1;
            if (rdoC.Checked) return 2;
            if (rdoD.Checked) return 3;

            return -1;
        }

        private void BtnSubmitAnswer_Click(object? sender, EventArgs e)
        {
            int selectedIndex = GetSelectedQuizOption();

            if (selectedIndex == -1)
            {
                lblQuizFeedback.ForeColor = Color.Gold;
                lblQuizFeedback.Text = "Please select an answer.";
                return;
            }

            QuizAnswerResult result =
                quizManager.CheckAnswer(selectedIndex);

            lblQuizFeedback.ForeColor =
                result.IsCorrect ? Color.LightGreen : Color.Salmon;

            lblQuizFeedback.Text = result.Feedback;

            lblQuizScore.Text =
                $"Score: {quizManager.Score}/{quizManager.TotalQuestions}";

            // Prevents the score increasing twice for the same question.
            btnSubmitAnswer.Enabled = false;
            btnNextQuestion.Enabled = true;
        }

        private void BtnNextQuestion_Click(object? sender, EventArgs e)
        {
            bool hasAnotherQuestion = quizManager.MoveNext();

            if (hasAnotherQuestion)
            {
                lblQuizFeedback.Text = "";
                btnSubmitAnswer.Enabled = true;
                btnNextQuestion.Enabled = false;

                DisplayCurrentQuestion();
            }
            else
            {
                // Shows final score and feedback after question 12.
                lblQuizFeedback.ForeColor = Color.LightGreen;
                lblQuizFeedback.Text =
                    $"Quiz complete! Final score: " +
                    $"{quizManager.Score}/{quizManager.TotalQuestions}. " +
                    GetFinalQuizComment();

                btnSubmitAnswer.Enabled = false;
                btnNextQuestion.Enabled = false;
            }
        }

        private string GetFinalQuizComment()
        {
            double percentage =
                (double)quizManager.Score / quizManager.TotalQuestions * 100;

            if (percentage >= 80)
            {
                return "Excellent! You have strong cybersecurity awareness.";
            }

            if (percentage >= 60)
            {
                return "Good work. Review the topics you missed.";
            }

            return "Keep learning. Revisit the cybersecurity tips and try again.";
        }
    }
}