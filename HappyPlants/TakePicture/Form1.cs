using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace TakePicture
{
    public partial class Form1 : Form
    {
        private string _path;
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;

        private NewFrameEventHandler newFrameEventHandler;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(string path) : this()
        {
            _path = path;            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FinalFrame = new VideoCaptureDevice(CaptureDevice[comboBox1.SelectedIndex].MonikerString);

            newFrameEventHandler = new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.NewFrame += newFrameEventHandler;
            FinalFrame.Start();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox2.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in CaptureDevice)
            {
                comboBox1.Items.Add(Device.Name);
            }
            comboBox1.SelectedIndex = 0;
            FinalFrame = new VideoCaptureDevice();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
              
                pictureBox1.Image = (Bitmap)pictureBox2.Image.Clone();

                if (string.IsNullOrEmpty(_path))
                {
                    return;
                }

                pictureBox1.Image.Save(_path);
            }
            catch(Exception)
            {
                throw;
            }
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (FinalFrame.IsRunning)
            {
                FinalFrame.NewFrame -= newFrameEventHandler;                

                FinalFrame.SignalToStop();
                FinalFrame.WaitForStop();
            }
        }
    }
}
