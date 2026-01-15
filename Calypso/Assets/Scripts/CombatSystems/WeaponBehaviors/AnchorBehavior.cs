using System.Collections;
using UnityEngine;

public class AnchorBehavior : MonoBehaviour, IWeaponBehavior
{
    [SerializeField] private float volleyRate;

    private IEnumerator ThrowAnchor(WeaponController weapon)
    {
        int volleyCount = weapon.GetAmount();

        for (int i = 0; i < volleyCount; i++)
        {
            yield return new WaitForSeconds(volleyRate);
        }
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        throw new System.NotImplementedException();
    }

    public void Attack(WeaponController weapon)
    {
        ThrowAnchor(weapon);
    }

    public bool IsAimable()
    {
        return false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
