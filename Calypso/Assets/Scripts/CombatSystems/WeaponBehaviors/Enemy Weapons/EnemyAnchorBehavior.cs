using UnityEngine;

public class EnemyAnchorBehavior : AnchorBehavior
{
    protected override Transform GetTarget() => TargetCalculator.GetPlayer();
}
