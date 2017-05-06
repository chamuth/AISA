using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AISA
{
    public static class AudioHandler
    {
        private static  void PlayAudio(string input)
        {
            Uri uri = new Uri(System.AppDomain.CurrentDomain.BaseDirectory + "\\Audio\\" + input);
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
    }
}
