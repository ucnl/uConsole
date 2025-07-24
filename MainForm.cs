using System;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UCNLDrivers;
using UCNLNMEA;
using UCNLUI;

namespace uConsole
{
    public partial class MainForm: Form
    {
        #region Properties

        string instanceID = "1";
        int childID = 1;


        string PortName
        {
            get => portCbx.SelectedItem?.ToString();
            set => UIHelpers.TrySetCbxItem(portCbx, value.ToString());
        }

        BaudRate PortBaudrate
        {
            get => (BaudRate)Enum.Parse(typeof(BaudRate), baudrateCbx.SelectedItem.ToString());
            set => UIHelpers.TrySetCbxItem(baudrateCbx, value.ToString());
        }

        Random rnd;

        NMEASerialPort port;

        bool IsPortInitialized { get => (port != null) && (port.IsOpen); }

        bool isPingPongMode = false;

        #endregion

        #region Constructor

        public MainForm()
        {
            #region Early init

            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
                instanceID = args[1];

            string vString = string.Format("{0} v{1} (#{2}), {3}",
                Application.ProductName,
                Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                instanceID,
                MDates.GetReferenceNote());
            
            #endregion


            InitializeComponent();

            var brates = Enum.GetNames(typeof(BaudRate));

            baudrateCbx.Items.Clear();
            baudrateCbx.Items.AddRange(brates);
            PortBaudrate = BaudRate.baudRate9600;

            for (int i = 0; i < 8; i++)
            {
                int size = 2 << i;
                var title = string.Format("{0} bytes", size);

                var makeRandomNewItem = makeRandomDataBtn.DropDownItems.Add(title);
                makeRandomNewItem.Tag = size;
                makeRandomNewItem.Click += MakeRandomData_Click;

                var sendRandomNewItem = sendRandomDataBtn.DropDownItems.Add(title);
                sendRandomNewItem.Tag = size;
                sendRandomNewItem.Click += SendRandomData_Click;                
            }

            ConnectionStateChanged(false);
            RefreshPorts();

            rnd = new Random();

            this.Text = vString;
            textHistoryTxb_TextChanged(textHistoryTxb, null);
        }        

        #endregion

        #region Methods

        private void InvokeAppendLine(string line)
        {
            if (textHistoryTxb.InvokeRequired)
            {
                textHistoryTxb.Invoke((MethodInvoker)delegate { AppendLine(line); });
            }
            else
            {
                AppendLine(line);
            }
        }

        private void AppendLine(string line)
        {
            DateTime now = DateTime.Now;

            textHistoryTxb.AppendText(string.Format("{0:00}:{1:00}:{2:00}.{3:000} : {4}",
                now.Hour,
                now.Minute,
                now.Second,
                now.Millisecond,
                line));

            if (!line.EndsWith("\r\n"))
                textHistoryTxb.AppendText("\r\n");
        }


        private string GetHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sb.AppendFormat("{0:X2}", data[i]);

            return sb.ToString();
        }

