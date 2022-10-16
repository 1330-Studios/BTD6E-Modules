namespace GodlyTowers.Towers {
    public class Paragons {
        public static (TowerModel, ShopTowerDetailsModel, string, TowerModel) GetDartMonkey(GameModel model) {
            var origParagon = model.towers.First(t => t.name.Equals("DartMonkey-Paragon"));
            var paragon = origParagon.Clone().Cast<TowerModel>();
            paragon.baseId = "ParagonDartMonkey";
            paragon.name = "ParagonDartMonkey";
            paragon.towerSet = "Primary";
            paragon.emoteSpriteLarge = new("Paragon");
            paragon.tier = 0;
            paragon.tiers = new int[] { 0, 0, 0 };
            paragon.cost = 400000;
            paragon.canAlwaysBeSold = true;
            paragon.isParagon = false;
            paragon.dontDisplayUpgrades = true;
            paragon.icon = paragon.portrait;

            if (!LocalizationManager.Instance.textTable.ContainsKey("ParagonDartMonkey"))
                LocalizationManager.Instance.textTable.Add("ParagonDartMonkey", "Apex Plasma Master");

            var paragonDetails = model.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            paragonDetails.towerId = "ParagonDartMonkey";
            paragonDetails.towerIndex = GlobalTowerIndex.Index;

            return (paragon, paragonDetails, "DartMonkey", origParagon);
        }

        public static (TowerModel, ShopTowerDetailsModel, string, TowerModel) GetBoomerangMonkey(GameModel model) {
            var origParagon = model.towers.First(t => t.name.Equals("BoomerangMonkey-Paragon"));
            var paragon = origParagon.Clone().Cast<TowerModel>();
            paragon.baseId = "ParagonBoomerangMonkey";
            paragon.name = "ParagonBoomerangMonkey";
            paragon.towerSet = "Primary";
            paragon.emoteSpriteLarge = new("Paragon");
            paragon.tier = 0;
            paragon.tiers = new int[] { 0, 0, 0 };
            paragon.cost = 450000;
            paragon.canAlwaysBeSold = true;
            paragon.isParagon = false;
            paragon.dontDisplayUpgrades = true;
            paragon.icon = paragon.portrait;

            if (!LocalizationManager.Instance.textTable.ContainsKey("ParagonBoomerangMonkey"))
                LocalizationManager.Instance.textTable.Add("ParagonBoomerangMonkey", "Glaive Dominus");

            var paragonDetails = model.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            paragonDetails.towerId = "ParagonBoomerangMonkey";
            paragonDetails.towerIndex = GlobalTowerIndex.Index;

            return (paragon, paragonDetails, "BoomerangMonkey", origParagon);
        }

        public static (TowerModel, ShopTowerDetailsModel, string, TowerModel) GetNinjaMonkey(GameModel model) {
            var origParagon = model.towers.First(t => t.name.Equals("NinjaMonkey-Paragon"));
            var paragon = origParagon.Clone().Cast<TowerModel>();
            paragon.baseId = "ParagonNinjaMonkey";
            paragon.name = "ParagonNinjaMonkey";
            paragon.towerSet = "Primary";
            paragon.emoteSpriteLarge = new("Paragon");
            paragon.tier = 0;
            paragon.tiers = new int[] { 0, 0, 0 };
            paragon.cost = 525000;
            paragon.canAlwaysBeSold = true;
            paragon.isParagon = false;
            paragon.dontDisplayUpgrades = true;
            paragon.icon = paragon.portrait;

            if (!LocalizationManager.Instance.textTable.ContainsKey("ParagonNinjaMonkey"))
                LocalizationManager.Instance.textTable.Add("ParagonNinjaMonkey", "Ascended Shadow");

            var paragonDetails = model.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            paragonDetails.towerId = "ParagonNinjaMonkey";
            paragonDetails.towerIndex = GlobalTowerIndex.Index;

            return (paragon, paragonDetails, "NinjaMonkey", origParagon);
        }

        public static (TowerModel, ShopTowerDetailsModel, string, TowerModel) GetMonkeyBuccaneer(GameModel model) {
            var origParagon = model.towers.First(t => t.name.Equals("MonkeyBuccaneer-Paragon"));
            var paragon = origParagon.Clone().Cast<TowerModel>();
            paragon.baseId = "ParagonMonkeyBuccaneer";
            paragon.name = "ParagonMonkeyBuccaneer";
            paragon.towerSet = "Primary";
            paragon.emoteSpriteLarge = new("Paragon");
            paragon.tier = 0;
            paragon.tiers = new int[] { 0, 0, 0 };
            paragon.cost = 612750;
            paragon.canAlwaysBeSold = true;
            paragon.isParagon = false;
            paragon.dontDisplayUpgrades = true;
            paragon.icon = paragon.portrait;

            if (!LocalizationManager.Instance.textTable.ContainsKey("ParagonMonkeyBuccaneer"))
                LocalizationManager.Instance.textTable.Add("ParagonMonkeyBuccaneer", "Navarch of the Seas");

            var paragonDetails = model.towerSet[0].Clone().Cast<ShopTowerDetailsModel>();
            paragonDetails.towerId = "ParagonMonkeyBuccaneer";
            paragonDetails.towerIndex = GlobalTowerIndex.Index;

            return (paragon, paragonDetails, "MonkeyBuccaneer", origParagon);
        }

        /*public static (TowerModel, TowerDetailsModel, string, TowerModel) GetSuperMonkey(GameModel model) {
            var origParagon = model.towers.First(t => t.name.Equals("SuperMonkey-520"));
            FileIOUtil.SaveFile("supermonkey_vengeful", Properties.Resources.supermonkey_vengeful);
            var paragon = FileIOUtil.LoadObject<TowerModel>("supermonkey_vengeful");

            var paragonDetails = model.towerSet[0].Clone().Cast<TowerDetailsModel>();
            paragonDetails.towerId = "SuperMonkeyVengeful";
            paragonDetails.towerIndex = ++index;

            return (paragon, paragonDetails, "SuperMonkey", origParagon);
        }*/
    }
}
