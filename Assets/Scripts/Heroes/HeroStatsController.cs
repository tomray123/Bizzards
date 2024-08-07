using UnityEngine;

/// <summary>
/// Manages hero's stats.
/// </summary>
public class HeroStatsController
{
    private HeroStats currentHeroStats;  // Current hero's stats

    public HeroStats CurrentHeroStats => currentHeroStats;

    /// <summary>
    /// Initializes the current hero with given stats.
    /// </summary>
    /// <param name="heroStats">New hero's stats</param>
    public void InitializeHero(HeroStats heroStats)
    {
        currentHeroStats = heroStats;
    }
}
