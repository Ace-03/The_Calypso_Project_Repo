using System.Collections.Generic;
using UnityEngine;

public class RadarWeaponBehavior : MonoBehaviour, IWeaponBehavior
{
    private float BeamSpeed = 1f;

    [SerializeField] private float beamBaseLength = 5f;
    [SerializeField] private Transform beamPivotTransform;
    [SerializeField] private GameObject beamPrefab;

    private List<GameObject> beamInstances = new List<GameObject>();

    public void ApplyWeaponStats(WeaponController weapon)
    {
        BeamSpeed = weapon.GetCooldown();
        MakeBeams(weapon);
    }

    private void MakeBeams(WeaponController weapon)
    {
        float beamCount = weapon.GetAmount();
        float length = beamBaseLength + weapon.GetArea();
        float aoeTickRate = weapon.GetAOETick();

        foreach (GameObject beam in beamInstances)
        {
            Destroy(beam);
        }
        beamInstances.Clear();

        for (int i = 0; i < beamCount; i++)
        {
            GameObject newBeam = Instantiate(beamPrefab, beamPivotTransform);
            newBeam.transform.localScale = new Vector3(newBeam.transform.localScale.x, length, newBeam.transform.localScale.z) / 5;
            BulletTrigger bt = newBeam.GetComponent<BulletTrigger>();

            bt.SetData(weapon.GetWeaponData());
            bt.weaponData = weapon.GetWeaponData();
            bt.SetTickInterval(weapon.GetAOETick());

            beamInstances.Add(newBeam);
        }

        for (int i = 0; i < beamCount; i++)
        {
            float angle = i * (360f / beamCount);
            beamInstances[i].transform.localRotation = Quaternion.Euler(0, angle, 90);
        }
    }

    public void Attack(WeaponController weapon)
    {
        return;
    }

    public bool IsAimable()
    {
        return false;
    }

    void Update()
    {
        float newYRotation = (BeamSpeed * Time.deltaTime * 25) % 360;
        beamPivotTransform.Rotate(0, newYRotation, 0);
    }
}
