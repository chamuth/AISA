using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Speech.Recognition;
using System.Windows;
using System.Speech.Synthesis;

namespace AISA.SpeechRecognition
{
    public class Recognizer
    {
        private SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private Action _End = null;

        /// <summary>
        /// Creates a new Speech Recognizer Object
        /// </summary>
        public Recognizer(Action end)
        {
            _recognizer.LoadGrammar(new Grammar(
                new GrammarBuilder(
                    new Choices(CommandHandler.GetCommands())
                    )
                )
            );

            _recognizer.SpeechRecognized += speech_Recognized;
            _recognizer.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;

            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeAsync(RecognizeMode.Single);

            synthesizer.SelectVoice("Kate");
            synthesizer.Volume = 100;

            synthesizer.Rate = 0;

            _End = end;

            Console.WriteLine("Recognizer Started");
        }

        private void _recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            _End();

            //Say that it's wrong
            synthesizer.SpeakAsync(CommandHandler.Handle("ERR:WRONG"));

            //Null the recognizer
            _recognizer = null;
            //Start AISA Handler again
            AISAHandler.Start();
        }

        private void speech_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            //Activate the ending function
            _End();

            //Play results audio
            AudioHandler.Results();

            //Speak the answer from the CommandHandler
            synthesizer.SpeakAsync(CommandHandler.Handle(e.Result.Text));

            //Null the recognizer
            _recognizer = null;

            //Start AISA Handler again
            AISAHandler.Start();
        }

    }
}
