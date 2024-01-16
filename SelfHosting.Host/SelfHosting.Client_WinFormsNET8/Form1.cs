using SelfHosting.Contracts;
using System.Reflection.Metadata;
using System.ServiceModel;

namespace SelfHosting.Client_WinFormsNET8
{
    public partial class Form1 : Form
    {
        ChannelFactory<IPizzaService> channelFactory;

        public Form1()
        {
            InitializeComponent();
            //var netTcp = new NetTcpBinding();
            //netTcp.ReliableSession.Enabled = true;
            //netTcp.ReliableSession.Ordered = true;
            //channelFactory = new ChannelFactory<IPizzaService>(netTcp, new EndpointAddress("net.tcp://localhost:1"));


            var httBasic = new BasicHttpBinding();
            channelFactory = new ChannelFactory<IPizzaService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:2"));

            //var ws = new WSHttpBinding();
            //ws.Security.Mode = SecurityMode.None;
            //ws.ReliableSession.Enabled = true;
            //ws.ReliableSession.Ordered = true;
            //channelFactory = new ChannelFactory<IPizzaService>(ws, new EndpointAddress("http://localhost:3"));
        }

        private void loadbutton_Click(object sender, EventArgs e)
        {
            try
            {
                var client = channelFactory.CreateChannel(new EndpointAddress("http://localhost:2"));
                dataGridView1.DataSource = client.GetAllPizzas();
            }
            catch (FaultException<OutOfPizzaError> ex)
            {
                MessageBox.Show($"Keine Pizza mehr {ex.Detail.WerIstSchuld} hat alle gegessen");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void orderbutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is Pizza p)
            {
                try
                {
                    var client = channelFactory.CreateChannel();
                    client.OrderPizza((int)numericUpDown1.Value, p);
                    var result = "VOID";
                    MessageBox.Show($"Kosten {result}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void massOrderButton_Click(object sender, EventArgs e)
        {
            var pizza = new Pizza() { Id = 99, Name = "Massenpizza", Price = 99.99m };
            var client = channelFactory.CreateChannel();

            for (int i = 0; i < 1000; i++)
            {
                var count = i;
                //Task.Run(() =>
                //{
                client.OrderPizza(count, pizza);
                //});
            }

        }
    }
}
