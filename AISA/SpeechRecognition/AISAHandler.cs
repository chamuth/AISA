using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Speech.Recognition;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using AISA.Core;

namespace AISA.SpeechRecognition
{
    public static class AISAHandler
    {
        [Serializable]
        public class MicrophoneNotWorkingException : Exception
        {
            public MicrophoneNotWorkingException() { }
            public MicrophoneNotWorkingException(string message) : base(message) { }
            public MicrophoneNotWorkingException(string message, Exception inner) : base(message, inner) { }
            protected MicrophoneNotWorkingException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

        private static SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        private static Action AISACallback;
        private static Action AISAEndCallback;
        private static Action<string, string, bool> AISAResultCallback;

        /// <summary>
        /// Initialize the AISA Handler
        /// </summary>
        /// <param name="start">Lambda Expression that executes when AISA is recognized</param>
        /// <param name="end">Lambda Expression when the program command is executed</param>
        public static void Initialize(Action start, Action end, Action<string, string, bool> result)
        {
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "AISA" }))));
            _recognizer.SpeechRecognized += _recognizer_SpeechRecognized;

            try
            {
                _recognizer.SetInputToDefaultAudioDevice();
            }
            catch (Exception)
            {
                throw new MicrophoneNotWorkingException("Please make sure your microphone is connected");
            }

            //Save the functions
            AISACallback = start;
            AISAEndCallback = end;
            AISAResultCallback = result;
        }

        public static void Start(bool first = false)
        {
            if (ViewControllerConnector.PaperStarted == false)
            {
                try
                {
                    if (first == true)
                    {
                        var da = new DispatcherTimer();
                        da.Interval = TimeSpan.FromSeconds(4);
                        da.Tick += (a, b) =>
                        {
                            da.Stop();

                            try
                            {
                                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
                            }
                            catch (Exception) { }
                        };

                        da.Start();
                    }
                    else
                    {
                        _recognizer.RecognizeAsync(RecognizeMode.Multiple);
                    }
                }
                catch (Exception) { }
            }
        }

        public static void Pause()
        {
            _recognizer.RecognizeAsyncCancel();
        }

        private static void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (ViewControllerConnector.PaperStarted == false)
            {
                if (e.Result.Text == "AISA")
                {
                    AudioHandler.Start();
                    AISACallback();
                    Pause();
                    var rec = new Recognizer(AISAEndCallback, AISAResultCallback);
                }
            }
        }
    }
}
