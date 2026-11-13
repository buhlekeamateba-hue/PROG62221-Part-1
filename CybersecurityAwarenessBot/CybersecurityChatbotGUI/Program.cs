namespace CybersecurityChatbotGUI
{
    internal static class Program
    {
            [STAThread]
            static void Main()
            {
                // Starts the Windows Forms application.
                ApplicationConfiguration.Initialize();
                Application.Run(new ChatbotForm());
            }
        }
    }
