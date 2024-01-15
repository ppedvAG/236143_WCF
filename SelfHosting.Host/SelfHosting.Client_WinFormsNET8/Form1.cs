using SelfHosting.Contracts;
using System.ServiceModel;

namespace SelfHosting.Client_WinFormsNET8
{
    public partial class Form1 : Form
    {
        ChannelFactory<IPizzaService> channelFactory;

        public Form1()
        {
            InitializeComponent();

            channelFactory = new ChannelFactory<IPizzaService>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:1"));
        }

        private void loadbutton_Click(object sender, EventArgs e)
        {
            var client = channelFactory.CreateChannel();
            dataGridView1.DataSource = client.GetAllPizzas();
        }

        private void orderbutton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem is Pizza p)
            {
                var client = channelFactory.CreateChannel();
                var result = client.OrderPizza((int)numericUpDown1.Value, p);

                MessageBox.Show($"Kosten {result:c}");
            }
        }
    }
}
