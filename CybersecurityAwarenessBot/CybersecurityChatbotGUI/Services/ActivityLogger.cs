using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CybersecurityChatbotGUI.Models;

namespace CybersecurityChatbotGUI.Services
{
    public class ActivityLogger
    {
        // Stores meaningful actions while the application is open.
        private readonly List<ActivityEntry> entries =
            new List<ActivityEntry>();

        public void Log(string description)
        {
            entries.Add(new ActivityEntry
            {
                Time = DateTime.Now,
                Description = description
            });
        }

        public List<ActivityEntry> GetRecent(int count)
        {
            // Returns the newest entries first.
            return entries
                .OrderByDescending(entry => entry.Time)
                .Take(count)
                .ToList();
        }
    }
}