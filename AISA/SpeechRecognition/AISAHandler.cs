using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Speech.Recognition;
using System.Windows;
using System.Windows.Media;

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

        /// <summary>
        /// Initialize the AISA Handler
        /// </summary>
        /// <param name="start">Lambda Expression that executes when AISA is recognized</param>
        /// <param name="end">Lambda Expression when the program command is executed</param>
        public static void Initialize(Action start, Action end)
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
        }

        public static void Start()
        {
            _recognizer.RecognizeAsync(RecognizeMode.Single);
        }

        public static void Pause()
        {
            _recognizer.RecognizeAsyncCancel();
        }

        private static void _recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "AISA")
            {
                AudioHandler.Start();
                AISACallback();

                var rec = new Recognizer(AISAEndCallback);
            }
        }
        //private static void InstantiateAISA()
        //{
        //    _recognizer = new SpeechRecognitionEngine();
        //    _recognizer.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(new string[] { "AISA" }))));
        //    _recognizer.SpeechRecognized += speech_Recognized;

        //    _recognizer.SetInputToDefaultAudioDevice();
        //    _recognizer.RecognizeAsync(RecognizeMode.Single);
        //}

        //public static void Start(Action aisaCallback, Action aisaRevCallback)
        //{
        //    InstantiateAISA();
        //    AISACallback = aisaCallback;
        //}

        //public static void Start()
        //{
        //    InstantiateAISA();
        //}

        //private static void speech_Recognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    if (e.Result.Text.Contains("AISA")) 
        //    {
        //        //Play start audio
        //        AudioHandler.Start();
        //        AISACallback();

        //        Console.WriteLine("AISA Module Started");
        //    }
        //}
    }
}
