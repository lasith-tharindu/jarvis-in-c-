using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Xml;
using WMPLib;
using System.Diagnostics;

namespace JARVIS
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();
        private SpeechRecognitionEngine engine;
        private SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        string Temperature;
        string Condition;
        string Humidity;
        string WindSpeed;
        string Town;
        string TFCond;
        string TFHigh;
        string TFLow;

        
        private WebBrowser browese;
        private bool isJarvisListening = true;
        public Form1()
        {
            InitializeComponent();
            player.URL = @"E:\Esoft\C#\JARVIS\JARVIS\bin\Debug\Music\ironMan (3).mp3";
        }

        private void LoadSpeech()
        {
            try
            {
                engine = new SpeechRecognitionEngine();
                engine.SetInputToDefaultAudioDevice();
            
                Choices cNumbers = new Choices();
                for(int i = 0; i<100; i++)
                cNumbers.Add(i.ToString());

                Choices c_commandsOfSystem = new Choices();
                c_commandsOfSystem.Add(GrammarRules.whatTimeIs.ToArray());//whatTimeIs
                c_commandsOfSystem.Add(GrammarRules.WhatDateIs.ToArray());//what is today
                c_commandsOfSystem.Add(GrammarRules.JarvisStartListening.ToArray());//jarvis start listening
                c_commandsOfSystem.Add(GrammarRules.JarvisStopListening.ToArray());// jarvis stop listening
                c_commandsOfSystem.Add(GrammarRules.MinimizeWindow.ToArray());// Minimize window
                c_commandsOfSystem.Add(GrammarRules.NormalWindow.ToArray());
                c_commandsOfSystem.Add(GrammarRules.WhoareYOu.ToArray());
                c_commandsOfSystem.Add(GrammarRules.OpenProgram.ToArray());
                c_commandsOfSystem.Add(GrammarRules.Computer.ToArray());
                c_commandsOfSystem.Add(GrammarRules.close.ToArray());
                 c_commandsOfSystem.Add(GrammarRules.Play.ToArray());
                 c_commandsOfSystem.Add(GrammarRules.restart.ToArray());
                c_commandsOfSystem.Add(GrammarRules.GetWeather.ToArray());
                c_commandsOfSystem.Add(GrammarRules.chrome.ToArray());
                c_commandsOfSystem.Add(GrammarRules.music.ToArray());
                c_commandsOfSystem.Add(GrammarRules.vs.ToArray());
                c_commandsOfSystem.Add(GrammarRules.vscode.ToArray());
                c_commandsOfSystem.Add(GrammarRules.firefox.ToArray());
                c_commandsOfSystem.Add(GrammarRules.rider.ToArray());
                c_commandsOfSystem.Add(GrammarRules.notepad.ToArray());
                c_commandsOfSystem.Add(GrammarRules.notepad1.ToArray());
                c_commandsOfSystem.Add(GrammarRules.commandpom.ToArray());
                c_commandsOfSystem.Add(GrammarRules.word.ToArray());

                GrammarBuilder gb_commandsOfsystem = new GrammarBuilder();
                gb_commandsOfsystem.Append(c_commandsOfSystem);
                Grammar g_commandsOfSystem = new Grammar(gb_commandsOfsystem);
                g_commandsOfSystem.Name = "sys";

                GrammarBuilder gbNumber = new GrammarBuilder();
                gbNumber.Append(cNumbers);
                gbNumber.Append(new Choices("time", "more", "any less", "per"));
                gbNumber.Append(cNumbers);


                Grammar gNumbers = new Grammar(gbNumber);
                gNumbers.Name = "calc";

                engine.LoadGrammar(g_commandsOfSystem);
                engine.LoadGrammar(gNumbers);

               // engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(words))));
                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(audioLevel);
                engine.SpeechRecognitionRejected +=new EventHandler<SpeechRecognitionRejectedEventArgs>(rej);

                synthesizer.SpeakStarted += new EventHandler<SpeakStartedEventArgs>(speakStarted);
                synthesizer.SpeakProgress += new EventHandler<SpeakProgressEventArgs>(speakProgress);

                engine.RecognizeAsync(RecognizeMode.Multiple);
                Speak("");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Occurred at loadspeech():" + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            LoadSpeech();
            player.controls.play();
            //Speak(" Hello Sir");

        }

        private void GetWeather()
        {
            string query = String.Format("http://weather.yahooapis.com/forecastrss?w=2434216");
            XmlDocument wData = new XmlDocument();
            wData.Load(query);

            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
            manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

            XmlNode channel = wData.SelectSingleNode("rss").SelectSingleNode("channel");
            XmlNodeList nodes = wData.SelectNodes("/rss/channel/item/yweather:forecast", manager);

            Temperature = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;

            Condition = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;

            Humidity = channel.SelectSingleNode("yweather:atmosphere", manager).Attributes["humidity"].Value;

            WindSpeed = channel.SelectSingleNode("yweather:wind", manager).Attributes["speed"].Value;

            Town = channel.SelectSingleNode("yweather:location", manager).Attributes["city"].Value;

            TFCond = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["text"].Value;

            TFHigh = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["high"].Value;

            TFLow = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["low"].Value;
        }

        private void rec(object s, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            float conf = e.Result.Confidence;
            string date = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();
            string log_filename = "log\\"+ date + ".txt";

            StreamWriter sw = File.AppendText(log_filename);
            if (File.Exists(log_filename))
                sw.WriteLine(speech);
            else
            {
                sw.WriteLine(speech);
            }

            sw.Close();

            if (conf > 0.35f)
            {
                this.label1.BackColor = Color.DarkCyan;
                this.label1.ForeColor = Color.LawnGreen;
                
                if (GrammarRules.JarvisStopListening.Any(x=>x== speech))
                {
                    isJarvisListening = false;
                }
                else if (GrammarRules.JarvisStartListening.Any(x => x == speech))
                {
                    isJarvisListening = true;
                    Speak("Hello Sir", "Yes Sir..","I'm here","I'm here");
                }
                if (isJarvisListening == true)
                {
                    switch (e.Result.Grammar.Name)
                    {
                        case "sys":
                            if (GrammarRules.whatTimeIs.Any(x => x == speech))
                            {
                                Runner.whatTimeIs();
                            }
                            else if (GrammarRules.WhatDateIs.Any(x => x == speech))
                            {
                                Runner.WhatDateIs();
                            }
                            else if (GrammarRules.MinimizeWindow.Any(x=>x== speech)){
                                MinimizeWindow();
                            }
                            else if (GrammarRules.NormalWindow.Any(x => x == speech))
                            {
                                NormalWindow();
                            }else if(GrammarRules.WhoareYOu.Any(x=> x == speech))
                            {
                                WhoareYOu();
                            }
                            else if (GrammarRules.Computer.Any(x => x == speech))
                            {
                                Computet();
                            }
                            else if (GrammarRules.Googlr.Any(x=> x==speech))
                            {
                                Googlr();
                            }
                            else if(GrammarRules.close.Any(x=>x== speech))
                            {
                                close();
                            }
                            else if (GrammarRules.Play.Any(x => x == speech))
                            {
                                Play();
                            }
                            else if (GrammarRules.restart.Any(x => x == speech))
                            {
                                restart();
                            }
                            else if (GrammarRules.music.Any(x => x == speech))
                            {
                                music();
                            }
                             else if (GrammarRules.vs.Any(x => x == speech))
                             {
                                vs();
                             }
                             else if (GrammarRules.chrome.Any(x => x == speech))
                             {
                                chrome();
                             }

                             else if (GrammarRules.notepad.Any(x => x == speech))
                            {
                                notepad();
                            }
                            else if (GrammarRules.OpenProgram.Any(x => x == speech))
                            {
                                switch (speech)
                                {
                                    case "browser":
                                        browese = new WebBrowser();
                                       
                                        break;

                                }
                            }
                            
                            break;
                            
                            

                        case "calc":

                            break;
                    }

                }

            }
            else
            {
                this.label1.ForeColor = Color.Orange;
            }
            
        }

         private void notepad()
        {
            Speak("opening notepad");
            Process.Start(@"C:\WINDOWS\system32\notepad.exe");
        }
         private void chrome()
        {
            Speak("opening chrome");
            Process.Start(@"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe");
        }
         private void vs()
         {
            Speak("opening visual studio 2017");
            Process.Start(@"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe");
         }
        private void music()
        {
            Speak("opening AIMP");
            Process.Start(@"C:\Program Files (x86)\AIMP3\AIMP.exe");
            
        }
         public void restart()
        {
            System.Windows.Forms.Application.Restart();
            this.Close();
        }
        
        private void Play()
        {
            { 
            Speak("yes sir...Playing some music.");

                player.URL = @"E:\Songs\English\Audio\The Official UK Top 40 Singles Chart 16-12-2012\01 James Arthur - Impossible.mp3";
                player.controls.play();

            }

        }
        private void Googlr()
        {
            
        }

        private void Computet()
        {
            Speak("I can read email, weather report, i can search web for you, i can fix and tell you about your appointments, anything that you need" +
                "like a persoanl assistant do, you can ask me question i will reply to you");
        }

        private void WhoareYOu()
        {
            Speak("Great, and you ? ");
        }

        private void close()
        {
           Application.Exit() ;
         //   Speak("Exiting application");
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void audioLevel(object s, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;

        }

        private void rej(object s,SpeechRecognitionRejectedEventArgs e)
        {
            this.label1.ForeColor = Color.Red;
        }

        private void Speak(string text)
        {
            synthesizer.SpeakAsync(text);
        }
        private void Speak(params string[] texts)
        {
            Random r = new Random();
            Speak(texts[r.Next(0, texts.Length)]);
        }
        private void speakStarted(object s, SpeakStartedEventArgs e)
        {
           // lblJarvis.Text = "JARVIS :";
        }

        private void speakProgress(object s,SpeakProgressEventArgs e)
        {
          //  lblJarvis.Text += e.Text + "";
        }
        private void MinimizeWindow()
        {
            if (this.WindowState == FormWindowState.Normal || this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Minimized;
                Speak("Minimizando a Window", "As You Wish", "As you wish, I'll do it.");
            }
            else
            {
                Speak("it's already minimized", " the window is minimized", " already did that");
            }
        }

        private void NormalWindow()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                Speak("leaving the window to normal size", "as you wish", "I will do this", "okay", "I'll do it");
            }
            else
            {
                Speak("the window is already full size", "already did that", "This has already been done");
            }
        }
        
        private void browser(object s,WebBrowser e)
        {
            browese.Navigate("google.com");
        }
       
    }
}
