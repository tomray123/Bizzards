using UnityEngine;

/// <summary>
/// Manages hero initialization and handles integration with ReferenceManager.
/// </summary>
public class HeroManager : MonoBehaviour
{
    [SerializeField] private HeroStats initialHeroStats;

    public HeroStatsController HeroStatsController { get; private set; }
    public HeroHealthController HeroHealthController { get; private set; }

    private void Awake() {
        // Initialize controllers
        HeroStatsController = new HeroStatsController();
        HeroHealthController = new HeroHealthController();
    }

    private void Start()
    {
        // Fetch the reference manager instance
        var referenceManager = ReferenceManager.Instance;
        if (referenceManager != null)
        {
            referenceManager.playerData.heroManager = this;
        }

        // Initialize hero stats and health
        if (initialHeroStats != null)
        {
            HeroStatsController.InitializeHero(initialHeroStats);
            HeroHealthController.SetInitialHealth(initialHeroStats.Health);
        }
    }
}
