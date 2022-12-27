namespace AdditionalTiers.Utils.Towers;
internal struct UMM_Tower {
    internal int CurrentUpgrade;
    internal string Name;
    internal TowerModel TowerModel;
    internal string TowerType;
    internal int UpgradeCost;
    internal string Portrait;
    internal double CurrentSPA;
    internal int CurrentDamage;
    internal double NextSPA;
    internal int NextDamage;
    internal int NextRange;
    internal string Extra;
    internal bool MaxUpgrade;
    internal string NextUpgradeName;

    public UMM_Tower(int currentUpgrade, string name, TowerModel towerModel, string towerType, int upgradeCost, string portrait, double currentSPA, int currentDamage, double nextSPA, int nextDamage, int nextRange, string extra, bool maxUpgrade, string nextUpgradeName) {
        CurrentUpgrade = currentUpgrade;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        TowerModel = towerModel ?? throw new ArgumentNullException(nameof(towerModel));
        TowerType = towerType ?? throw new ArgumentNullException(nameof(towerType));
        UpgradeCost = upgradeCost;
        Portrait = portrait ?? throw new ArgumentNullException(nameof(portrait));
        CurrentSPA = currentSPA;
        CurrentDamage = currentDamage;
        NextSPA = nextSPA;
        NextDamage = nextDamage;
        NextRange = nextRange;
        Extra = extra ?? throw new ArgumentNullException(nameof(extra));
        MaxUpgrade = maxUpgrade;
        NextUpgradeName = nextUpgradeName ?? throw new ArgumentNullException(nameof(nextUpgradeName));
    }
}