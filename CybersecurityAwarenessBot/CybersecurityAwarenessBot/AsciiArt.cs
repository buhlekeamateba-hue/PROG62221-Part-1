using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityAwarenessBot
{
    internal class AsciiArt
    {
        public void DisplayLogo()
        {
            // Display Cybot's logo in cyan.
     
        Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("============================================================");
            Console.WriteLine("   _____ __   __ ____   ____ _ ______  ");
            Console.WriteLine("  / ____|\\ \\ / /|  _ \\ / __ \\__  _|");
            Console.WriteLine(" | |      \\ V / | |_) | |  | | | | | ");
            Console.WriteLine(" | |       > <   |  _ <| |  | | | | | ");
            Console.WriteLine(" | |____  / . \\ | |_) | |__| | | | |  ");
            Console.WriteLine("  \\_____|/_/ \\_\\|____/ \\____/ |_|   ");
            Console.WriteLine("============================================================");
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("         CYBOT - CYBERSECURITY AWARENESS BOT");
            Console.WriteLine("                    STAY SAFE ONLINE");
            Console.WriteLine("============================================================");

            // Return the console colour to normal.
            Console.ResetColor();
            Console.WriteLine();
        }
}                                                                                                          
        }