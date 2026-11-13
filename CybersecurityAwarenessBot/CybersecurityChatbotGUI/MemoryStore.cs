using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI
{
    internal class MemoryStore
    {
            // Stores details Cybot can recall later.
            public string UserName { get; set; } = "";

            // Stores a topic the user says they are interested in.
            public string FavouriteTopic { get; set; } = "";

            // Stores the current topic for follow-up questions.
            public string CurrentTopic { get; set; } = "";
        }
    }
