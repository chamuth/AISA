using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Speech.Recognition;
using System.Windows;
using System.Speech.Synthesis;
using AISA.Core;
using System.Windows.Threading;

namespace AISA.SpeechRecognition
{
    public class Recognizer
    {
        private SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();
        private Action _End = null;
        private Action<string, string> ResultCallback;
        private DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Creates a new Speech Recognizer Object
        /// </summary>
        public Recognizer(Action end, Action<string, string> result)
        {
            //Inform the ViewcontrollerConnector
            ViewControllerConnector.StartedCommandRecognition = true;

            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(CommandHandler.GetCommands()))));

            _recognizer.SpeechRecognized += speech_Recognized;
            _recognizer.SpeechDetected += _recognizer_SpeechDetected;
            _recognizer.SpeechRecognitionRejected += _recognizer_SpeechRecognitionRejected;
            _recognizer.SpeechHypothesized += _recognizer_SpeechHypothesized;

            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeAsync(RecognizeMode.Single);

            synthesizer.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            synthesizer.Volume = 100;

            synthesizer.Rate = 0;

            _End = end;

            ResultCallback = result;

            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();

            //Stop recognition
            StopRecognition();
        }

        private void _recognizer_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            ViewControllerConnector.ChangeHypothesis(e.Result.Text);
        }

        private void _recognizer_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            //Reset the preview panel
            ViewControllerConnector.None();
        }

        private void StopRecognition()
        {
            _End();

            //Play wrong audio
            AudioHandler.Wrong();

            //Say that it's wrong
            var result_string = CommandHandler.Handle("ERR:WRONG");
            synthesizer.SpeakAsync(result_string);
            ResultCallback("Couldn't recognize", result_string);

            //Null the recognizer
            _recognizer = null;
            //Start AISA Handler again
            AISAHandler.Start();
        }

        private void _recognizer_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            //Inform the ViewcontrollerConnector
            ViewControllerConnector.StartedCommandRecognition = false;

            StopRecognition();
            timer.Stop();
        }

        private void speech_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            //Inform the ViewcontrollerConnector
            ViewControllerConnector.StartedCommandRecognition = false;

            //Activate the ending function
            _End();

            //Play results audio
            AudioHandler.Results();

            //Speak the answer from the CommandHandler
            var result_string = CommandHandler.Handle(e.Result.Text);
            if (!result_string.StartsWith("SUDO:"))
            {
                ResultCallback(e.Result.Text, result_string);
            }else
            {
                ResultCallback(e.Result.Text, "Here's what I've got");
            }
            
            synthesizer.SpeakAsync(result_string.Replace("SUDO:", ""));
            //Null the recognizer
            _recognizer = null;

            //Start AISA Handler again
            AISAHandler.Start(true);
            timer.Stop();
        }

    }
}
