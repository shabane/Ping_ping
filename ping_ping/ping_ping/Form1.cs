using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using Microsoft.Win32;
using System.Speech;
using System.Speech.Synthesis;
namespace ping_ping
{
    public partial class Form1 : Form
    {
        int time_cash = 1;

        int v_speed = 5;
        int v_volum = 100;

        SpeechSynthesizer voice = new SpeechSynthesizer();

        PingReply againrp;

        string host = Properties.Settings.Default.host_ip;

        int timeoute = Properties.Settings.Default.time_out;

        int bytte = Properties.Settings.Default.byte_Data;

        int timerspeed = Properties.Settings.Default.timer_speed;

        string save = " ";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            v_speed = Properties.Settings.Default.voice_rate;
            v_volum = Properties.Settings.Default.voice_volum;
            chv.Checked = Properties.Settings.Default.chv;
            btnr.Visible = false;
            //Properties.Settings.Default.host_ip = "8.8.8.8";
            //Properties.Settings.Default.byte_Data = 32;
            chbdf.Checked = Properties.Settings.Default.difult_font;
            chsettingdefult.Checked = Properties.Settings.Default.Setting;
            chbs.Checked = Properties.Settings.Default.startup_run;
            panel_log.Visible = false;
            //button3.Visible = false;
            string font_form = Properties.Settings.Default.font_lbl;
            this.label1.Font = new System.Drawing.Font(font_form, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //        btn1.Visible = false;

            toolTip1.SetToolTip(txtspeed, "Minimum Is 1");
            toolTip1.SetToolTip(txtbyte, "Minimum Is 1" + "\n" + "Maximaize 65,500");


            timer1.Interval = timerspeed;

            notifyIcon1.Visible = true;

            notifyIcon1.Text = "Ping_Ping";

            txthost.Text = Properties.Settings.Default.host_ip;

            txtspeed.Text = Properties.Settings.Default.timer_speed.ToString();

            txtbyte.Text = Properties.Settings.Default.byte_Data.ToString();

            txtreply.Text = Properties.Settings.Default.time_out.ToString();

            //FormBorderStyle = FormBorderStyle.Sizable;

            txtspeed.Text = Properties.Settings.Default.timer_speed.ToString();
            timer1.Interval = Properties.Settings.Default.timer_speed;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Interval = Properties.Settings.Default.timer_speed;
            try
            {

                if (await time() == "0")
                {
                    label1.Text = againrp.Status.ToString();
                }
                else
                {
                    label1.Text = await time();

                    // rich log 

                    richTextBox1.Text += "Host : " + host_log + "  Ping :" + round + " TTL : " + ttl + "  Bytes : " + data_buffer + Environment.NewLine;
                    save += "Host : " + host_log + "  Ping :" + round + " TTL : " + ttl + "  Bytes : " + data_buffer + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Properties.Settings.Default.host_ip = "8.8.8.8";
                txthost.Text = Properties.Settings.Default.host_ip;
                btnr.Visible = false;
                timer1.Enabled = false;
            }
        }


        string ttl;
        string host_log;
        string round;
        string data_buffer;

        private Task<string> time()
        {
            return Task.Run(() =>
            {
                // For Mine Application //
                byte[] date = new byte[bytte];
                Ping ping = new Ping();

                PingReply reply = ping.Send(host, timeoute, date);
                againrp = reply;
                string rtime = reply.RoundtripTime.ToString();

                // log programing
                try
                {
                    ttl = reply.Options.Ttl.ToString();
                    host_log = reply.Address.ToString();
                    round = reply.RoundtripTime.ToString();
                    data_buffer = reply.Buffer.Length.ToString();

                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Properties.Settings.Default.host_ip = "127.0.0.1";
                    // lblhost.Text = Properties.Settings.Default.host_ip;
                }
                //
                return rtime;
            });
        }


        private void button1_Click(object sender, EventArgs e)
        {

            // host
            Properties.Settings.Default.host_ip = host = txthost.Text;
            // time oute
            Properties.Settings.Default.time_out = timeoute = Convert.ToInt32(txtreply.Text);
            // buffer data
            Properties.Settings.Default.byte_Data = bytte = Convert.ToInt32(txtbyte.Text);
            // timer speed

            //  Properties.Settings.Default.timer_speed =
            //timer1.Interval = timerspeed;
            Properties.Settings.Default.timer_speed = Convert.ToInt32(txtspeed.Text);
            timer1.Interval = Properties.Settings.Default.timer_speed;
            // font lbl
            // Properties.Settings.Default.font_lbl = fontDialog1.Font.Name;

            Properties.Settings.Default.difult_font = chbdf.Checked;
            Properties.Settings.Default.Setting = chsettingdefult.Checked;
            Properties.Settings.Default.startup_run = chbs.Checked;
            // save

            Properties.Settings.Default.Save();
            // clear
            lblcolor.Text = " . ";
            lbldata.Text = " . ";
            lblfont.Text = " . ";
            lblhost.Text = " . ";
            lblreply.Text = " . ";
            lblspeed.Text = " . ";

            MessageBox.Show("Setting Saved");
        }

        private void button4_Click(object sender, EventArgs e)
        {


            DialogResult result;
            gb_all.Visible = true;
            lblfont.Text = "Font";
            //gb_all.Visible = true;
            result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.label1.ForeColor = colorDialog1.Color;
                //gb_all.Visible = false;
            }
            else
            {
                gb_all.Visible = true;
            }
            //DialogResult result;


            //lblcolor.Text = "Color +";
            //result = colorDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    gb_all.Visible = false;
            //    this.label1.ForeColor = colorDialog1.Color;
            //}
            //else
            //{

            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result;
            gb_all.Visible = true;
            lblfont.Text = "Font";
            //gb_all.Visible = true;
            result = fontDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.label1.Font = fontDialog1.Font;
                //gb_all.Visible = false;
            }
            else
            {
                gb_all.Visible = true;
            }

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (chbdf.Checked == true)
            {
                this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                label1.Font = fontDialog1.Font;
            }
        }

