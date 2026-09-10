namespace CybersecurityAwarenessBot
{
    internal class ResponseHandler
    {
        public string GetResponse(string userInput, string userName)
        {
            // Make matching case-insensitive and remove spaces at each end.
            string cleanedInput = userInput.ToLower().Trim();

            // General Cybot questions.
            if (cleanedInput.Contains("how are you"))
            {
                return $"I am doing well, {userName}! I am ready to help you stay safe online.";
            }
            else if (cleanedInput.Contains("purpose") ||
                     cleanedInput.Contains("what do you do"))
            {
                return $"My purpose is to help you, {userName}, learn simple cybersecurity habits.";
            }
            else if (cleanedInput.Contains("what can i ask") ||
                     cleanedInput.Contains("help"))
            {
                return "You can ask me about password safety, phishing scams, safe browsing, or my purpose.";
            }

            // Password safety.
            else if (cleanedInput.Contains("password") ||
                     cleanedInput.Contains("passphrase"))
            {
                return "Use a long, unique password or passphrase for every account. "
                     + "Avoid personal information and enable two-factor authentication when possible.";
            }

            // Phishing.
            else if (cleanedInput.Contains("phishing") ||
                     cleanedInput.Contains("scam") ||
                     cleanedInput.Contains("suspicious email"))
            {
                return "Phishing messages try to trick you into sharing information. "
                     + "Do not click unexpected links or attachments. Check the sender and verify requests through official channels.";
            }

            // Safe browsing.
            else if (cleanedInput.Contains("browsing") ||
                     cleanedInput.Contains("browser") ||
                     cleanedInput.Contains("website") ||
                     cleanedInput.Contains("safe online"))
            {
                return "Check that a website uses HTTPS before entering personal information. "
                     + "Avoid downloads from unknown sources and keep your browser and antivirus software updated.";
            }

            // Default response for unrecognised questions.
            else
            {
                return "I did not quite understand that. Could you rephrase? "
                     + "You can ask Cybot about passwords, phishing, or safe browsing.";
            }
        }
    }
}