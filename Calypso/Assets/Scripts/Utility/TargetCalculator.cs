using System.Collections.Generic;
using UnityEngine;

public static class TargetCalculator
{
    public static Transform GetClosestEnemy(Vector3 currentPos)
    {
        List<Transform> enemies = MakeEnemyList();

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
