namespace CybersecurityAwarenessBot
{
    internal class Chatbot
    {
        // Create objects from the other classes Cybot needs.
        private readonly VoiceGreeting _voiceGreeting = new VoiceGreeting();
        private readonly AsciiArt _asciiArt = new AsciiArt();
        private readonly ResponseHandler _responseHandler = new ResponseHandler();

        public void Start()
        {
            Console.Clear();

            // Play the WAV voice greeting first.
            _voiceGreeting.PlayGreeting();

            // Display Cybot's ASCII-art logo.
            _asciiArt.DisplayLogo();

            // Ask for a valid user name.
            UserProfile user = GetValidUserProfile();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Cybot > Welcome, {user.Name}!");
            Console.WriteLine("I am your Cybersecurity Awareness Assistant.");
            Console.WriteLine("You can ask me about:");
            Console.WriteLine("- Password safety");
            Console.WriteLine("- Phishing");
            Console.WriteLine("- Safe browsing");
            Console.WriteLine("- My purpose");
            Console.ResetColor();

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Type 'exit' or 'quit' when you want to end the conversation.");
            Console.WriteLine();

            // Keep the chatbot running until the user types exit or quit.
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{user.Name} > ");
                Console.ResetColor();

                // Read the user's question safely.
                string userInput = Console.ReadLine() ?? "";

                // Validate blank questions.
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    WriteBotMessage(
                        "Please type a question so that I can help you.",
                        ConsoleColor.Yellow);

                    continue;
                }

                // Clean the input before checking if the user wants to leave.
                string cleanedInput = userInput.ToLower().Trim();

                // End the chatbot conversation.
                if (cleanedInput == "exit" || cleanedInput == "quit")
                {
                    WriteBotMessage(
                        $"Goodbye, {user.Name}. Stay safe online!",
                        ConsoleColor.Green);

                    break;
                }

                // Get Cybot's response from ResponseHandler.
                string response = _responseHandler.GetResponse(userInput, user.Name);

                // Display Cybot's response.
                WriteBotMessage(response, ConsoleColor.Magenta);
            }
        }

        private UserProfile GetValidUserProfile()
        {
            while (true)
            {
                Console.Write("Cybot > What is your name? ");
                string name = Console.ReadLine() ?? "";

                // Reject blank names and ask again.
                if (!string.IsNullOrWhiteSpace(name))
                {
                    //create UserProfile using its constructor
                    return new UserProfile(name.Trim());
           
                }

                WriteBotMessage(
                    "I did not catch your name. Please enter your name so I can personalise the conversation.",
                    ConsoleColor.Yellow);
            }
        }

        private static void WriteBotMessage(string message, ConsoleColor colour)
        {
            // Use a consistent coloured format for every Cybot message.
            Console.ForegroundColor = colour;
            Console.WriteLine($"Cybot > {message}");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}