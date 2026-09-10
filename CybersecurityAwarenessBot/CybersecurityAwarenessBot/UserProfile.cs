using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityAwarenessBot
{
    internal class UserProfile
    {
        //stores the user's name
        public string Name { get; set; } = "";
        //a constructor ensures every userprofile begins with a name
        public UserProfile(string name)
        { 
            this.Name = name; 
        }
    }
}
