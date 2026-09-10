# PROG62221-Part-1
# Cybersecurity Awareness Bot

## PROG6221 – Programming 2A

A C# .NET 8.0 console application designed to help South African citizens learn basic cybersecurity practices through an interactive, rule-based chatbot.

## Project Overview

The Cybersecurity Awareness Bot is an interactive console application that provides users with basic cybersecurity information and awareness tips.

The application allows users to enter their name and interact with the chatbot using natural questions and keywords. It provides information about password safety, phishing scams, and safe browsing, while also handling general questions and invalid input.

The application was developed using C# and object-oriented programming principles.

## Features

* Voice greeting played when the application starts
* Custom cybersecurity-themed ASCII header
* Console colours for an improved user interface
* User name input and validation
* Personalised chatbot responses
* Case-insensitive user input
* Keyword-based response system
* Cybersecurity information covering:

  * Password Safety
  * Phishing Scams
  * Safe Browsing
* Responses to general questions such as:

  * How are you?
  * What's your purpose?
  * What can I ask you about?
* Input validation for blank questions
* Fallback responses for unsupported questions
* `exit` and `quit` commands to end the conversation
* Modular object-oriented class structure
* GitHub version control
* Automated CI build using GitHub Actions

## Technologies Used

* C#
* .NET 8.0
* Visual Studio
* Git
* GitHub
* GitHub Actions

## Project Structure

```text
A1/
│
├── Audio/
│   └── greeting.wav
│
├── .github/
│   └── workflows/
│       └── ci.yml
│
├── Program.cs
├── Chatbot.cs
├── UserProfile.cs
├── VoiceGreeting.cs
├── AsciiArt.cs
├── ResponseHandler.cs
└── README.md
```

## Class Responsibilities

### Program.cs

Acts as the entry point of the application and starts the chatbot.

### Chatbot.cs

Manages the main conversation flow, user interaction, and execution of the chatbot.

### UserProfile.cs

Stores user information, including the user's name.

### VoiceGreeting.cs

Locates and plays the `greeting.wav` audio file when the application starts.

### AsciiArt.cs

Displays the cybersecurity-themed ASCII header and controls the console presentation.

### ResponseHandler.cs

Processes the user's input and provides an appropriate response based on keywords and recognised questions.

## Installation and Setup

### Requirements

Before running the application, ensure that the following are installed:

* Visual Studio
* .NET 8.0 SDK
* Git (for version control)

### Running the Application

1. Clone or download the repository from GitHub.
2. Open the project solution in Visual Studio.
3. Ensure that `Audio/greeting.wav` is included in the project.
4. Build the solution.
5. Run the application using **Ctrl + F5** or the **Start Without Debugging** option.
6. Enter your name when prompted.
7. Ask the chatbot questions about cybersecurity.

## Audio Setup

The application uses a `.wav` audio file for the initial voice greeting.

The audio file is stored in:

```text
Audio/greeting.wav
```

The file is configured in Visual Studio with:

```text
Build Action: Content
Copy to Output Directory: Copy if newer
```

The application uses `System.Media.SoundPlayer` to play the greeting.

## Example Interaction

```text
====================================================
        CYBERSECURITY AWARENESS BOT
====================================================

        Stay Alert. Stay Safe. Stay Secure.
====================================================

What is your name?
Sentle

Hello, Sentle! How can I help you with cybersecurity today?

You: How are you?

Bot: I'm doing well, Sentle! I'm ready to help you learn
about staying safe online.

You: What is phishing?

Bot: Phishing is a type of cyber scam where criminals
pretend to be a trustworthy person or organisation to
trick you into revealing sensitive information.

You: exit

Bot: Goodbye, Sentle! Stay safe online.
```

## Continuous Integration

This project uses GitHub Actions to automatically build the application whenever changes are pushed to the repository.

The CI workflow is located at:

```text
.github/workflows/ci.yml
```

A successful workflow run should display a green check mark in GitHub.

## CI Build Screenshot

The following screenshot shows a successful GitHub Actions CI build:

**[Insert screenshot of the successful green CI check here]**

## Version Control

The project was developed using Git and GitHub.

Meaningful commits were used to document the development process, including project setup, implementation of the chatbot features, audio integration, testing, and CI configuration.

## Author

Name: Sentle Mateba
Module: PROG6221 – Programming 2A
Year: 2026
