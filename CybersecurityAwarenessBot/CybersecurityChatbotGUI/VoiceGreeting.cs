using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI
{
    internal class VoiceGreeting
    {
            public void PlayGreeting()
            {
                try
                {
                    // Finds Assets/greeting.wav after Visual Studio copies it to output.
                    string audioPath = Path.Combine(
                        AppContext.BaseDirectory,
                        "Assets",
                        "greeting.wav");

                    if (File.Exists(audioPath))
                    {
                        SoundPlayer player = new SoundPlayer(audioPath);

                        // Waits for the short greeting before the conversation begins.
                        player.PlaySync();
                    }
                }
                catch (Exception)
                {
                    // The app continues working even when audio has a problem.
                }
            }
        }
    }

