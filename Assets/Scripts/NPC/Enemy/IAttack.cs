/// <summary>
/// Interface for enemy attacks.
/// </summary>
public interface IAttack
{
    /// <summary>
    /// Executes the attack.
    /// </summary>
    void ExecuteAttack();

    /// <summary>
    /// Sets the damage for the attack.
    /// </summary>
    /// <param name="damage">Damage value</param>
    void SetDamage(float damage);

    /// <summary>
    /// Gets the range of the attack.
    /// </summary>
    /// <returns>The range of the attack</returns>
    float GetRange();

    /// <summary>
    /// Checks if the attack can be executed (e.g., based on cooldown).
    /// </summary>
    /// <returns>True if the attack can be executed, otherwise false</returns>
    bool CanAttack();
}
