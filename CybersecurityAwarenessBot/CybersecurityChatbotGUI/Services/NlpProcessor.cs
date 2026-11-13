using System;
using System.Text.RegularExpressions;

namespace CybersecurityChatbotGUI.Services
{
    public class NlpProcessor
    {
        public IntentResult DetectIntent(string rawInput)
        {
            // Makes matching case-insensitive and removes unnecessary spaces.
            string input = rawInput.Trim().ToLower();

            // Checks activity-log phrases first.
            if (ContainsAny(input,
                "show activity log",
                "show my activity",
                "recent actions",
                "what have you done for me"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.ShowActivityLog
                };
            }

            // Recognises different ways to start the quiz.
            if (ContainsAny(input,
                "start quiz",
                "play quiz",
                "cyber quiz",
                "test my knowledge",
                "cybersecurity game"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.StartQuiz
                };
            }

            // Recognises different ways to request saved tasks.
            if (ContainsAny(input,
                "show tasks",
                "show my tasks",
                "view tasks",
                "what tasks do i have"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.ShowTasks
                };
            }

            // Recognises reminder commands.
            if (input.Contains("remind me to") ||
                input.Contains("set a reminder"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.AddTaskWithReminder,
                    TaskTitle = ExtractTaskTitle(rawInput),
                    ReminderDate = ExtractReminderDate(input)
                };
            }

            // Recognises task creation commands.
            if (ContainsAny(input,
                "add task",
                "add a task",
                "create task",
                "create a task",
                "new task"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.AddTask,
                    TaskTitle = ExtractTaskTitle(rawInput)
                };
            }

            // Lets normal cybersecurity questions continue to Part 2 chatbot logic.
            return new IntentResult
            {
                Intent = UserIntent.CybersecurityQuestion
            };
        }

        private bool ContainsAny(string input, params string[] phrases)
        {
            foreach (string phrase in phrases)
            {
                if (input.Contains(phrase))
                {
                    return true;
                }
            }

            return false;
        }

        public string ExtractTaskTitle(string input)
        {
            string title = input;

            // Removes common command wording from the beginning.
            title = Regex.Replace(
                title,
                @"^(please\s+)?(add\s+(a\s+)?task(\s+to|\s*-\s*)?|" +
                @"create\s+(a\s+)?task(\s+to)?|new\s+task(\s+to)?|" +
                @"remind\s+me\s+to|set\s+a\s+reminder(\s+to)?)\s*",
                "",
                RegexOptions.IgnoreCase);

            // Removes simple reminder wording from the end.
            title = Regex.Replace(
                title,
                @"\s+(tomorrow|in\s+\d+\s+days?)\.?$",
                "",
                RegexOptions.IgnoreCase);

            return title.Trim().TrimEnd('.');
        }

        public DateTime? ExtractReminderDate(string input)
        {
            if (input.Contains("tomorrow"))
            {
                return DateTime.Today.AddDays(1);
            }

            Match match = Regex.Match(
                input,
                @"in\s+(\d+)\s+days?",
                RegexOptions.IgnoreCase);

            if (match.Success)
            {
                int days = Convert.ToInt32(match.Groups[1].Value);
                return DateTime.Today.AddDays(days);
            }

            return null;
        }
    }
}