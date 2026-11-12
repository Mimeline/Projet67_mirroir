// Capture la vue Game à la fin de chaque image, la réduit, l'encode en JPEG et l'envoie aux téléphones.
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace PhoneRemote.Capture
{
    public class FrameCapture : MonoBehaviour
    {
        const int MaxSide = 720;
        const int Quality = 70;
        const float Interval = 1f / 25f;

        public Action<byte[]> OnFrame;

        RenderTexture screen;
        RenderTexture small;
        Texture2D encoder;
        bool pending;

        IEnumerator Start()
        {
            var wait = new WaitForEndOfFrame();
            while (true)
            {
                yield return wait;
                if (!pending) Grab();
                yield return new WaitForSecondsRealtime(Interval);
            }
        }

        void Grab()
        {
            int w = Screen.width, h = Screen.height;
            float k = Mathf.Min(1f, (float)MaxSide / Mathf.Max(w, h));
            int sw = Mathf.Max(1, Mathf.RoundToInt(w * k)), sh = Mathf.Max(1, Mathf.RoundToInt(h * k));
            if (screen == null || screen.width != w || screen.height != h) screen = Resize(screen, w, h);
            if (small == null || small.width != sw || small.height != sh) small = Resize(small, sw, sh);
            ScreenCapture.CaptureScreenshotIntoRenderTexture(screen);
            bool flip = SystemInfo.graphicsUVStartsAtTop;
            Graphics.Blit(screen, small, new Vector2(1, flip ? -1 : 1), new Vector2(0, flip ? 1 : 0));
            pending = true;
            AsyncGPUReadback.Request(small, 0, TextureFormat.RGBA32, Encode);
        }

        void Encode(AsyncGPUReadbackRequest request)
        {
            pending = false;
            if (request.hasError || OnFrame == null) return;
            if (encoder == null || encoder.width != small.width || encoder.height != small.height)
                encoder = new Texture2D(small.width, small.height, TextureFormat.RGBA32, false);
            encoder.LoadRawTextureData(request.GetData<byte>());
            OnFrame(ImageConversion.EncodeToJPG(encoder, Quality));
        }

        static RenderTexture Resize(RenderTexture old, int w, int h)
        {
            if (old != null) old.Release();
            return new RenderTexture(w, h, 0, RenderTextureFormat.ARGB32);
        }
    }
}
