using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI
{
    internal class SentimentDetector
    {
        // The sentiment categories required.
        public enum Sentiment
        {
            Neutral,
            Worried,
            Curious,
            Frustrated
        }
            public Sentiment DetectSentiment(string input)
            {
                string cleanedInput = input.ToLower().Trim();

                if (cleanedInput.Contains("worried") ||
                    cleanedInput.Contains("scared") ||
                    cleanedInput.Contains("afraid") ||
                    cleanedInput.Contains("nervous"))
                {
                    return Sentiment.Worried;
                }

                if (cleanedInput.Contains("curious") ||
                    cleanedInput.Contains("interested") ||
                    cleanedInput.Contains("wondering"))
                {
                    return Sentiment.Curious;
                }

                if (cleanedInput.Contains("frustrated") ||
                    cleanedInput.Contains("annoyed") ||
                    cleanedInput.Contains("angry") ||
                    cleanedInput.Contains("confused"))
                {
                    return Sentiment.Frustrated;
                }

                return Sentiment.Neutral;
            }

            public string GetSentimentPrefix(Sentiment sentiment)
            {
                // The bot gives emotional support before useful cybersecurity advice.
                return sentiment switch
                {
                    Sentiment.Worried => "I understand that this can feel worrying. ",
                    Sentiment.Curious => "It is great that you want to learn more. ",
                    Sentiment.Frustrated => "I understand that this can be frustrating. ",
                    _ => ""
                };
            }
        }
    }

