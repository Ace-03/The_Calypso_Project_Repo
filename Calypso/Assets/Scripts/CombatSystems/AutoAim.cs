using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class AutoAim : MonoBehaviour
{
    public Transform target;
    public List<AI_NAV> targetList;
    public List<Transform> t_List;
    public bool alsoAutoOff = false;
    public float rangeOfEngage = 20f;
    ParticleSystem ps;

    void Update()
    {
        if(ps == null)
        {
            ps = GetComponent<ParticleSystem>();
        }
        UpdateEnemyList();
        target = GetClosestEnemy(t_List);
        LookAtTarget();
        if (alsoAutoOff)
        {
            bool inRange = CheckRange();
            if (inRange)
            {
                if (!ps.isPlaying)
                {
                    ps.Play();
                }
            }
            else
            {
                ps.Stop();
            }
        }
    }
    void LookAtTarget()
    {
        if (target != null)
        {
            Vector3 lookPos = target.position - transform.position;
            Quaternion lookRot = Quaternion.LookRotation(lookPos, Vector3.up);
            float eulerY = lookRot.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, eulerY - 90f, 0);
            transform.rotation = rotation;
        }
    }
    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
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
    void UpdateEnemyList()
    {
        targetList.Clear();
        targetList.AddRange(FindObjectsByType<AI_NAV>(FindObjectsSortMode.None));
        t_List.Clear();
        foreach (AI_NAV aI_NAV in targetList)
        {
            t_List.Add(aI_NAV.transform);
        }
    }
    bool CheckRange()
    {
        float distance = Vector3.Distance(target.position,transform.position);
        return !(distance>rangeOfEngage);
    }
}
