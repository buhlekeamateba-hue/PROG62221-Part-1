using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI
{
    internal class KeywordResponder
    {
            // Dictionary maps cybersecurity topics to several useful responses.
            private readonly Dictionary<string, List<string>> topicResponses;

            // One Random object is reused to select varied answers.
            private readonly Random random = new Random();

            public KeywordResponder()
            {
                topicResponses = new Dictionary<string, List<string>>
                {
                    ["password"] = new List<string>
                {
                    "Use a long, unique password or passphrase for every account. Avoid personal information such as your birthday.",
                    "A password manager can help you create and store strong, different passwords for every account.",
                    "Turn on two-factor authentication whenever possible. It adds protection even if someone discovers your password."
                },

                    ["phishing"] = new List<string>
                {
                    "Be careful of unexpected links and attachments. Check the sender's address before responding to a message.",
                    "Phishing messages often create urgency. Do not share passwords, OTPs, or banking details because a message pressures you.",
                    "If an email seems suspicious, contact the organisation using its official website or phone number instead of the message link."
                },

                    ["scam"] = new List<string>
                {
                    "Online scams often promise prizes, jobs, or urgent help. Pause and verify the message before sending money or information.",
                    "Never share a one-time PIN, password, or banking details with someone who contacted you unexpectedly.",
                    "If a message sounds too good to be true or threatens you with urgency, treat it as suspicious and verify it independently."
                },

                    ["privacy"] = new List<string>
                {
                    "Review your social-media privacy settings and limit who can see your personal information.",
                    "Only give apps the permissions they truly need. For example, a torch app should not need access to your contacts.",
                    "Think carefully before posting personal details such as your address, ID number, location, or travel plans."
                },

                    ["safe browsing"] = new List<string>
                {
                    "Check for HTTPS and a padlock icon before entering sensitive details on a website.",
                    "Avoid downloading software, files, or browser extensions from unknown websites.",
                    "Keep your browser, operating system, and antivirus software updated to reduce security risks."
                },

                    ["malware"] = new List<string>
                { "Malware is harmful software. Avoid unknown downloads and keep your antivirus software updated.",
                    "Do not open unexpected attachments, because they may install malware on your device.",
                    "Back up important files regularly so you can recover them if malware damages or locks your data."}
                };
            }

            public string DetectTopic(string input)
            {
                string cleanedInput = input.ToLower().Trim();

                if (cleanedInput.Contains("password") || cleanedInput.Contains("passphrase"))
                {
                    return "password";
                }

                if (cleanedInput.Contains("phishing"))
                {
                    return "phishing";
                }

                if (cleanedInput.Contains("scam") || cleanedInput.Contains("fraud"))
                {
                    return "scam";
                }

                if (cleanedInput.Contains("privacy") || cleanedInput.Contains("private"))
                {
                    return "privacy";
                }

                if (cleanedInput.Contains("browsing") ||
                    cleanedInput.Contains("browser") ||
                    cleanedInput.Contains("website") ||
                    cleanedInput.Contains("online safety"))
                {
                    return "safe browsing";
                }

                if (cleanedInput.Contains("malware") ||
                    cleanedInput.Contains("virus") ||
                    cleanedInput.Contains("ransomware"))
                {
                    return "malware";
                }

                return "";
            }

            public string GetRandomResponse(string topic)
            {
                // Checks that the selected topic exists in the dictionary.
                if (topicResponses.ContainsKey(topic))
                {
                    List<string> responses = topicResponses[topic];

                    // Randomly selects one useful response from the topic list.
                    return responses[random.Next(responses.Count)];
                }

                return "I am not sure about that topic yet.";
            }
        }
    }


