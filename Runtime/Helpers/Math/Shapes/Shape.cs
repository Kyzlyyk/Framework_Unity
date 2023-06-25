using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public abstract class Shape
    {
        public abstract Vector2 Size { get; }
        public abstract Quaternion Rotation { get; }
        public abstract Vector2 Center { get; }

        public abstract float GetPerimeter();
        public abstract float GetArea();
    }
}
