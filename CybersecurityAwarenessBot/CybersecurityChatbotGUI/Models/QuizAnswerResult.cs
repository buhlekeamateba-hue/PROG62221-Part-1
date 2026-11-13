using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI.Models
{
    public class QuizAnswerResult
    {
        // True if the selected option was correct.
        public bool IsCorrect { get; set; }

        // Feedback and explanation shown to the user.
        public string Feedback { get; set; } = "";
    }
}
