using System.Net;
using System.Windows;

namespace EnvyAudioChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Server server;
        Client client;
        IPAddress[] addresses;

        public MainWindow()
        {
            InitializeComponent();
            server = new Server();
            client = new Client();
            addresses = server.GetAddressList();

            foreach (var a in addresses)
                IpServerSelectBox.Items.Add(a.ToString());

            PreSet();
        }

        private void PreSet()
        {
            StopButton.IsEnabled = false;
            DisconnectButton.IsEnabled = false;
            volumeSlider.Value = volumeSlider.Maximum / 2f;
        }
    }
}