        private void settingdefult_CheckedChanged(object sender, EventArgs e)
        {
            if (chsettingdefult.Checked == false)
            {

            }
            else
            {
                txthost.Text = host = "8.8.8.8";

                timeoute = 1000;
                txtreply.Text = "1000";

                bytte = 32;
                txtbyte.Text = "32";

                txtspeed.Text = "1000";
                timer1.Interval = 1000;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //for set to top

            this.TopMost = true;
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
            this.Width = 300;
            this.Height = 90;
            FormBorderStyle = FormBorderStyle.None;
            label8.Visible = false;

            this.Width = label1.Width + 100;
            this.Height = label1.Height + 100;
            gb_all.Visible = false;

            this.ShowInTaskbar = false;
        }

        private void txthost_TextChanged(object sender, EventArgs e)
        {
            //lblhost.Text = "Host +";
        }

        private void txtspeed_TextChanged(object sender, EventArgs e)
        {
            //lblspeed.Text = "Speed +";
            if (txtspeed.TextLength == 0 || Convert.ToInt32(txtspeed.Text) <= 0)
            {
                txtspeed.Text = (1).ToString();
            }
        }

        private void txtreply_TextChanged(object sender, EventArgs e)
        {
            //lblreply.Text = "Reply +";
            if (txtreply.TextLength <= 0 || Convert.ToInt32(txtreply.Text) <= 0)
            {
                txtreply.Text = (1).ToString();
            }
        }

        private void txtbyte_TextChanged(object sender, EventArgs e)
        {
            //lbldata.Text = "Data +";
            if (txtbyte.TextLength >= 5)
            {
                txtbyte.Text = (65500).ToString();
            }
            else if (txtbyte.TextLength <= 0 || Convert.ToInt32(txtbyte.Text) <= 0)
            {
                txtbyte.Text = (1).ToString();
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Programing By MOHAMAD_SHABANE_ARANI" + "\n" + "\n" +
                "This App Ping A Host In Specified TIME , DATA , TIME OUT "
                + "\n" + "This App Is Good For Gamers"
                + "\n" + "Develop By Mohamad Shabane Arani"
                               , "MOHAMAD_SHABANE_ARANI"

                );
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel_log.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult result = new DialogResult();
            result = MessageBox.Show("Do You Want To Exit ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {

            }
            else
            {
                Application.Exit();
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // back color defult
            gb_all.Visible = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //
            this.ShowInTaskbar = true;

            this.Width = 450;
            this.Height = 369;
            this.ShowInTaskbar = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnexit.PerformClick();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
            notifyIcon1.BalloonTipText = "Ping_Ping Hide In Try Icon";
            notifyIcon1.BalloonTipTitle = "Ping_Ping";
            notifyIcon1.ShowBalloonTip(4000);


        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            gb_all.Visible = true;


            result = colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.label1.ForeColor = colorDialog1.Color;
                gb_all.Visible = false;
            }
            else
            {
                gb_all.Visible = true;
            }

            //gb_all.Visible = true;
            //btncolor.PerformClick();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            gb_all.Visible = true;
            lblfont.Text = "Font";
            //gb_all.Visible = true;
            result = fontDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.Font = this.label1.Font = fontDialog1.Font;
                gb_all.Visible = false;
            }
            else
            {
                gb_all.Visible = true;
            }


            //btnfont.PerformClick();

            //DialogResult result;
            //result = fontDialog1.
            //if (result == DialogResult.OK)
            //{

            //    this.gb_all.Visible = false;
            //}
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //   Application.Restart();
            panel_log.Visible = true;
            btnlog.PerformClick();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form4 frm4 = new Form4();
            frm4.ShowDialog();
        }

