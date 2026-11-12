// Transforme les messages tactiles du téléphone (« t id phase x y », coordonnées 0..1 depuis le haut gauche)
// en événements d'un écran tactile virtuel de l'Input System, sur le fil principal.
using System.Collections.Concurrent;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace PhoneRemote.Input
{
    public static class TouchInjector
    {
        static readonly ConcurrentQueue<string> inbox = new ConcurrentQueue<string>();
        static Touchscreen device;

        public static void Receive(string message) => inbox.Enqueue(message);

        public static void Attach()
        {
            device = InputSystem.AddDevice<Touchscreen>("PhoneRemote");
            while (inbox.TryDequeue(out _)) { }
        }

        public static void Detach()
        {
            if (device != null && device.added) InputSystem.RemoveDevice(device);
            device = null;
        }

        public static void Drain()
        {
            while (device != null && inbox.TryDequeue(out var message))
            {
                var parts = message.Split(' ');
                if (parts.Length != 5 || parts[0] != "t") continue;
                InputSystem.QueueStateEvent(device, ToTouch(parts));
            }
        }

        static TouchState ToTouch(string[] parts)
        {
            var c = CultureInfo.InvariantCulture;
            float x = float.Parse(parts[3], c) * Screen.width;
            float y = (1f - float.Parse(parts[4], c)) * Screen.height;
            var phase = parts[2] == "b" ? TouchPhase.Began : parts[2] == "m" ? TouchPhase.Moved
                : parts[2] == "e" ? TouchPhase.Ended : TouchPhase.Canceled;
            return new TouchState { touchId = int.Parse(parts[1], c) + 1, phase = phase, position = new Vector2(x, y) };
        }
    }
}
