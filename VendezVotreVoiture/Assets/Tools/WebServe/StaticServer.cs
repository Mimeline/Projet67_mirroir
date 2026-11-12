// Petit serveur de fichiers statiques sur le réseau local (port 8766), types MIME de Unity Web.
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Tools.WebServe
{
    public static class StaticServer
    {
        public const int Port = 8766;

        static readonly Dictionary<string, string> Types = new Dictionary<string, string>
        {
            { ".html", "text/html; charset=utf-8" }, { ".js", "application/javascript" },
            { ".wasm", "application/wasm" }, { ".css", "text/css" }, { ".png", "image/png" },
            { ".ico", "image/x-icon" }, { ".json", "application/json" },
        };

        static HttpListener listener;

        public static void Start(string root)
        {
            Stop();
            listener = new HttpListener();
            listener.Prefixes.Add($"http://*:{Port}/");
            listener.Start();
            var full = Path.GetFullPath(root);
            Task.Run(() => Loop(full));
        }

        public static void Stop()
        {
            listener?.Close();
            listener = null;
        }

        static async Task Loop(string root)
        {
            while (listener != null && listener.IsListening)
            {
                HttpListenerContext context;
                try { context = await listener.GetContextAsync(); }
                catch (Exception) { return; }
                var relative = Uri.UnescapeDataString(context.Request.Url.AbsolutePath).TrimStart('/');
                var path = Path.GetFullPath(Path.Combine(root, relative == "" ? "index.html" : relative));
                var found = path.StartsWith(root) && File.Exists(path);
                context.Response.StatusCode = found ? 200 : 404;
                if (found)
                {
                    var ext = Path.GetExtension(path.EndsWith(".unityweb") ? path.Substring(0, path.Length - 9) : path);
                    context.Response.ContentType = Types.TryGetValue(ext, out var t) ? t : "application/octet-stream";
                    var bytes = File.ReadAllBytes(path);
                    await context.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length);
                }
                context.Response.Close();
            }
        }
    }
}
