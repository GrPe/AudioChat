using System.Windows;

namespace EnvyAudioChat
{
    public partial class MainWindow : Window
    {
        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                client.Connect(IpAdressInputBox.Text);
                client.Start();
            }
            catch
            {
                StatusLabel.Content = "Server unreachable";
                return;
            }
            StatusLabel.Content = "Connected";
            ConnectButton.IsEnabled = false;
            DisconnectButton.IsEnabled = true;
            volumeSlider.Value = volumeSlider.Maximum / 2f;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client.Stop();
            StatusLabel.Content = "Disconnected";
            DisconnectButton.IsEnabled = false;
            ConnectButton.IsEnabled = true;
        }
    }
}