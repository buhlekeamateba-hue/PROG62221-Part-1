using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI.Models
{
    public class ActivityEntry
    {
        // Stores the date and time of an important action.
        public DateTime Time { get; set; }

        // Explains the action, for example: Task added.
        public string Description { get; set; } = "";

        public override string ToString()
        {
            // This is how each activity appears in the Activity Log list.
            return $"{Time:HH:mm} - {Description}";
        }
    }
}
