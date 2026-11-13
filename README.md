# Cybot – Cybersecurity Awareness Chatbot

## PROG6221 Programming 2A – Final POE

Cybot is a C# Windows Forms cybersecurity awareness chatbot created for the PROG6221 Programming 2A module. The application helps South African users learn safer online habits through an interactive chatbot, task manager, reminders, cybersecurity quiz, natural-language commands, and an activity log.

## Features

### Part 1 Features

- Cybot cybersecurity chatbot.
- Personalised greeting using the user's name.
- Voice greeting using a `.wav` audio file.
- Cybersecurity-themed visual branding.
- Input validation for empty messages.
- Password safety advice.
- Phishing scam advice.
- Safe browsing advice.
- Unknown-question fallback response.
- Object-oriented C# class structure.

### Part 2 Features

- Windows Forms graphical user interface.
- Chat history showing both user and Cybot messages.
- Delegate used to send chatbot responses to the GUI.
- Keyword recognition for cybersecurity questions.
- Conversation memory.
- Favourite cybersecurity topic memory.
- Sentiment responses for messages such as “I am worried” or “I am happy”.
- Randomised and helpful cybersecurity responses.
- Cybot branding carried forward from Part 1.

### Part 3 Features

- MySQL database integration.
- Persistent cybersecurity task manager.
- Add cybersecurity tasks.
- View saved tasks.
- Mark selected tasks as complete.
- Delete selected tasks.
- Optional task reminders with date and time.
- Tasks remain saved after the application closes and reopens.
- Twelve-question cybersecurity quiz.
- Multiple-choice and true/false quiz questions.
- Immediate correct/incorrect feedback.
- Final quiz score.
- Natural-language processing for chatbot commands.
- Activity Log showing recent user actions.
- Commands such as:
  - Add a task to update my password
  - Remind me to review privacy settings tomorrow
  - Show my tasks
  - Start quiz
  - What have you done for me?

## Technologies Used

- C#
- .NET Windows Forms
- Visual Studio 2022
- MySQL Community Server
- MySQL Workbench
- MySql.Data NuGet package
- GitHub
- GitHub Actions

## Project Structure

- `ChatbotForm.cs` – Main Windows Forms interface.
- `ChatBot.cs` – Cybot conversation logic and responses.
- `VoiceGreeting.cs` – Plays the Part 1 greeting audio.
- `Models` – Stores task, quiz, conversation, and activity data.
- `Data` – Contains MySQL database configuration and task repository code.
- `Services` – Contains quiz logic, NLP processing, and activity logging.
- `Audio` – Contains the greeting `.wav` file.
- `.github/workflows` – Contains the GitHub Actions CI workflow.

## Database Setup

1. Install MySQL Community Server and MySQL Workbench.
2. Create a MySQL connection using:
   - Host: `localhost`
   - Port: `3306`
   - User: `root`
3. Run the following SQL script in MySQL Workbench:

    CREATE DATABASE IF NOT EXISTS CyberSecurityBotDB;

    USE CyberSecurityBotDB;

    CREATE TABLE IF NOT EXISTS cyber_tasks
    (
        task_id INT AUTO_INCREMENT PRIMARY KEY,
        title VARCHAR(150) NOT NULL,
        description VARCHAR(500) NOT NULL,
        reminder_date DATETIME NULL,
        is_completed BOOLEAN NOT NULL DEFAULT FALSE,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );

4. In `DatabaseConfig.cs`, enter your own local MySQL password:

    Pwd=YOUR_MYSQL_PASSWORD;

5. Do not upload your real password to GitHub.

## How to Run the Application

1. Clone or download this repository.
2. Open the solution in Visual Studio 2022.
3. Install the `MySql.Data` NuGet package if it is not already installed.
4. Configure your MySQL password in `DatabaseConfig.cs`.
5. Ensure `greeting.wav` is in the `Audio` folder.
6. Build the solution.
7. Run the application using the green Start button.

## Testing Performed

The following features were tested:

- Entering a valid name.
- Empty chat input validation.
- Password safety questions.
- Phishing scam questions.
- Safe browsing questions.
- Favourite topic memory.
- Adding a task manually.
- Adding a task through chatbot natural-language commands.
- Setting a task reminder.
- Completing a selected task.
- Deleting a selected task.
- Checking MySQL task persistence after restarting the application.
- Completing the cybersecurity quiz.
- Viewing final quiz score.
- Viewing recent Activity Log entries.
- Using natural-language commands to open Tasks, Quiz, and Activity Log.

## GitHub Version Control

This project uses GitHub for version control and GitHub Actions for continuous integration.

Important: `DatabaseConfig.cs` must not be committed when it contains a real MySQL password.

## Releases and Tags

- `v1.0-part1` – Console chatbot implementation.
- `v2.0-part2` – Windows Forms GUI chatbot implementation.
- `v3.0-final` – Final POE with MySQL tasks, quiz, NLP, and activity log.

## Video Demonstration

Unlisted YouTube Video Link: https://youtu.be/EOWzTq8PM9I

## Author

Buhle
PROG6221 Programming 2A
2026
