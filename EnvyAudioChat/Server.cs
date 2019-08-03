using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EnvyAudioChat
{
    public class Server
    {
        private Socket listener;
        private Socket client;
        private AudioSocket audioSocket;

        public IPAddress[] GetAddressList()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            return hostEntry.AddressList;
        }

        public void Run(IPAddress address)
        {
            listener = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(address, Config.Port);
            listener.Bind(endPoint);
            listener.Listen(10);

            Task.Run(() => StartListening());
        }

        private void StartListening()
        {
            client = listener.Accept();
            audioSocket = new AudioSocket(client);
            audioSocket.Start();
        }

        public void Stop()
        {
            audioSocket?.Stop();
            audioSocket?.Dispose();
            audioSocket = null;
            listener?.Close();
            client?.Close();
        }

        public void SetVolume(float volume)
        {
            audioSocket?.SetVolume(volume);
        }
    }
}
