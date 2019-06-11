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

namespace ping_ping
{
    public partial class Form2 : Form
    {

        Form1 frm1 = new Form1();
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                string font_form = Properties.Settings.Default.font_lbl;
                // this.label1.Font = new System.Drawing.Font(font_form, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.label1.Font = frm1.label1.Font;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //this.txthost.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            // font
            //try
            //{
            //    string font_form = Properties.Settings.Default.font_lbl;
            //    this.label1.Font = new System.Drawing.Font(font_form, 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //    this.label1.Font = frm1.fontDialog1.Font;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //// mine
           
           // label1.Text = await work();
        }

        private Task<string> work()
        {
            return Task.Run(() =>
             {
                 string host = Properties.Settings.Default.host_ip;
                 int time_out = Properties.Settings.Default.time_out;

                 int data = Properties.Settings.Default.byte_Data;
                 byte[] byte_data = new byte[data];

                 Ping ping = new Ping();
                 PingReply reply = ping.Send(host, time_out, byte_data);
                 return reply.RoundtripTime.ToString(); ;
             });
        }

        private void button1_Click(object sender, EventArgs e)
        {

            frm1.Show();
        }
    }
}
