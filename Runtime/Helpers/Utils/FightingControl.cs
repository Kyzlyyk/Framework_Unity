using System;
using UnityEngine;

namespace Kyzlyk.Helpers.Utils
{
    /// <summary>
    /// A collection helpers functions for fighting.
    /// </summary>
    internal struct FightingControl
    {
#nullable enable
        public static (Transform?, float) FindNearestEntity(Transform[] entities, Vector2 origin)
        {
            if (entities == null || entities.Length == 0)
                return (null, -1);

            float nearestDistance = -1;
            float currentDistance;

            Transform nearestEnemy = entities[0];

            for (int i = 0; i < entities.Length; i++)
            {
                currentDistance = Vector2.Distance(origin, entities[i].transform.position);

                if (nearestDistance < currentDistance)
                {
                    nearestDistance = currentDistance;

                    nearestEnemy = entities[i];
                }
            }

            return (nearestEnemy, nearestDistance);
        }

        [Obsolete]
        public static (Transform?, float) FindNearestObject(Transform[] entities, Vector2 origin, bool isRightSide)
        {
            if (entities == null || entities.Length == 0)
                return (null, -1);

            Transform[] sideEnemies = new Transform[entities.Length];

            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] == null)
                    continue;

                if (isRightSide && origin.x < entities[i].transform.position.x)
                    sideEnemies[i] = entities[i];

                else if (!isRightSide && origin.x > entities[i].transform.position.x)
                    sideEnemies[i] = entities[i];
            }

            Transform nearestEnemy = sideEnemies[0];

            float currentDistance;

            float nearestDistance = Vector2.Distance(origin, sideEnemies[0].transform.position);

            for (int i = 1; i < sideEnemies.Length; i++)
            {
                if (sideEnemies[i] == null)
                    continue;

                currentDistance = Vector2.Distance(origin, sideEnemies[i].transform.position);

                if (currentDistance < nearestDistance)
                {
                    nearestDistance = currentDistance;

                    nearestEnemy = sideEnemies[i];
                }
            }

            return (nearestEnemy, nearestDistance);
        }

#nullable disable
    }
}
