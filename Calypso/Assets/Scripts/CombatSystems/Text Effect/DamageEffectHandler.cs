using UnityEngine;

public class DamageEffectHandler : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private OnDamageDealtEventSO damageDealt;
    [SerializeField] private OnDamageDealtEventSO damageTaken;
    [SerializeField] private OnRestoreHealthEventSO restoreHealth;

    [Header("Parameters")]
    [SerializeField] private Color enemyTextColor = Color.white;
    [SerializeField] private Color playerTextColor = Color.red;
    [SerializeField] private Color healPlayerTextColor = Color.green;
    [SerializeField] private float textVerticalOffset;


    [Header("Assets")]
    [SerializeField] private GameObject textPrefab;


    private void OnEnable()
    {
        damageDealt.RegisterListener(SpawnEnemyTextEffect);
        damageTaken.RegisterListener(SpawnPlayerTextEffect);
        restoreHealth.RegisterListener(SpawnHealTextEffect);
    }

    private void OnDisable()
    {
        damageDealt.UnregisterListener(SpawnEnemyTextEffect);
        damageTaken.UnregisterListener(SpawnPlayerTextEffect);
    }

    private void SpawnEnemyTextEffect(DamagePayload payload)
    {
        SpawnTextEffect(payload.damageInfo.damage, payload.receiver.transform.position, enemyTextColor);
    }

    private void SpawnPlayerTextEffect(DamagePayload payload)
    {
        SpawnTextEffect(payload.damageInfo.damage, payload.receiver.transform.position, playerTextColor);
    }

    private void SpawnHealTextEffect(HealPayload payload)
    {
        SpawnTextEffect(payload.value, PlayerManager.Instance.transform.position, healPlayerTextColor);
    }

    private void SpawnTextEffect(float value, Vector3 position, Color textcolor)
    {
        GameObject textObject = Instantiate(textPrefab, position + (Vector3.up * textVerticalOffset), Quaternion.identity);
        TextController controller = textObject.GetComponent<TextController>();

        string roundingValue = "";

        if (value % 1 == 0)
            roundingValue = "n0";
        else
            roundingValue = "n1";

        controller.SetText(value.ToString(roundingValue));
        controller.SetColor(textcolor);
    }
}
