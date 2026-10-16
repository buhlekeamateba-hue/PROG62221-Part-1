# PROG6221 Programming 2A - POE Part 2

## Student Details

- **Name:** Sentle Mateba
- **Student Number:** ST10500205
- **Module:** PROG6221 - Programming 2A
- **Project Name:** Cybot - Cybersecurity Awareness Chatbot

## Project Overview

Cybot is a Cybersecurity Awareness Chatbot created for PROG6221 Part 2.

The project expands the Part 1 console application into a Windows Forms graphical user interface. Cybot helps users learn about password safety, phishing, online scams, privacy, malware, and safe browsing.

## Part 1 Features Carried Forward

- WAV voice greeting
- Cybot cybersecurity branding
- Personalised user-name greeting
- Cybersecurity guidance
- Password, phishing, and safe-browsing responses
- Input validation
- Structured classes and methods

## Part 2 Features

- Windows Forms graphical user interface
- Voice greeting plays when the application opens
- Cybot ASCII-style cybersecurity banner
- Personalised conversation using the user's name
- Keyword recognition for cybersecurity topics
- Random responses using a dictionary and lists
- Conversation flow for follow-up questions
- Memory for the user's name, current topic, and favourite topic
- Sentiment detection for worried, curious, and frustrated messages
- Safe handling of blank and unknown input
- Delegate used to display chatbot responses in the GUI
- Send button and Enter-key support

## GUI Overview

The Cybot GUI includes:

- A cybersecurity-themed banner
- A chat-history area
- A text box for user messages
- A Send button
- A status message showing supported topics
- Coloured user and Cybot messages

## Cybersecurity Topics Supported

- Password safety
- Phishing
- Online scams
- Privacy
- Safe browsing
- Malware

## Example Questions

    Tell me about password safety.
    What is phishing?
    I am worried about scams.
    How can I protect my privacy?
    Tell me more.
    I am interested in privacy.
    What is my favourite topic?

## Memory and Sentiment Features

Cybot remembers:

- The user's name
- The current cybersecurity topic
- The user's favourite cybersecurity topic

Cybot detects simple sentiments, including:

- Worried
- Scared
- Curious
- Interested
- Frustrated
- Annoyed

Example:

    You > I am worried about scams.

    Cybot > I understand that this can feel worrying.
            Cybot gives a useful scam safety tip.

## How to Run the Project

1. Clone or download this repository.
2. Open the solution file in Visual Studio 2022.
3. Set `CybersecurityChatbotGUI` as the startup project.
4. Confirm that `greeting.wav` is inside the `Assets` folder.
5. Select `greeting.wav` and set Build Action to `Content`.
6. Set Copy to Output Directory to `Copy if newer`.
7. Build the solution using `Ctrl + Shift + B`.
8. Run the application using `Ctrl + F5`.
9. Enter your name when Cybot asks for it.
10. Ask Cybot a cybersecurity question.

## Project Structure

    CybersecurityChatbotSolution
    │
    ├── CybersecurityAwarenessBot
    │   └── Part 1 console application
    │
    └── CybersecurityChatbotGUI
        ├── Program.cs
        ├── ChatbotForm.cs
        ├── ChatbotForm.Designer.cs
        ├── ChatBot.cs
        ├── KeywordResponder.cs
        ├── MemoryStore.cs
        ├── SentimentDetector.cs
        ├── VoiceGreeting.cs
        │
        └── Assets
            └── greeting.wav

## Testing

| **Test** | **Expected Result** |
|---|---|
| Open the application | The GUI opens and the voice greeting plays |
| Enter a name | Cybot welcomes the user by name |
| Ask about passwords | Cybot gives password safety advice |
| Ask about phishing or scams repeatedly | Cybot can give different valid responses |
| Ask about privacy, then type “tell me more” | Cybot continues with privacy advice |
| Say “I am interested in privacy” | Cybot remembers the interest |
| Ask “What is my favourite topic?” | Cybot recalls the saved topic |
| Type “I am worried about scams” | Cybot recognises sentiment and gives advice |
| Send a blank message | Cybot requests a message without crashing |
| Ask an unrelated question | Cybot gives a helpful fallback response |

## GitHub Actions / Continuous Integration

GitHub Actions is used to restore and build the project automatically after pushes and pull requests.

The workflow file is located at:

    .github/workflows/ci.yml

A successful GitHub Actions build should show a green check mark in the repository's Actions tab.

## Tags and Releases

- **v2.0-gui** - Created the Windows Forms GUI and moved Part 1 features into the GUI.
- **v2.1-dynamic-responses** - Added keyword recognition, random responses, and conversation flow.
- **v2.2-memory-sentiment** - Added memory, sentiment detection, validation, and final polish.

## Video Presentation

Unlisted YouTube video link:

[Add your Part 2 YouTube link here]

## Author

Sentle Mateba - PROG6221 Programming 2A
