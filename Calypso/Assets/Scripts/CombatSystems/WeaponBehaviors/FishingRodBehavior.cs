using System.Collections;
using UnityEngine;

public class FishingRodBehavior : MonoBehaviour, IWeaponBehavior
{
    [SerializeField] private GameObject triggerObject;
    [SerializeField] private BulletTrigger trigger;
    [SerializeField] private float volleyDuration;


    private IEnumerator SwingRod(WeaponController weapon)
    {
        int volleyCount = weapon.GetAmount();
        float volleyRate = volleyDuration / volleyCount;
        if (ContextRegister.Instance.GetContext().playerManager.GetFacingRight())
        {
            transform.eulerAngles = new Vector3 (0f, 0f, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3 (0f, 180f, 0f);
        }

        for (int i = 0; i < volleyCount; i++)
        {
            triggerObject.SetActive(true);

            // player animation

            yield return new WaitForSeconds(weapon.GetDuration());

            // stop animation

            triggerObject.SetActive(false);

            yield return new WaitForSeconds(volleyRate);
        } 
    }

    public void ApplyWeaponStats(WeaponController weapon)
    {
        triggerObject.transform.localScale *= weapon.GetArea();
        trigger.SetDamageSource(weapon.GetDamageSource());
    }

    public void Attack(WeaponController weapon)
    {
        StartCoroutine(SwingRod(weapon));
    }

    public bool IsAimable()
    {
        return true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerObject.SetActive(false);
    }
}
