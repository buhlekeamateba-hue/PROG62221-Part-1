using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;
using CybersecurityChatbotGUI.Models;

namespace CybersecurityChatbotGUI.Data
{
    public class TaskRepository
    {
        public int AddTask(CyberTask task)
        {
            // Adds a new task to the MySQL database.
            string sql = @"INSERT INTO cyber_tasks
                           (title, description, reminder_date, is_completed)
                           VALUES
                           (@title, @description, @reminder, @completed);";

            using (MySqlConnection connection =
                   new MySqlConnection(DatabaseConfig.ConnectionString))
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@title", task.Title);
                command.Parameters.AddWithValue("@description", task.Description);

                // Uses NULL when the user did not choose a reminder.
                command.Parameters.AddWithValue(
                    "@reminder",
                    task.ReminderDate.HasValue
                        ? (object)task.ReminderDate.Value
                        : DBNull.Value);

                command.Parameters.AddWithValue("@completed", task.IsCompleted);

                connection.Open();
                command.ExecuteNonQuery();

                // Returns the ID created by MySQL.
                return Convert.ToInt32(command.LastInsertedId);
            }
        }

        public List<CyberTask> GetAllTasks()
        {
            List<CyberTask> tasks = new List<CyberTask>();

            // Loads all tasks, showing pending tasks first.
            string sql = @"SELECT task_id, title, description,
                                  reminder_date, is_completed
                           FROM cyber_tasks
                           ORDER BY is_completed, task_id DESC;";

            using (MySqlConnection connection =
                   new MySqlConnection(DatabaseConfig.ConnectionString))
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CyberTask task = new CyberTask
                        {
                            TaskId = Convert.ToInt32(reader["task_id"]),
                            Title = Convert.ToString(reader["title"]) ?? "",
                            Description = Convert.ToString(reader["description"]) ?? "",

                            // A reminder can be NULL in the database.
                            ReminderDate = reader["reminder_date"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["reminder_date"]),

                            IsCompleted = Convert.ToBoolean(reader["is_completed"])
                        };

                        tasks.Add(task);
                    }
                }
            }

            return tasks;
        }

        public void MarkTaskCompleted(int taskId)
        {
            // Changes the selected task to completed in MySQL.
            string sql = @"UPDATE cyber_tasks
                           SET is_completed = TRUE
                           WHERE task_id = @id;";

            using (MySqlConnection connection =
                   new MySqlConnection(DatabaseConfig.ConnectionString))
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", taskId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateReminder(int taskId, DateTime reminderDate)
        {
            // Updates the reminder date for an existing task.
            string sql = @"UPDATE cyber_tasks
                           SET reminder_date = @reminder
                           WHERE task_id = @id;";

            using (MySqlConnection connection =
                   new MySqlConnection(DatabaseConfig.ConnectionString))
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@reminder", reminderDate);
                command.Parameters.AddWithValue("@id", taskId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int taskId)
        {
            // Permanently removes the selected task from MySQL.
            string sql = "DELETE FROM cyber_tasks WHERE task_id = @id;";

            using (MySqlConnection connection =
                   new MySqlConnection(DatabaseConfig.ConnectionString))
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", taskId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}