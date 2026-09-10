using System.Media;


namespace CybersecurityAwarenessBot
{
    internal class VoiceGreeting
    {
        public void PlayGreeting()
        {
            try
            {
                //locate greeting.wav after Visual Studio copies it to the build folder.
                string audioPath = Path.Combine(AppContext.BaseDirectory, "Audio", "greeting.wav");

                //Only attempt playback if the file exists.
                if (File.Exists(audioPath))
                {
                    SoundPlayer player = new SoundPlayer(audioPath);
                    //PlaySync waits until the greeting finishes.
                    player.PlaySync();
                }
                else
                {
                    Console.WriteLine("[Cybot's voice greeting file was not found.]");
                }
            }
            catch (Exception)
            { 
                //Prevents audio errors from crashing the chatbot.
                Console.WriteLine("[Cybot could not play the voice greeting.]");
            }
        }
    }
}
