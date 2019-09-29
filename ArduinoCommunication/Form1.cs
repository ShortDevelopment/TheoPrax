using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint RegisterWindowMessage(string lpString);
        
        SerialPort actualSerialPort = new SerialPort();
        string[] availableSerialPorts;
        public Form1()
        {
            InitializeComponent();
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == RegisterWindowMessage("Show:ArduinoCommunication")) {
                this.Show();
                this.WindowState = FormWindowState.Maximized;
                this.BringToFront();
            }
            base.WndProc(ref m);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            actualSerialPort = new SerialPort(availableSerialPorts[PortAuswahlBox.SelectedIndex]);
            actualSerialPort.BaudRate = 9600;
            actualSerialPort.DataReceived += ActualSerialPort_DataReceived;
            actualSerialPort.Open();
            actualSerialPort.WriteLine("s");
            panel1.Hide();
            panel2.Show();
            


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
            
            panel1.Location = new Point(this.Width / 2 - panel1.Width / 2, this.Height / 2 - panel1.Height / 2);
            this.KeyPreview = true;
            button1.Enabled = false;
            AktualisiereDropdown();
           





        }

        private void ActualSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string message;
            this.Invoke((MethodInvoker)delegate {
                System.Diagnostics.Debug.Print("---Hallo");
                message = actualSerialPort.ReadLine();
                if (message != "t1:nan")
                {
                    if (message.Contains("p1:"))
                    {
                        message = message.Split(new string[] { ":" }, StringSplitOptions.None)[1];
                        DrucksensorOutput1.Text = message;
                    }
                    if (message.Contains("p2:"))
                    {
                        message = message.Split(new string[] { ":" }, StringSplitOptions.None)[1];
                        DrucksensorOutput2.Text = message;
                    }
                    if (message.Contains("t1:"))
                    {
                        message = message.Split(new string[] { ":" }, StringSplitOptions.None)[1];
                        TemperaturSensorOutput1.Text = message;
                    }
                    if (message.Contains("t2:"))
                    {
                        message = message.Split(new string[] { ":" }, StringSplitOptions.None)[1];
                        TemperaturSensorOutput2.Text = message;
                    }
                }
                
            });
            
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Application.Exit();
            }
        }

        private void PortAuswahlBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            availableSerialPorts = SerialPort.GetPortNames();
            if (PortAuswahlBox.SelectedItem != null && PortAuswahlBox.SelectedItem != "")
            {
                button1.Enabled = true;
                button1.BackColor = Color.FromArgb(255, 255, 0, 0);
                
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Hide();
        }

        private void PortAuswahlBox_MouseClick(object sender, MouseEventArgs e)
        {
            PortAuswahlBox.Items.Clear();
            AktualisiereDropdown();
        }
        public void AktualisiereDropdown()
        {
            ManagementObjectCollection ManObjReturn;
            ManagementObjectSearcher ManObjSearch;
            ManObjSearch = new ManagementObjectSearcher("Select * from Win32_SerialPort");
            ManObjReturn = ManObjSearch.Get();

            foreach (ManagementObject ManObj in ManObjReturn)
            {
                var a = PortAuswahlBox.Items.Add(ManObj["Name"].ToString());

                if (PortAuswahlBox.Items[a].ToString().ToLower().Contains("arduino uno"))
                {
                    PortAuswahlBox.SelectedItem = a;
                }
            }
        }
    }
}
