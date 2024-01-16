using SelfHosting.Contracts;
using System.ServiceModel;
using System.Windows;

namespace SelfHosting.Client_WpfFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ChannelFactory<IPizzaService> channelFactory;

        public MainWindow()
        {
            InitializeComponent();

            channelFactory = new ChannelFactory<IPizzaService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:1"));
        }

        private void Laden(object sender, RoutedEventArgs e)
        {
            var client = channelFactory.CreateChannel();
            grid.ItemsSource = client.GetAllPizzas();
        }

        private void Bestellen(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is Pizza p)
            {
                var client = channelFactory.CreateChannel();
                //var result = client.OrderPizza((int)sl1.Value, p);

                //MessageBox.Show($"Kosten {result:c}");
            }
        }
    }
}
