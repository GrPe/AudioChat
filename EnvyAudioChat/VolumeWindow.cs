using System.Windows;

namespace EnvyAudioChat
{
    public partial class MainWindow : Window
    {
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float volume = GetVolume();
            server.SetVolume(volume);
            client.SetVolume(volume);
        }

        private float GetVolume()
        {
            if (volumeSlider.Value < volumeSlider.Maximum / 2f)
                return (float)(volumeSlider.Value * 2f / volumeSlider.Maximum);
            else
                return (float)(volumeSlider.Value - (volumeSlider.Maximum / 2f));
        }
    }
}