        private void gb_all_Enter(object sender, EventArgs e)
        {

        }

        private void unlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //rtb_log.Visible = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
            this.FormBorderStyle = FormBorderStyle.None;
            label8.Visible = false;
            btnexit.Visible = false;
            button7.Visible = false;
            button2.Visible = false;
            button4.Visible = false;
            panel1.Visible = false;
            label1.Visible = false;
            //this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_log.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            panel_log.Width = 250;
            panel_log.Height = 150;
            button6.Visible = false;
            // groupBox2.Width = 253;
            // groupBox2.Height = 150;
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            panel_log.Visible = false;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (chbs.Checked == true)
            {
                try
                {
                    reg.SetValue("ping_ping", Application.ExecutablePath.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    reg.DeleteValue("ping_ping");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void Button6_Click_1(object sender, EventArgs e)
        {

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                System.IO.File.WriteAllText(saveFileDialog1.FileName + ".txt", save);
            }
            else
            {
            }
        }

        private void SetSettingDefultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            txthost.Text = host = "8.8.8.8";

            timeoute = 1000;
            txtreply.Text = "1000";

            bytte = 32;
            txtbyte.Text = "32";

            txtspeed.Text = "1000";
            timer1.Interval = 1000;

        }

        private void ToolToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void Label1_TextChanged(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                if (chv.Checked == true)
                {
                    if (time_cash % Properties.Settings.Default.say == 0)
                    {
                        voice.SpeakAsyncCancelAll();
                        voice.Volume = Properties.Settings.Default.voice_volum;
                        voice.Rate = Properties.Settings.Default.voice_rate;
                        voice.SpeakAsync(label1.Text);
                    }
                    time_cash += 1;
                }
                else
                {

                }

            });


        }

        private void Chv_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.chv = chv.Checked;
        }

        private void Btnr_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.host_ip = "127.0.0.1";
            Properties.Settings.Default.byte_Data = 32;
            Properties.Settings.Default.timer_speed = 1000;
            Properties.Settings.Default.time_out = 1000;
            Properties.Settings.Default.Save();
            Application.Restart();
        }
    }
}
