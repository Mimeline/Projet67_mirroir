// Un téléphone connecté : envoie les images (une seule en attente), reçoit les messages tactiles.
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneRemote.Server
{
    public class ClientSocket
    {
        readonly TcpClient tcp;
        readonly Stream stream;
        int sending;

        public ClientSocket(TcpClient tcp, Stream stream) { this.tcp = tcp; this.stream = stream; }

        public bool IsOpen => tcp.Connected;

        public void SendFrame(byte[] frame)
        {
            if (Interlocked.Exchange(ref sending, 1) == 1) return;
            Task.Run(() =>
            {
                try { WebSocketFraming.WriteBinary(stream, frame); }
                catch (Exception) { Close(); }
                Interlocked.Exchange(ref sending, 0);
            });
        }

        public void ReceiveLoop(Action<string> onMessage)
        {
            try
            {
                for (var text = WebSocketFraming.ReadText(stream); text != null; text = WebSocketFraming.ReadText(stream))
                    if (text.Length > 0) onMessage(text);
            }
            catch (Exception) { }
            Close();
        }

        public void Close() => tcp.Close();
    }
}
