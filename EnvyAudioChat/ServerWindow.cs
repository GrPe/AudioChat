using System.Windows;

namespace EnvyAudioChat
{
    public partial class MainWindow : Window
    {
        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            int index = IpServerSelectBox.SelectedIndex;
            server.Run(addresses[index]);
            RunButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            StatusLabel.Content = "Server running";
            volumeSlider.Value = volumeSlider.Maximum / 2f;
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            server.Stop();
            RunButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            StatusLabel.Content = "Server stopped";
        }
    }
}