        private string SafeGetString(byte[] data)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                if ((data[i] == 0x0D) ||
                    (data[i] == 0x0A) ||
                    (data[i] >= 0x20))
                    sb.Append(Convert.ToChar(data[i]));
                else
                    sb.AppendFormat("0x{0:X2}", data[i]);
            }

            return sb.ToString();
        }


        private void OnIncoming(byte[] data)
        {
           InvokeAppendLine(string.Format("({0}) >> {1}", port.PortName, SafeGetString(data)));

            if (isPingPongMode)
                Send(data);
        }

        private void OnOutcoming(string line)
        {
            InvokeAppendLine(string.Format("({0}) << {1}", port.PortName, line));
        }

        private void OnOutcoming(byte[] data)
        {
            InvokeAppendLine(string.Format("({0}) << {1}", port.PortName, GetHexString(data)));
        }

        private void ProcessException(Exception ex, bool isMsgBox)
        {

            AppendLine(ex.Message);

            if (isMsgBox)
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private string GenerateRandomString(int size)
        {
            byte[] buffer = new byte[size / 2];
            rnd.NextBytes(buffer);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.AppendFormat("{0:X2}", buffer[i]);
            }

            return sb.ToString();
        }

        private void ConnectionStateChanged(bool connected)
        {
            portCbx.Enabled = !connected;
            baudrateCbx.Enabled = !connected;
            refreshPortsBtn.Enabled = !connected;

            linkBtn.Checked = connected;

            sendRandomDataBtn.Enabled = connected;
            crlfBtn.Enabled = connected;
            sendTxb.Enabled = connected;

            linkBtn.Checked = connected;
            connectionStateLbl.Text = connected ? string.Format("CONNECTED TO ({0})", port.PortName) : "IDLE";
        }

        private void RefreshPorts()
        {
            var ports = SerialPort.GetPortNames();

            portCbx.Items.Clear();
            portCbx.Items.AddRange(ports);

            if (portCbx.Items.Count > 0)
                portCbx.SelectedIndex = 0;

            linkBtn.Enabled = portCbx.Items.Count > 0;
        }


        private void Send(string line)
        {
            bool isSent = false;

            try
            {
                port.SendData(line);
                isSent = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }

            if (isSent)
                OnOutcoming(line);
        }

        private void Send(byte[] data)
        {

            bool isSent = false;

            try
            {
                port.SendRaw(data);
                isSent = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }

            if (isSent)
                OnOutcoming(data);
        }

        private void InitPort(string portName, BaudRate baudrate)
        {
            port = new NMEASerialPort(
                new SerialPortSettings(portName, baudrate, Parity.None, DataBits.dataBits8, StopBits.One, Handshake.None));

            port.PortError += (o, e) =>
            {
                InvokeAppendLine(string.Format("({0}) >> {1}", port.PortName, e.EventType));
            };

            port.RawDataReceived += (o, e) =>
            {
                OnIncoming(e.Data);                
            };

            try
            {
                port.Open();
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }
        }

        private void DeInitPort()
        {
            if (IsPortInitialized)
            {
                try
                {
                    port.Close();
                }
                catch (Exception ex)
                {
                    ProcessException(ex, true);
                }
            }
        }

        #endregion

        #region Handlers

        private void refreshPortsBtn_Click(object sender, EventArgs e)
        {
            RefreshPorts();
        }

        private void linkBtn_Click(object sender, EventArgs e)
        {
            if (!IsPortInitialized)
                InitPort(PortName, PortBaudrate);
            else
                DeInitPort();

            ConnectionStateChanged(IsPortInitialized);
        }

        private void MakeRandomData_Click(object sender, EventArgs e)
        {
            sendTxb.Text = GenerateRandomString((int)((ToolStripItem)sender).Tag);
        }

        private void SendRandomData_Click(object sender, EventArgs e)
        {
            Send(GenerateRandomString((int)((ToolStripItem)sender).Tag));
        }

        private void autoscrollBtn_Click(object sender, EventArgs e)
        {
            autoscrollBtn.Checked = !autoscrollBtn.Checked;
        }

        private void textHistoryTxb_TextChanged(object sender, EventArgs e)
        {
            if (autoscrollBtn.Checked)
                textHistoryTxb.ScrollToCaret();

            saveAsBtn.Enabled = !string.IsNullOrEmpty(textHistoryTxb.Text);
            copyToClipboardBtn.Enabled = saveAsBtn.Enabled;
            clearHistoryBtn.Enabled = saveAsBtn.Enabled;
        }

        private void clearHistoryBtn_Click(object sender, EventArgs e)
        {
            textHistoryTxb.Clear();
        }

        private void copyToClipboardBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textHistoryTxb.Text);
        }

        private void crlfBtn_Click(object sender, EventArgs e)
        {            
            Send("\r\n");
        }

        private void sendBinaryBtn_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:

                        if (saveAsBtn.Enabled)
                            saveAsBtn_Click(saveAsBtn, null);
                        e.SuppressKeyPress = true;
                        break;

                    case Keys.L:

                        if (linkBtn.Enabled)
                            linkBtn_Click(linkBtn, null);
                        e.SuppressKeyPress = true;
                        break;

                    case Keys.R:

                        if (refreshPortsBtn.Enabled)
                            refreshPortsBtn_Click(refreshPortsBtn, null);
                        e.SuppressKeyPress = true;
                        break;

                    case Keys.C:

                        if (copyToClipboardBtn.Enabled)
                            copyToClipboardBtn_Click(copyToClipboardBtn, null);
                        e.SuppressKeyPress = true;
                        break;

                    case Keys.D:

                        if (runAnotherInstance.Enabled)
                            runAnotherInstance_Click(runAnotherInstance, null);
                        e.SuppressKeyPress = true;
                        break;

                    case Keys.X:

                        if (clearHistoryBtn.Enabled)
                            clearHistoryBtn_Click(clearHistoryBtn, null);
                        e.SuppressKeyPress = true;
                        break;

                    case Keys.N:

                        if (addNMEA0183ChecksumBtn.Enabled)
                            addNMEA0183ChecksumBtn_Click(addNMEA0183ChecksumBtn, null);
                        e.SuppressKeyPress = true;
                        break;

                    case Keys.Enter:

                        if (crlfBtn.Enabled)
                            crlfBtn_Click(crlfBtn, null);
                        e.SuppressKeyPress = true;
                        break;
                }
            }

            if (!e.SuppressKeyPress)
            {
                if ((sendTxb.Enabled) && (!sendTxb.Focused))
                    sendTxb.Focus();
            }
        }

        private void saveAsBtn_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sDialog = new SaveFileDialog())
            {
                sDialog.Title = "Save history as text...";
                sDialog.DefaultExt = "txt";
                sDialog.Filter = "Text files (*.txt)|*.txt";
                sDialog.FileName = "uConsole_" + StrUtils.GetYMDString() + "_" + StrUtils.GetHMSString();

                if (sDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(sDialog.FileName, textHistoryTxb.Text);
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }
                }
            }
        }

        private void sendTxb_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control && (e.KeyCode == Keys.Enter) && (!string.IsNullOrEmpty(sendTxb.Text)))
            {
                Send(sendTxb.Text);
                sendTxb.Clear();
            }
        }

        private void isPingPongModeBtn_Click(object sender, EventArgs e)
        {
            isPingPongMode = !isPingPongMode;
            isPingPongModeBtn.Checked = isPingPongMode;
        }

        private void runAnotherInstance_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.ExecutablePath,
                    string.Format("{0}-{1}", instanceID, childID));

                childID++;
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }
        }

        private void addNMEA0183ChecksumBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sendTxb.Text))
            {
                if (sendTxb.Text.StartsWith("$") && sendTxb.Text.EndsWith("*"))
                {
                    var chkSum = NMEAParser.GetCheckSum(sendTxb.Text.Substring(1, sendTxb.Text.Length - 2));
                    sendTxb.AppendText(string.Format("{0:X2}", chkSum));
                }
            }
        }

        #endregion
    }
}
