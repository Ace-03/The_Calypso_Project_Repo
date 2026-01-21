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

        target = TargetCalculator.GetClosestEnemy(transform.position);
        LookAtTarget();
        if (alsoAutoOff)
        {
            if (target == null)
            {
                ps.Stop();
                return;
            }
            bool inRange = TargetCalculator.CheckRange(target.position, transform.position, rangeOfEngage);
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
}
