using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS
{
   public class GrammarRules
    {
        public static IList<string> whatTimeIs = new List<string>()
        {
            "what time is it",
            "tell me the time",
            "Could you tell me what time it is?"
            
        };

        public static IList<String> WhatDateIs = new List<string>()
        {
            "Date today",
            "what day is it",
            "What is the date",
            "Do you know tell me the date today",
        };

        public static IList<string> JarvisStartListening = new List<string>()
        {
            "Hi jarvis",
            "Jarvis",
            "jarvis are you there",
            "are you listening",
            
        };

       

        public static IList<string> JarvisStopListening = new List<string>()
        {
            "stop listening",
            "stop listening to me",
            "stay deaf",
        };

        public static IList<string> MinimizeWindow = new List<string>()
        {
            "Minimize Window",
            "Minimize a Window",
            "Jarvis Minimize a Window"

        };
        public static IList<string> NormalWindow = new List<string>()
        {
            "full size window",
            "leaves the window full size",
            "normal size"
        };
        public static IList<string> WhoareYOu = new List<string>()
        {
                "how are you"
        };

        public static IList<string> OpenProgram = new List<string>()
        {
            "Opening",
            "Starting",
            "browser"
        };

        public static IList<string> GetWeather = new List<string>()
        {
            "What is the Temperature "
        };

        public static IList<string> Computer = new List<string>()
        {
            "what can you do"
        };

        public static IList<string> Googlr = new List<string>()
        {
            "search"
        };
        public static IList<string> close = new List<string>()
        {
            "Exit the application"
        };

         public static IList<string> Play = new List<string>()
        {
            "Play some music",
            "pause the music",
            "next song"
        };

        public static IList<string> restart = new List<string>()
       {
           "restart"
       };

        public static IList<string> chrome = new List<string>()
        {
            "open chrome"
        };

        public static IList<string> music = new List<string>()
        {
            "open music player"
        };

        public static IList<string> vscode = new List<string>()
        {
            "open visual Studio"
        };
        public static IList<string> vs = new List<string>()
        {
            "open visual Studio code"
        };

        public static IList<string> rider = new List<string>()
        {
            "open rider"
        };
        public static IList<string> firefox = new List<string>()
        {
            "open Firefox"
        };

        public static IList<string> notepad = new List<string>()
        {
            "open notepad"
        };
        public static IList<string> commandpom = new List<string>()
        {
            "open cmd"
        };
        public static IList<string> notepad1 = new List<string>()
        {
            "open notepad++"
        };

        public static IList<string> word = new List<string>()
        {
            "open word"
        };
    }
}
