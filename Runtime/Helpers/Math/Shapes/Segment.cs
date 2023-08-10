using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public readonly struct Segment
    {
        public Segment(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }

        public Vector2 A { get; }
        public Vector2 B { get; }

        public override string ToString()
        {
            return $"{A} :A - {Vector2.Distance(A, B)} - B: {B}";
        }
    }
}
