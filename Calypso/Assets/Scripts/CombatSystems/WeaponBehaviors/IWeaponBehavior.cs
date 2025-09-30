public interface IWeaponBehavior
{
    /* Attack is the method that connects the weapon
     * controller to the particle systems behavior
     * when attack is called the particle effect activates once
     * the weapon controller will determine how the particle effect
     * will loop (this may not be necessary).
     */
    void Attack(WeaponController weapon);


    /* use weapon stats like speed, area, tick, (cooldown potentially)
     * on behavior controller to adjust its behavior.
     */
    void ApplyWeaponStats(WeaponController weapon);

    bool IsAimable();
}
