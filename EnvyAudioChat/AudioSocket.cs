using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace EnvyAudioChat
{

    public class AudioSocket : IDisposable
    {
        private Socket socket;
        private WaveInEvent waveIn;
        private WaveOutEvent waveOut;

        private VolumeSampleProvider volumeProvider;
        private float volume = 1f;

        public AudioSocket(Socket socket)
        {
            this.socket = socket;
        }

        public void Start()
        {
            waveIn = new WaveInEvent();
            waveOut = new WaveOutEvent();

            Task.Run(() => Listen());
            Task.Run(() => Send());
        }

        public void Stop()
        {
            socket.Close();
            waveIn.StopRecording();
        }

        private void Send()
        {
            waveIn.DataAvailable += (s, a) =>
            {
                socket.Send(a.Buffer, a.BytesRecorded, SocketFlags.None);
            };

            waveIn.RecordingStopped += (s, a) =>
            {
                waveIn?.Dispose();
                waveOut?.Dispose();
            };

            waveIn.StartRecording();
        }


        private void Listen()
        {
            var provider = new RawSourceWaveStream(new BufferedStream(new NetworkStream(socket)), waveIn.WaveFormat).ToSampleProvider();
            volumeProvider = new VolumeSampleProvider(provider)
            {
                Volume = volume
            };
            waveOut.Init(volumeProvider);
            waveOut.Play();
        }

        public void SetVolume(float vol)
        {
            volume = vol;
            if (volumeProvider != null)
                volumeProvider.Volume = volume;
        }

        public void Dispose()
        {
            Stop();
            socket?.Dispose();
        }
    }
}
