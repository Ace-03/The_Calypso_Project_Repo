using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveDefinition", menuName = "Scriptable Objects/Wave")]

public class WaveDefinitionSO : ScriptableObject
{
    [System.Serializable]
    public class EnemySpawnInfo
    {
        public EnemyDefinitionSO enemyDefinition;
        public float spawnRate;
        public int maxActiveEnemies;
    }

    public List<EnemySpawnInfo> enemiesInWave;
    public float waveDuration;
}
