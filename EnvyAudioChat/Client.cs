using System.Net.Sockets;

namespace EnvyAudioChat
{
    public class Client
    {
        private Socket socket;
        private AudioSocket audioSocket;

        public void Connect(string ipAddress)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ipAddress, Config.Port);
            audioSocket = new AudioSocket(socket);
        }

        public void Start()
        { 
            audioSocket.Start();
        }

        public void Stop()
        {
            audioSocket.Stop();
            audioSocket.Dispose();
            audioSocket = null;
            socket?.Close();
        }
        public void SetVolume(float volume)
        {
            audioSocket?.SetVolume(volume);
        }
    }
}
