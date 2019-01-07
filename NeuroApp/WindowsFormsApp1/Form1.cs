using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using ArduinoUploader;
using ArduinoUploader.Hardware;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private String[] constPorts;
        List<String> listPort;
        public static String portName;
        public Form1()
        {
            InitializeComponent();
            constPorts = SerialPort.GetPortNames();
            listPort = constPorts.ToList();
            listPort.ForEach(Console.WriteLine);
            //getAvailablePortNames(comboBox1);
            label2.Visible = false;
        }

        public void getAvailablePortNames(object obj)
        {
            String[] ports = SerialPort.GetPortNames();
            ((ComboBox)obj).Items.Clear();

            foreach (string port in ports)
            {
                ((ComboBox)obj).Items.Add(port);
            }
            if (((ComboBox)obj).Items.Count > 0)
            {
                ((ComboBox)obj).SelectedIndex = 0;
            }
            else
            {
                ((ComboBox)obj).Text = " ";
            }
        }
        //Open eeprom program
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (comboBox1.Text == "")
                {
                    label5.Visible = false;
                    label4.Visible = true;
                    //label2.Visible = true;
                    //label2.Text = "Porfavor selecciona el puerto";
                }
                else
                {
                    portName = comboBox1.Text;
                    if (listPort.Contains(portName))
                    {
                        //label2.Visible = true;
                        //label2.Text = "Puerto incorrecto, seleccione el correcto";
                        label4.Visible = false;
                        label5.Visible = true;
                    }
                    else
                    {
                        label4.Visible = false;
                        label5.Visible = false;
                        var uploader = new ArduinoSketchUploader(
                            new ArduinoSketchUploaderOptions()
                            {
                                FileName = @"C:\eeprom_clear\eeprom_clear.ino.standard.hex",
                                PortName = portName,
                                ArduinoModel = ArduinoModel.NanoR3
                            });
                        this.timer1.Start();
                        this.label3.Visible = true;
                        progressBar1.Visible = true;
                        uploader.UploadSketch();
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                label2.Text = "Acceso denegado al puerto";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Visible = false;
            label2.Text = "";
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            getAvailablePortNames(comboBox1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Increment(2);
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
                this.progressBar1.Visible = false;
                this.label3.Visible = false;
                popForm2 popup = new popForm2();
                DialogResult dialogResult = popup.ShowDialog();
                progressBar1.Value = 0;
                popup.Dispose();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
