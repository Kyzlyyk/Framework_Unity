using System.Collections.Generic;
using UnityEngine;

namespace Kyzlyk.Helpers.Utils
{
    public static class Templater
    {
        public static Vector2[] ConvertTemplate(byte[][] template, Vector2 offset = default)
        {
            List<Vector2> converted = new(template.Length);
            
            for (int y = 0; y < template.Length; y++)
            {
                for (int x = 0; x < template[y].Length; x++)
                {
                    if (template[y][x] == 0) continue;
                    
                    converted.Add(new Vector2(x, y) + offset);
                }
            }

            return converted.ToArray();
        }
    }
}
