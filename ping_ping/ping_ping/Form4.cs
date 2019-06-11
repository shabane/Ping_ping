using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech;
using System.Speech.Synthesis;
namespace ping_ping
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.voice_rate = Convert.ToInt32(numericUpDownspeed.Text);
            Properties.Settings.Default.voice_volum = Convert.ToInt32(numericUpDownvolum.Text);
            Properties.Settings.Default.say = Convert.ToInt32(numericUpDowncash.Text);
            Properties.Settings.Default.Save();
            MessageBox.Show("Setting Saved");
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            numericUpDownspeed.Text = Properties.Settings.Default.voice_rate.ToString(); ;
            numericUpDownvolum.Text = Properties.Settings.Default.voice_volum.ToString();
            numericUpDowncash.Text = Properties.Settings.Default.say.ToString();
            toolTip1.SetToolTip(numericUpDowncash, "Say Ping In Each Time ");
            toolTip1.SetToolTip(numericUpDownvolum, "Volume Of Ping Reader");
            toolTip1.SetToolTip(numericUpDownspeed, "Speed Of Ping Reader In Each Time");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer voice = new SpeechSynthesizer();
            voice.SpeakAsyncCancelAll();
            voice.Volume = Properties.Settings.Default.voice_volum;
            voice.Rate = Properties.Settings.Default.voice_rate;
            voice.SpeakAsync("This Is A Test ");
        }

        private void ToolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
