using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI.Data
{
    public static class DatabaseConfig
    {
        public static string ConnectionString =
    "Server=localhost;Port=3306;" +
    "Database=CyberSecurityBotDB;" +
    "Uid=root;Pwd=YOUR_MYSQL_PASSWORD;";
    }
}