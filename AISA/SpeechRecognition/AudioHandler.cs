using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AISA
{
    /// <summary>
    /// Handles audio playback (SFX) of AISA
    /// </summary>
    public static class AudioHandler
    {
        /// <summary>
        /// Play a specific audio from the input file
        /// </summary>
        /// <param name="input"></param>
        private static  void PlayAudio(string input)
        {
            Uri uri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\Audio\\" + input);
            var player = new MediaPlayer();
            player.Open(uri);
            player.Play();
        }

        public static void Start()
        {
            PlayAudio("Start.mp3");
        }

        public static void Results()
        {
            PlayAudio("Results.mp3");
        }

        public static void Wrong()
        {
            PlayAudio("Wrong.mp3");
        }
    }
}
