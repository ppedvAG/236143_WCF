using ppedv.WcfChat.Contracts;
using System.IO;
using System.ServiceModel;
using System.Windows;

namespace ppedv.WcfChat.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IChatClient
    {
        IChatServer server;

        public MainWindow()
        {
            InitializeComponent();
            nameTb.Text = "Fred";
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var tcp = new NetTcpBinding();
            tcp.ReliableSession.Enabled = true;
            var tcpAdr = "net.tcp://localhost:1";

            var chf = new DuplexChannelFactory<IChatServer>(new InstanceContext(this), tcp, tcpAdr);
            server = chf.CreateChannel();

            server.Login(nameTb.Text);
        }

        private void SendText(object sender, RoutedEventArgs e)
        {

        }

        private void SendImage(object sender, RoutedEventArgs e)
        {

        }



        private void Logout(object sender, RoutedEventArgs e)
        {

        }

        public void LoginResponse(bool ok, string msg)
        {
            if (ok)
            {
                chatLb.Items.Add(msg);
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        public void ShowUserlist(IEnumerator<string> users)
        {
            throw new NotImplementedException();
        }

        public void ShowText(string msg)
        {
            throw new NotImplementedException();
        }

        public void ShowImage(Stream steam)
        {
            throw new NotImplementedException();
        }
    }
}