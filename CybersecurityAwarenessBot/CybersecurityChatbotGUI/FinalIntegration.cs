using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CybersecurityChatbotGUI.Models;
using CybersecurityChatbotGUI.Services;

namespace CybersecurityChatbotGUI
{
    public partial class ChatbotForm
    {
        // Recognises Part 3 natural-language commands.
        private readonly NlpProcessor nlpProcessor = new NlpProcessor();

        private bool TryHandlePart3Command(string userMessage)
        {
            IntentResult result = nlpProcessor.DetectIntent(userMessage);

            switch (result.Intent)
            {
                case UserIntent.StartQuiz:
                    StartQuizFromChat();
                    return true;

                case UserIntent.ShowTasks:
                    tabMain.SelectedTab = tabTasks;
                    LoadTasks();

                    activityLogger.Log("Tasks requested through chatbot");
                    DisplayRecentActivity(5);

                    DisplayBotMessage(
                        "I have opened your saved cybersecurity tasks.");
                    return true;

                case UserIntent.ShowActivityLog:
                    tabMain.SelectedTab = tabActivity;

                    activityLogger.Log(
                        "Activity Log requested through chatbot");

                    DisplayRecentActivity(5);

                    DisplayBotMessage(
                        "I have opened your recent Activity Log.");
                    return true;

                case UserIntent.AddTask:
                    DisplayBotMessage(
                        AddTaskFromChat(result.TaskTitle, null));
                    return true;

                case UserIntent.AddTaskWithReminder:
                    DisplayBotMessage(
                        AddTaskFromChat(
                            result.TaskTitle,
                            result.ReminderDate));
                    return true;

                // Normal cybersecurity questions must continue to Part 2 logic.
                case UserIntent.CybersecurityQuestion:
                default:
                    return false;
            }
        }

        private void StartQuizFromChat()
        {
            // Opens the quiz tab and starts question 1.
            tabMain.SelectedTab = tabQuiz;

            quizManager.StartQuiz();

            lblQuizFeedback.Text = "";
            btnSubmitAnswer.Enabled = true;
            btnNextQuestion.Enabled = false;

            DisplayCurrentQuestion();

            activityLogger.Log(
                "Cybersecurity quiz started through chatbot");

            DisplayRecentActivity(5);

            DisplayBotMessage(
                "Sure. I have opened the Cybersecurity Quiz.");
        }

        private string AddTaskFromChat(
            string taskTitle,
            DateTime? reminderDate)
        {
            // Prevents empty task commands from being saved.
            if (string.IsNullOrWhiteSpace(taskTitle))
            {
                return "I recognised that you want to add a task, " +
                       "but I need the task details. Try: " +
                       "'Add a task to review my privacy settings.'";
            }

            CyberTask task = new CyberTask
            {
                Title = taskTitle,
                Description =
                    "Task created from chatbot conversation.",
                ReminderDate = reminderDate,
                IsCompleted = false
            };

            try
            {
                int taskId = taskRepository.AddTask(task);

                activityLogger.Log(
                    "Task added through chatbot: " + taskTitle);

                if (reminderDate.HasValue)
                {
                    activityLogger.Log(
                        "Reminder set for " + taskTitle +
                        " on " +
                        reminderDate.Value.ToShortDateString());
                }

                // Updates the task grid and opens the Tasks tab.
                LoadTasks();
                tabMain.SelectedTab = tabTasks;
                DisplayRecentActivity(5);

                if (reminderDate.HasValue)
                {
                    return "Task added successfully. Reminder set for " +
                           reminderDate.Value.ToShortDateString() + ".";
                }

                return "Task added successfully. You can open the " +
                       "Tasks tab to add a reminder.";
            }
            catch (Exception ex)
            {
                return "I could not save that task: " + ex.Message;
            }
        }
    }
}