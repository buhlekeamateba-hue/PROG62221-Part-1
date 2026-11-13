using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CybersecurityChatbotGUI.SentimentDetector;

namespace CybersecurityChatbotGUI
{
    internal class ChatBot
    {
        // A delegate stores a method that can display Cybot's response.
        public delegate void BotResponseHandler(string message);

            private readonly MemoryStore memory = new MemoryStore();
            private readonly KeywordResponder keywordResponder = new KeywordResponder();
            private readonly SentimentDetector sentimentDetector = new SentimentDetector();

            // The form connects this delegate to its DisplayBotMessage method.
            public BotResponseHandler? ResponseHandler { get; set; }

            public void BeginConversation()
            {
                SendResponse(
                    "Hello! Welcome to Cybot, your Cybersecurity Awareness Bot. " +
                    "What is your name?");
            }

            public void ProcessInput(string userInput)
            {
                string cleanedInput = userInput.ToLower().Trim();

                // First message is used to capture the user's name.
                if (string.IsNullOrWhiteSpace(memory.UserName))
                {
                    memory.UserName = userInput.Trim();

                    SendResponse(
                        $"Nice to meet you, {memory.UserName}! " +
                        "Ask me about passwords, phishing, scams, privacy, malware, or safe browsing.");

                    return;
                }

                // Lets the user update their name later.
                if (cleanedInput.StartsWith("my name is "))
                {
                    memory.UserName = userInput.Substring(11).Trim();

                    SendResponse($"Thank you. I will remember that your name is {memory.UserName}.");
                    return;
                }

                // Allows Cybot to recall the user's name.
                if (cleanedInput.Contains("what is my name") ||
                    cleanedInput.Contains("remember my name"))
                {
                    SendResponse($"Your name is {memory.UserName}.");
                    return;
                }

                // Allows Cybot to recall the favourite topic.
                if (cleanedInput.Contains("what is my favourite topic") ||
                    cleanedInput.Contains("what is my favorite topic"))
                {
                    if (string.IsNullOrWhiteSpace(memory.FavouriteTopic))
                    {
                        SendResponse("You have not told me a favourite cybersecurity topic yet.");
                    }
                    else
                    {
                        SendResponse($"You told me that you are interested in {memory.FavouriteTopic}.");
                    }

                    return;
                }

                // Required general questions from Part 1.
                if (cleanedInput.Contains("how are you"))
                {
                    SendResponse($"I am doing well, {memory.UserName}! I am ready to help you stay safe online.");
                    return;
                }

                if (cleanedInput.Contains("purpose") ||
                    cleanedInput.Contains("what do you do"))
                {
                    SendResponse(
                        "My purpose is to teach practical cybersecurity habits that help protect your information online.");

                    return;
                }

                if (cleanedInput.Contains("what can i ask") ||
                    cleanedInput == "help")
                {
                    SendResponse(
                        "You can ask about passwords, phishing, scams, privacy, malware, and safe browsing.");

                    return;
                }

                // Detect the user's mood before deciding the final response.
                Sentiment sentiment = sentimentDetector.DetectSentiment(cleanedInput);

                // Detect a new cybersecurity topic in the current message.
                string detectedTopic = keywordResponder.DetectTopic(cleanedInput);

                // If a new topic is found, store it as the current conversation topic.
                if (!string.IsNullOrWhiteSpace(detectedTopic))
                {
                    memory.CurrentTopic = detectedTopic;

                    // Remember an interest when the user says they are interested or curious.
                    if (cleanedInput.Contains("interested in") ||
                        cleanedInput.Contains("i like") ||
                        cleanedInput.Contains("curious about"))
                    {
                        memory.FavouriteTopic = detectedTopic;

                        SendResponse(
                            $"I will remember that you are interested in {detectedTopic}. " +
                            BuildTopicResponse(detectedTopic, sentiment));

                        return;
                    }

                    SendResponse(BuildTopicResponse(detectedTopic, sentiment));
                    return;
                }

                // Continue the previous topic when a follow-up phrase is used.
                if (IsFollowUp(cleanedInput) &&
                    !string.IsNullOrWhiteSpace(memory.CurrentTopic))
                {
                    SendResponse(
                       $"Here is another {memory.CurrentTopic} tip: " +
                        BuildTopicResponse(memory.CurrentTopic, sentiment));

                    return;
                }

                // Handles sentiment even when no topic is recognised.
                if (sentiment != Sentiment.Neutral)
                {
                    SendResponse(
                        sentimentDetector.GetSentimentPrefix(sentiment) +
                        "Tell me whether this relates to passwords, phishing, scams, privacy, malware, or safe browsing.");

                    return;
                }

                // Friendly fallback for unsupported input.
                SendResponse(
                    "I am not sure I understand. Please try rephrasing, or ask me about passwords, phishing, scams, privacy, malware, or safe browsing.");
            }

            private string BuildTopicResponse(string topic, Sentiment sentiment)
            {
                string sentimentPrefix = sentimentDetector.GetSentimentPrefix(sentiment);
                string cybersecurityTip = keywordResponder.GetRandomResponse(topic);

                // Uses stored favourite-topic memory naturally in later conversations.
                if (!string.IsNullOrWhiteSpace(memory.FavouriteTopic) &&
                    memory.FavouriteTopic == topic)
                {
                    return $"{sentimentPrefix}Since you are interested in {topic}, remember: {cybersecurityTip}";
                }

                return $"{sentimentPrefix}{cybersecurityTip}";
            }

            private bool IsFollowUp(string input)
            {
                return input.Contains("tell me more") ||
                       input.Contains("give me another tip") ||
                       input.Contains("another tip") ||
                       input.Contains("explain more") ||
                       input.Contains("what else") ||
                       input.Contains("continue") ||
                       input.Contains("i do not understand");
            }

            private void SendResponse(string message)
            {
                // Invokes the GUI method connected through the delegate.
                ResponseHandler?.Invoke(message);
            }
        }
    }

