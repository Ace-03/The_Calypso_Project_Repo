using System.Collections.Generic;
using UnityEngine;

public static class TargetCalculator
{
    public static Transform GetClosestEnemy(Vector3 currentPos)
    {
        List<Transform> enemies = MakeEnemyList();

        if (enemies.Count == 0) return null;

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    public static Transform GetRandomOfClosestEnemies(Vector3 currentPos)
    {
        List<Transform> enemies = MakeEnemyList();
        List<Transform> validTargets = new List<Transform>();
        float maxValidDist = Mathf.Infinity;
        foreach (Transform enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.position, currentPos);

            if (dist < maxValidDist)
            {
                maxValidDist = dist;
                validTargets.Add(enemy);

                if (validTargets.Count > 10)
                {
                    Transform farthest = null;
                    float maxdist = 0;
                    foreach (Transform t in validTargets)
                    {
                        float d = Vector3.Distance(t.position, currentPos);
                        if (d > maxdist)
                        {
                            farthest = t;
                            maxdist = d;
                        }
                    }
                    validTargets.Remove(farthest);
                }
            }
        }
        if (validTargets.Count > 0)
        {
            return validTargets[Random.Range(0, validTargets.Count)];
        }

        return null;
    }

    public static Transform GetPlayer() => ContextRegister.Instance?.GetContext().playerTransform;

    private static List<Transform> MakeEnemyList()
    {
        List<AI_NAV> navAgents = new List<AI_NAV>();
        List<Transform> targetList = new List<Transform>();

        navAgents.AddRange(Object.FindObjectsByType<AI_NAV>(FindObjectsSortMode.None));
        foreach (AI_NAV aI_NAV in navAgents)
        {
            targetList.Add(aI_NAV.transform);
        }
        return targetList;
    }

    public static bool CheckRange(Vector3 targetPos, Vector3 currentPosition, float range)
    {
        return !(Vector3.Distance(targetPos, currentPosition) > range);
    }

}
