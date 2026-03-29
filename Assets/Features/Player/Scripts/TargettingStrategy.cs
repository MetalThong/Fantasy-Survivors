using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TargettingStrategy
{
    public static Enemy FindClosestEnemyInRange(List<Enemy> enemies, Transform transform, float range)
    {
        if (enemies == null || enemies.Count == 0) return null;
        Enemy closest = null;
        float minDist = float.MaxValue;

        Vector3 pos = transform.position;

        foreach (var e in enemies)
        {
            if (e == null) continue;

            float dist = (e.transform.position - pos).sqrMagnitude;
            if (dist < minDist && dist <= range * range)
            {
                minDist = dist;
                closest = e;
            }
        }
        return closest;
    }

    public static Enemy FindRandomEnemyInRange(List<Enemy> enemies, Transform origin, float range)
    {
        if (enemies == null || enemies.Count == 0) return null;

        float sqrRange = range * range;
        Vector3 pos = origin.position;

        Enemy chosen = null;
        int count = 0;

        foreach (var e in enemies)
        {
            if (e == null) continue;

            float dist = (e.transform.position - pos).sqrMagnitude;
            if (dist <= sqrRange)
            {
                count++;
                if (Random.Range(0, count) == 0)
                {
                    chosen = e;
                }
            }
        }

        return chosen;
    }
}
