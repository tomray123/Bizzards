using UnityEngine;

/// <summary>
/// ScriptableObject class to store hero stats data.
/// </summary>
[CreateAssetMenu(fileName = "NewHeroStats", menuName = "HeroStats")]
public class HeroStats : ScriptableObject, IHeroStats
{
    [SerializeField] private string heroName;  // Hero's name
    [SerializeField] private float health;     // Hero's health
    [SerializeField] private float moveSpeed;  // Hero's movement speed

    // Properties to access the fields
    public string HeroName => heroName;
    public float Health => health;
    public float MoveSpeed => moveSpeed;
}
