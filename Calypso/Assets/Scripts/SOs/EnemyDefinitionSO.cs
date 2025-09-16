using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyDefinition", menuName = "EnemySO")]
public class EnemyDefinitionSO : ScriptableObject
{
    public string enemyName;
    public Sprite icon;
    public GameObject enemyPrefab;
    public GameObject deathEffect;
    public int maxHealth;
    public float movementSpeed;
    public WeaponController weapon;
    public float knockbackResistance;
    public float stunResistance;
    public float poisonResistance;
    public float slowResistance;
    public int experienceReward;
    public int resource1;
    public int resource2;
}
