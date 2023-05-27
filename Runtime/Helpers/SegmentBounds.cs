using UnityEngine;

namespace Kyzlyk.Helpers
{
    public struct Segment
    {
        public Segment(Vector2 a, Vector2 b)
        {
            A = a;
            B = b;
        }

        public Vector2 A;
        public Vector2 B;
    }
    
    public readonly struct SegmentBounds
    {
        public SegmentBounds(Segment[] segments)
        {
            Segments = segments;
        }

        public readonly Segment[] Segments;

        public bool Contains(Vector2 position)
        {
            for (int i = 0; i < Segments.Length; i++)
            {
                if ((position.x >= Segments[i].A.x && position.x <= Segments[i].B.x)
                    && (position.y >= Segments[i].A.y && position.y <= Segments[i].B.y))
                {
                    return true;
                }
            }

            return false;
        }
    }
}