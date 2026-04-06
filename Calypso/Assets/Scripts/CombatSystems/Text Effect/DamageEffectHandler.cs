using UnityEngine;

public class DamageEffectHandler : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private OnDamageDealtEventSO damageDealt;
    [SerializeField] private OnDamageDealtEventSO damageTaken;

    [Header("Parameters")]
    [SerializeField] private Color enemyTextColor = Color.white;
    [SerializeField] private Color playerTextColor = Color.red;
    [SerializeField] private float textVerticalOffset;


    [Header("Assets")]
    [SerializeField] private GameObject textPrefab;


    private void OnEnable()
    {
        damageDealt.RegisterListener(SpawnEnemyTextEffect);
        damageTaken.RegisterListener(SpawnPlayerTextEffect);
    }

    private void OnDisable()
    {
        damageDealt.UnregisterListener(SpawnEnemyTextEffect);
        damageTaken.UnregisterListener(SpawnPlayerTextEffect);
    }


    private void SpawnEnemyTextEffect(DamagePayload payload)
    {
        SpawnTextEffect(payload, enemyTextColor);
    }

    private void SpawnPlayerTextEffect(DamagePayload payload)
    {
        SpawnTextEffect(payload, playerTextColor);
    }

    private void SpawnTextEffect(DamagePayload payload, Color textcolor)
    {
        GameObject textObject = Instantiate(textPrefab, payload.receiver.transform.position + (Vector3.up * textVerticalOffset), Quaternion.identity);
        TextController controller = textObject.GetComponent<TextController>();

        float damage = payload.damageInfo.damage;
        string roundingValue = "";

        if (damage % 1 == 0)
            roundingValue = "n0";
        else
            roundingValue = "n1";

        controller.SetText(damage.ToString(roundingValue));
        controller.SetColor(textcolor);
    }
}
