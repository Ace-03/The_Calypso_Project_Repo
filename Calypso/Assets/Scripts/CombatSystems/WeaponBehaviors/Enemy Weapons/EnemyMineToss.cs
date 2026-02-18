using UnityEngine;

public class EnemyMineToss : MineTossBehavior
{
    protected override Transform GetTarget() => TargetCalculator.GetPlayer();
}
