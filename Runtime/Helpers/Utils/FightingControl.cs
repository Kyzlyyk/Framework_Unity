using GSystems.Entities;
using UnityEngine;

namespace Kyzlyk.Helpers.Utils
{
    /// <summary>
    /// A collection helpers functions for fighting.
    /// </summary>
    internal struct FightingControl
    {
#nullable enable
        public static (IEntityDataProvider?, float) FindNearestEntity(IEntityDataProvider[] entities, Vector2 origin)
        {
            if (entities == null || entities.Length == 0)
                return (null, -1);

            float nearestDistance = -1;
            float currentDistance;

            IEntityDataProvider nearestEnemy = entities[0];

            for (int i = 0; i < entities.Length; i++)
            {
                currentDistance = Vector2.Distance(origin, entities[i].CurrentPosition);

                if (nearestDistance < currentDistance)
                {
                    nearestDistance = currentDistance;

                    nearestEnemy = entities[i];
                }
            }

            return (nearestEnemy, nearestDistance);
        }

        public static (IEntityDataProvider?, float) FindNearestEnemy(IEntityDataProvider[] entities, Vector2 origin, bool isRightSide)
        {
            if (entities == null || entities.Length == 0)
                return (null, -1);

            IEntityDataProvider[] sideEnemies = new IEntityDataProvider[entities.Length];

            for (int i = 0; i < entities.Length; i++)
            {
                if (entities[i] == null)
                    continue;

                if (isRightSide && origin.x < entities[i].CurrentPosition.x)
                    sideEnemies[i] = entities[i];

                else if (!isRightSide && origin.x > entities[i].CurrentPosition.x)
                    sideEnemies[i] = entities[i];
            }

            IEntityDataProvider nearestEnemy = sideEnemies[0];

            float currentDistance;

            float nearestDistance = Vector2.Distance(origin, sideEnemies[0].CurrentPosition);

            for (int i = 1; i < sideEnemies.Length; i++)
            {
                if (sideEnemies[i] == null)
                    continue;

                currentDistance = Vector2.Distance(origin, sideEnemies[i].CurrentPosition);

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
