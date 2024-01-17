

using System;
using System.Drawing;
using System.IO;
using System.ServiceModel;
using System.Windows.Forms;
using WcfStream.Contracts;

namespace WcfStream.ClientWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var tcp = new NetTcpBinding();
            tcp.TransferMode = TransferMode.Streamed;
            tcp.MaxReceivedMessageSize = int.MaxValue;
            tcp.MaxConnections = 100;
            tcp.MaxBufferSize = int.MaxValue;
            tcp.MaxBufferPoolSize = int.MaxValue;

            tcp.ReceiveTimeout = TimeSpan.FromSeconds(5);
            tcp.SendTimeout = TimeSpan.FromSeconds(5);



            var chf = new ChannelFactory<IStreamingSample>(tcp, new EndpointAddress("net.tcp://localhost:1"));

            server = chf.CreateChannel();
            communicationObject = (ICommunicationObject)server;
            //communicationObject.Opened += CommunicationObject_Changed;
            communicationObject.Opening += CommunicationObject_Changed;
            communicationObject.Closed += CommunicationObject_Changed;
            communicationObject.Closing += CommunicationObject_Changed;
            communicationObject.Faulted += CommunicationObject_Changed;
        }
        ICommunicationObject communicationObject;

        private void CommunicationObject_Changed(object sender, EventArgs e)
        {
            string aaa = communicationObject.State.ToString();
            //this.Invoke(() => Text = aaa);
            this.Invoke(new MethodInvoker(() => { Text = aaa; }));
        }

        IStreamingSample server;
        int countStart = 0;
        int countend = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            ((Control)sender).Enabled = false;
            label1.Text = $"Start: {countStart++} / Ende: {countend}";
            var pb = new PictureBox();
            using (var stream = server.GetStream(""))
            {
                //using (var ms = new MemoryStream())
                {
                    //stream.CopyTo(ms);
                    //ms.Position  0;
                    var img = new Bitmap(stream);
                    pb.Image = img;
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Height = 100;
                    pb.Width = 100;
                    pb.Dock = DockStyle.Top;
                    panel1.Controls.Add(pb);
                    panel1.Controls.Add(new Label() { Text = $"Start: {countStart} / Ende: {countend}" });
                    label1.Text = $"Start: {countStart} / Ende: {countend++}";
                }
            }
            ((Control)sender).Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Add(new Label() { Text = server.GetString() });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var pppp = server.GetPizzas();

        }
    }
}
