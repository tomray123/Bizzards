using UnityEngine;

/// <summary>
/// Interface for hero stats.
/// </summary>
public interface IHeroStats
{
    string HeroName { get; }
    float Health { get; }
    float MoveSpeed { get; }
}
