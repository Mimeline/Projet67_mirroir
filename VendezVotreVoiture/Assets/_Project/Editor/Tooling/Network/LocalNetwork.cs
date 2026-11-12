// Adresse IPv4 du PC sur le réseau local, pour les liens à ouvrir depuis un téléphone.
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Project.Tooling.Network
{
    public static class LocalNetwork
    {
        public static string Address() => NetworkInterface.GetAllNetworkInterfaces()
            .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .SelectMany(n => n.GetIPProperties().UnicastAddresses).Select(a => a.Address)
            .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)?.ToString() ?? "localhost";
    }
}
