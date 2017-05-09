using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Speech.Synthesis;

using System.Threading.Tasks;

namespace AISA.Core
{
    public static class OpenSpeak
    {
        private static SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public static bool IsInitialized { get; set; }

        /// <summary>
        /// Initialize the OpenSpeak Engine
        /// </summary>
        public static void Init()
        {
            synthesizer.SelectVoice("Kate");
            synthesizer.Volume = 100;

            synthesizer.Rate = 0;
        }

        public static void Speak(string Text, bool OnSameThread = false)
        {
            if (IsInitialized)
            {
                if (!OnSameThread)
                    synthesizer.SpeakAsync(Text);
                else
                    synthesizer.Speak(Text);
            }
            else
            {
                //Recur until the engine is initialized
                Init();
                Speak(Text);
            }
        }

    }
}
