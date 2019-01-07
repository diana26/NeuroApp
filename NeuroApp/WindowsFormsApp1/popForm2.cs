using ArduinoUploader;
using ArduinoUploader.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public partial class popForm2 : Form
    {
        SerialPort myPort;
        public popForm2()
        {
            InitializeComponent();
            //PopUpForm pop1 = new PopUpForm();
            //pop1.Dispose();
        }

        private void popForm2_Load(object sender, EventArgs e)
        {

        }

        private void ReceivedSerialHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            this.Invoke((MethodInvoker)delegate
            {
                label1.Text = sp.ReadExisting() + " Luxes";
            });
            myPort.Close();
        }
        //Open luxes calibration
        private void button3_Click(object sender, EventArgs e)
        {
            myPort = new SerialPort();
            myPort.PortName = Form1.portName;
            myPort.BaudRate = 9600;
            myPort.Open();
            myPort.Write("Luxes");
            myPort.Close();
            myPort.DataReceived += new SerialDataReceivedEventHandler(ReceivedSerialHandler);
            myPort.Open();
        }
        //Open Neurolink program
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.label2.Visible = true;
                this.progressBar1.Visible = true;
                this.timer1.Start();
                var uploader = new ArduinoSketchUploader(
                    new ArduinoSketchUploaderOptions()
                    {
                        FileName = @"C:\NeuroLinkFirmware\NeuroLinkFirmware.ino.eightanaloginputs.hex",
                        PortName = Form1.portName,
                        ArduinoModel = ArduinoModel.NanoR3
                    });
                uploader.UploadSketch();
            }
            catch(UnauthorizedAccessException)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(2);
            if(progressBar1.Value == 100)
            {             
                progressBar1.Visible = false;
                this.label2.Visible = false;
                timer1.Stop();
                this.Dispose();
                progressBar1.Value = 0;
                PopUp3 popup3 = new PopUp3();
                DialogResult dialogResult = popup3.ShowDialog();
            }
        }
        //Cancel
        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
