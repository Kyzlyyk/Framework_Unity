using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Kyzlyk.LSGSystem.Breaking;
using Kyzlyk.Helpers;

namespace Kyzlyk.LSGSystem.Layout
{
    public struct ChunkGenerator
    {
        public static void GenerateFromStart(byte[][] space, int width, int height, Builder builder, Vector2 offset = new())
        {
            Vector2 currentPoint = offset;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (space[i][j] != 0)
                        builder.SpawnGMaterial(currentPoint, new GMaterialTexture(), false);
                    
                    currentPoint.x++;
                }

                currentPoint.x = offset.x;
                currentPoint.y--;
            }

            builder.Apply();
        }
        
        public static void GenerateFromEnd(byte[][] space, int width, int height, Builder builder, Vector2 offset = new())
        {
            Vector2 currentPoint = offset;

            for (int i = height - 1; i >= 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    if (space[i][j] != 0)
                        builder.SpawnGMaterial(currentPoint, new GMaterialTexture(), false);
                    
                    currentPoint.x++;
                }

                currentPoint.x = offset.x;
                currentPoint.y++;
            }

            builder.Apply();
        }

        public static KeyValuePair<Vector2, bool>[] GenerateWithPlatforms(int width, int height, Builder builder, int randomCoefficient, Vector2 offset = new())
        {
            if (randomCoefficient == 0)
                return new KeyValuePair<Vector2, bool>[0];

            List<KeyValuePair<Vector2, bool>> space = new(width * height);
            Vector2 currentPosition = offset;

            if (randomCoefficient >= 100)
            {
                GenerateFromEnd(Enumerable.Repeat<byte[]>(Enumerable.Repeat<byte>(1, width).ToArray(), height).ToArray(), width, height, builder, offset);
            }
            
            Range<int> rangeLengthOfPlatform = new(2, 5);

            int GetRandomPlatformLength()
                => Random.Range(rangeLengthOfPlatform.StartValue, rangeLengthOfPlatform.EndValue + 1);

            bool BuildNewPlatform()
                => Random.Range(0, 100) <= randomCoefficient;

            for (int i = 0; i < height; i++)
            {
                int maxPlatformLength = GetRandomPlatformLength();
                
                int platformCountOnRow = 0;
                int currentPlatformLength = 0;
                
                bool endOfBuildingCurrentPlatform = !BuildNewPlatform();

                for (int j = 0; j < width; j++)
                {
                    bool skip = false;

                    if (!skip && endOfBuildingCurrentPlatform)
                    {
                        if (BuildNewPlatform())
                            endOfBuildingCurrentPlatform = false;
                        
                        skip = true;
                    }

                    if (!skip && currentPlatformLength == maxPlatformLength)
                    {
                        maxPlatformLength = GetRandomPlatformLength();
                        endOfBuildingCurrentPlatform = true;
                        currentPlatformLength = 0;
                        platformCountOnRow++;

                        skip = true;
                    }

                    if (!skip)
                    {
                        builder.SpawnGMaterial(currentPosition, new GMaterialTexture(), false);
                        currentPlatformLength++;
                    }

                    space.Add(new KeyValuePair<Vector2, bool>(currentPosition, true));
                    currentPosition.x++;
                }

                currentPosition.x = offset.x;
                currentPosition.y++;
            }

            builder.Apply();
            return space.ToArray();
        }

        public static void GenerateRandom(int width, int height, Vector2 startPoint, Builder builder, int randomCoef)
        {
            Vector2 currentPosition = startPoint;   

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int random = Random.Range(0, 100);

                    if (random <= randomCoef)
                        builder.SpawnGMaterial(currentPosition, new GMaterialTexture(), false);
                    
                    currentPosition.x++;
                }

                currentPosition.x = startPoint.x;
                currentPosition.y--;
            }

            builder.Apply();
        }
    }
}