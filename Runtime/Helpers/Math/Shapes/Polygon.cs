using Kyzlyk.Helpers.Extensions;
using System.Linq;
using UnityEngine;

namespace Kyzlyk.Helpers.Math
{
    public class Polygon
    {
        public Polygon(Vector2[] points)
        {
            Points = points;
        }

        public readonly Vector2[] Points;

        public bool Contains(Vector2 point)
        {
            int count = 0;
            for (int i = 0, j = Points.Length - 1; i < Points.Length; j = i++)
            {
                if (((Points[i].y > point.y) != (Points[j].y > point.y)) &&
                    (point.x < (Points[j].x - Points[i].x) * (point.y - Points[i].y) / (Points[j].y - Points[i].y) + Points[i].x))
                {
                    count++;
                }
            }

            return count.IsOdd();
        }
        
        public static bool Contains(Vector2[] polygon, Vector2 point)
        {
            int count = 0;
            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if (((polygon[i].y > point.y) != (polygon[j].y > point.y)) &&
                    (point.x < (polygon[j].x - polygon[i].x) * (point.y - polygon[i].y) / (polygon[j].y - polygon[i].y) + polygon[i].x))
                {
                    count++;
                }
            }

            return count.IsOdd();
        }
    }
}