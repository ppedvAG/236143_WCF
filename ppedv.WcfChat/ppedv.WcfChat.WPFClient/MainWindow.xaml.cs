using Microsoft.Win32;
using ppedv.WcfChat.Contracts;
using System.IO;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace ppedv.WcfChat.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public partial class MainWindow : Window, IChatClient
    {
        IChatServer server;

        public MainWindow()
        {
            InitializeComponent();
            nameTb.Text = $"Fred_{Guid.NewGuid().ToString().Substring(0, 4)}";
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            var tcp = new NetTcpBinding();
            tcp.Security.Mode = SecurityMode.None;
            //tcp.MaxReceivedMessageSize = int.MaxValue;


            tcp.ReliableSession.Enabled = true;
            tcp.ReliableSession.Ordered = true;
            var tcpAdr = "net.tcp://172.22.197.201:1";


            var chf = new DuplexChannelFactory<IChatServer>(new InstanceContext(this), tcp, tcpAdr);
            //chf.Credentials.ClientCertificate.Certificate.
            server = chf.CreateChannel();

            server.Login(nameTb.Text);
        }

        private void SendText(object sender, RoutedEventArgs e)
        {
            server?.SendText(msgTb.Text);
        }

        private void SendImage(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog() { Title = "Wähle ein Bild", Filter = "Bild|*.png;*.jpg;*.gif|Alle Dateien|*.*" };

            if (dlg.ShowDialog().Value)
            {
                using (var stream = File.OpenRead(dlg.FileName))
                {
                    server.SendImage(stream);
                }
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            server?.Logout();
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

        public void ShowUserlist(IEnumerable<string> users)
        {
            usersLb.ItemsSource = users;
        }

        public void ShowText(string msg)
        {

            //chatLb.Dispatcher.Invoke(() => { chatLb.Items.Add(msg); });
            chatLb.Items.Add(msg);
        }

        public void ShowImage(Stream steam)
        {
            var ms = new MemoryStream();

            steam.CopyTo(ms);
            ms.Position = 0;
            var img = new Image();
            img.BeginInit();
            img.Source = BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            img.Stretch = System.Windows.Media.Stretch.None;
            img.EndInit();

            chatLb.Items.Add(img);
        }

        private void mutiSend(object sender, RoutedEventArgs e)
        {
            server?.StartMultiSend();
        }

        private void mutiText(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                server.SendText($"{i} {RandomString()}");
            }
        }

        public static string RandomString()
        {
            var random = new Random();
            var length = random.Next(100, 100);
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}