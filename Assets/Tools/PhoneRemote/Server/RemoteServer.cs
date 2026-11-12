// Serveur du téléphone distant sur TCP (port 8765) : sert la page en HTTP, accepte les téléphones en WebSocket.
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PhoneRemote.Server
{
    public static class RemoteServer
    {
        public const int Port = 8765;

        static TcpListener listener;
        static readonly ConcurrentDictionary<ClientSocket, bool> clients = new ConcurrentDictionary<ClientSocket, bool>();

        public static void Start(string pagePath, Action<string> onMessage)
        {
            listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();
            var page = File.ReadAllBytes(pagePath);
            Task.Run(() => AcceptLoop(page, onMessage));
        }

        public static void Stop()
        {
            foreach (var client in clients.Keys) client.Close();
            clients.Clear();
            listener?.Stop();
            listener = null;
        }

        public static void Broadcast(byte[] frame)
        {
            foreach (var client in clients.Keys)
            {
                if (client.IsOpen) client.SendFrame(frame);
                else clients.TryRemove(client, out _);
            }
        }

        static async Task AcceptLoop(byte[] page, Action<string> onMessage)
        {
            while (listener != null)
            {
                TcpClient tcp;
                try { tcp = await listener.AcceptTcpClientAsync(); }
                catch (Exception) { return; }
                _ = Task.Run(() => Handle(tcp, page, onMessage));
            }
        }

        static void Handle(TcpClient tcp, byte[] page, Action<string> onMessage)
        {
            tcp.NoDelay = true;
            var stream = tcp.GetStream();
            string key = null;
            var reader = new StreamReader(stream, Encoding.ASCII, false, 1, true);
            for (var line = reader.ReadLine(); !string.IsNullOrEmpty(line); line = reader.ReadLine())
                if (line.StartsWith("Sec-WebSocket-Key:", StringComparison.OrdinalIgnoreCase)) key = line.Substring(18);
            if (key == null)
            {
                var head = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nContent-Type: text/html; charset=utf-8\r\nContent-Length: {page.Length}\r\nConnection: close\r\n\r\n");
                stream.Write(head, 0, head.Length);
                stream.Write(page, 0, page.Length);
                tcp.Close();
                return;
            }
            var reply = WebSocketFraming.Handshake(key);
            stream.Write(reply, 0, reply.Length);
            var client = new ClientSocket(tcp, stream);
            clients[client] = true;
            client.ReceiveLoop(onMessage);
            clients.TryRemove(client, out _);
        }
    }
}
