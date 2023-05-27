using UnityEngine;

namespace Kyzlyk.Helpers
{
    public struct Clip
    {
        public Clip(float frames, float delay)
        {
            Frames = frames;
            Delay = delay;
        }

        public Clip(bool isDefault)
        {
            Frames = 60;
            Delay = .1f;
        }

        public float Frames { get; set; }
        public float Delay { get; set; }

        public short GetFrameRate()
        {
            return (short)(1f / Time.unscaledDeltaTime);
        }

        public float GetOneFrameDuration()
        {
            return Frames * Delay;
        }

        public float GetClipDuration()
        {
            return Delay / Frames;
        }
    }
}