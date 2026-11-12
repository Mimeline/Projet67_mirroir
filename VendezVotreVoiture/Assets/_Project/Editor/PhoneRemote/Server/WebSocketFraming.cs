// Protocole WebSocket minimal (RFC 6455) : réponse de poignée de main, lecture d'une trame texte masquée,
// écriture d'une trame binaire. Le Mono de Unity n'offre pas les WebSockets de HttpListener.
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PhoneRemote.Server
{
    public static class WebSocketFraming
    {
        const string Guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        public static byte[] Handshake(string key)
        {
            var accept = Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(key.Trim() + Guid)));
            return Encoding.ASCII.GetBytes("HTTP/1.1 101 Switching Protocols\r\nUpgrade: websocket\r\n" +
                $"Connection: Upgrade\r\nSec-WebSocket-Accept: {accept}\r\n\r\n");
        }

        public static string ReadText(Stream stream)
        {
            var head = ReadExact(stream, 2);
            int opcode = head[0] & 0x0F;
            long length = head[1] & 0x7F;
            if (length == 126) { var b = ReadExact(stream, 2); length = (b[0] << 8) | b[1]; }
            else if (length == 127) { var b = ReadExact(stream, 8); length = 0; foreach (var x in b) length = (length << 8) | x; }
            var mask = (head[1] & 0x80) != 0 ? ReadExact(stream, 4) : new byte[4];
            var payload = ReadExact(stream, (int)length);
            for (int i = 0; i < payload.Length; i++) payload[i] ^= mask[i % 4];
            if (opcode == 0x8) return null;
            return opcode == 0x1 ? Encoding.UTF8.GetString(payload) : "";
        }

        public static void WriteBinary(Stream stream, byte[] data)
        {
            var header = new MemoryStream();
            header.WriteByte(0x82);
            if (data.Length < 126) header.WriteByte((byte)data.Length);
            else if (data.Length < 65536) { header.WriteByte(126); header.WriteByte((byte)(data.Length >> 8)); header.WriteByte((byte)data.Length); }
            else { header.WriteByte(127); for (int s = 56; s >= 0; s -= 8) header.WriteByte((byte)((long)data.Length >> s)); }
            stream.Write(header.ToArray(), 0, (int)header.Length);
            stream.Write(data, 0, data.Length);
            stream.Flush();
        }

        static byte[] ReadExact(Stream stream, int count)
        {
            var buffer = new byte[count];
            for (int read = 0; read < count;)
            {
                int n = stream.Read(buffer, read, count - read);
                if (n <= 0) throw new EndOfStreamException();
                read += n;
            }
            return buffer;
        }
    }
}
