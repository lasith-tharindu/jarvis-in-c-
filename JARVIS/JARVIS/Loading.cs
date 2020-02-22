using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;


namespace JARVIS
{
    public partial class Loading : Form
    {
        WindowsMediaPlayer player = new WindowsMediaPlayer();

        public Loading()
        {
            InitializeComponent();
            this.TransparencyKey = (BackColor);
           player.URL = @"E:\Esoft\C#\JARVIS\JARVIS\bin\Debug\Music\ironMan.mp3";
        }

        int[] targetColor = { 0, 255, 255 };
        int[] fadeRGB = new int[3];
        private void Loading_Load(object sender, EventArgs e)
        {
            
            label1.ForeColor = Color.FromArgb(this.BackColor.R, this.BackColor.G, this.BackColor.B);
            this.timer1.Start();
            player.controls.play();

            Speaker.Speak("INITIALISING ....");
            Speaker.Speak("activating biometric and face recognition process");
            Speaker.Speak("activating sissies  ");
            Speaker.Speak("currently loading system files from your drives");
            Speaker.Speak("currently getting all necessary files From Your drives");
            Speaker.Speak(" checking current internet status ");
            Speaker.Speak("access granted");
            Speaker.Speak(" Your Pc is now true connected");
            Speaker.Speak("getting current Windows  versions");
            Speaker.Speak(" access granted");
            Speaker.Speak("your Windows version is Microsoft Windows  ");
            Speaker.Speak("loading graphical interface");
            Speaker.Speak("graphical interface process is 78%");
            Speaker.Speak("graphical interface process is 92%");
            Speaker.Speak("graphical interface process is completed");
            Speaker.Speak("getting access to Window");
            Speaker.Speak("Windows  traditional management");
            Speaker.Speak("loading successfully");
            Speaker.Speak("you and I connect to the Windows PC");
            //Speaker.Speak("and your username Is Lasith Tharindu");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            this.progressBar1.Increment(1);
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            fadeIn();
        }
        void fadeOut()
        {
            fadeRGB[0] = label1.ForeColor.R;
            fadeRGB[1] = label1.ForeColor.G;
            fadeRGB[2] = label1.ForeColor.B;

            if (fadeRGB[0] > this.BackColor.R)
                fadeRGB[0]--;
            else if (fadeRGB[0] < this.BackColor.R)
                fadeRGB[0]++;
            if (fadeRGB[1] > this.BackColor.G)
                fadeRGB[1]--;
            else if (fadeRGB[1] < this.BackColor.G)
                fadeRGB[1]++;
            if (fadeRGB[2] > this.BackColor.B)
                fadeRGB[2]--;
            else if (fadeRGB[2] < this.BackColor.B)
                fadeRGB[2]++;
            if (fadeRGB[0] == this.BackColor.R && fadeRGB[1] == this.BackColor.G && fadeRGB[2] == this.BackColor.B)
                timer2.Stop();
            label1.ForeColor = Color.FromArgb(fadeRGB[0], fadeRGB[1], fadeRGB[2]);
        }
        void fadeIn()
        {
            fadeRGB[0] = label1.ForeColor.R;
            fadeRGB[1] = label1.ForeColor.G;
            fadeRGB[2] = label1.ForeColor.B;

            if (fadeRGB[0] > targetColor[0])
                fadeRGB[0]--;
            else if (fadeRGB[0] < targetColor[0])
                fadeRGB[0]++;
            if (fadeRGB[1] > targetColor[1])
                fadeRGB[1]--;
            else if (fadeRGB[1] < targetColor[1])
                fadeRGB[1]++;
            if (fadeRGB[2] > targetColor[2])
                fadeRGB[2]--;
            else if (fadeRGB[2] < targetColor[2])
                fadeRGB[2]++;
            if (fadeRGB[0] == targetColor[0] && fadeRGB[1] == targetColor[1] && fadeRGB[2] == targetColor[2])
                timer2.Stop();
            label1.ForeColor = Color.FromArgb(fadeRGB[0], fadeRGB[1], fadeRGB[2]);
        }
    }
}
