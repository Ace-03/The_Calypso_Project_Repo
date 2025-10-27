
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWaveSequenceDefinition", menuName = "Scriptable Objects/WaveSequenceSO")]
public class WaveSequenceDefinitionSO : ScriptableObject
{
    public List<WaveDefinitionSO> waveDefinitions;
}