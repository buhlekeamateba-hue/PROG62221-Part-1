using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI.Models
{
    public class QuizQuestion
    {
        // The question shown to the user.
        public string QuestionText { get; set; } = "";

        // Multiple-choice options, or True/False options.
        public List<string> Options { get; set; } = new List<string>();

        // The correct option position: 0 is first, 1 is second, and so on.
        public int CorrectOptionIndex { get; set; }

        // Explanation shown immediately after the user answers.
        public string Explanation { get; set; } = "";
    }
}
