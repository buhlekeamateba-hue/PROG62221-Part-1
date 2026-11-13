using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI.Services
{
    public class IntentResult
    {
        // The action Cybot recognised from the user's message.
        public UserIntent Intent { get; set; }

        // Task details extracted from a natural-language command.
        public string TaskTitle { get; set; } = "";

        // Optional reminder date extracted from phrases such as tomorrow.
        public DateTime? ReminderDate { get; set; }
    }
}
