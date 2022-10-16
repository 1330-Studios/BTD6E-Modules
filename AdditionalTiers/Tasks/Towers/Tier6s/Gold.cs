using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Towers.Projectiles;

using static Assets.Scripts.Simulation.Simulation;

namespace AdditionalTiers.Tasks.Towers.Tier6s;
internal class Gold : TowerTask {
    public static TowerModel gold;
    private static int time = -1;
    public Gold() {
        identifier = "Gold";
        getTower = () => gold;
        baseTower = AddedTierName.GOLD;
        tower = AddedTierEnum.GOLD;
        requirements += tts => tts.tower.towerModel.baseId.Equals("IceMonkey") && tts.tower.towerModel.tiers[2] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(gold);
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            gold = gm.towers.First(a => a.name.Equals(baseTower)).CloneCast();

            gold.range = 75;
            gold.cost = 0;
            gold.name = "Gold";
            gold.baseId = baseTower.Split('-')[0];
            gold.dontDisplayUpgrades = true;
            gold.SetDisplay("Gold");
            gold.portrait = new("GoldPor");

            for (int i = 0; i < gold.behaviors.Length; i++) {
                if (gold.behaviors[i].Is<AttackModel>(out var am)) {
                    am.range = 75;

                    for (int j = 0; j < am.weapons.Length; j++) {
                        var weapon = am.weapons[j];

                        weapon.Rate = 0;
                        weapon.projectile.id = "CryoGOLD";
                        weapon.projectile.display.guidRef = "GoldProj";
                        weapon.emission = new RandomArcEmissionModel("RAEM_", 1, 0, 0, 10, 1, null);

                        am.weapons[j] = weapon;
                    }
                }
            }

            gold.behaviors = gold.behaviors.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true).Cast<Model>());
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        assetsToRead.Add(new("Gold", "214d94590f8fe4643adb1eb1d91cafec", RendererType.SKINNEDMESHRENDERER));
        assetsToRead.Add(new("GoldProj", "ac08d93cfcd9d144189a14d22329c953", RendererType.SPRITERENDERER));
    }

    [HarmonyPatch(typeof(Bloon), nameof(Bloon.Damage))]
    public sealed class Bloon_Damage_GOLD {
        private static readonly System.Random random = new();
        private static ulong damage;
        private static int lastJackpot;
        private static readonly DateTime epochStart = new(1970, 1, 1);


        [HarmonyPostfix]
        public static void Postfix(ref Bloon __instance, Projectile projectile) {
            TimeSpan t = DateTime.UtcNow - epochStart;
            if (projectile?.Weapon?.attack?.tower?.towerModel?.name == "Gold" && t.TotalSeconds > lastJackpot) {
                damage++;
                if (damage % ((ulong)random.Next(50000)+1) == 0) {
                    projectile.Sim.CreateTextEffect(__instance.Position.ToVector3(), new("JackpotText"), 10, "JACKPOT!!!\n+ $100,000", false);
                    projectile.Sim.AddCash(100000, CashType.Normal, InGame.instance.bridge.GetInputId(), CashSource.Normal);
                    lastJackpot = (int)t.TotalSeconds+3;
                } else if (damage % 5000 == 0) {
                    projectile.Sim.CreateTextEffect(__instance.Position.ToVector3(), new("JackpotText"), 10, "+ $10,000", false);
                    projectile.Sim.AddCash(10000, CashType.Normal, InGame.instance.bridge.GetInputId(), CashSource.Normal);
                } else if (damage % 500 == 0) {
                    projectile.Sim.CreateTextEffect(__instance.Position.ToVector3(), new("JackpotText"), 10, "+ $100", false);
                    projectile.Sim.AddCash(100, CashType.Normal, InGame.instance.bridge.GetInputId(), CashSource.Normal);
                } else if (damage % 50 == 0) {
                    projectile.Sim.CreateTextEffect(__instance.Position.ToVector3(), new("JackpotText"), 10, "+ $5", false);
                    projectile.Sim.AddCash(5, CashType.Normal, InGame.instance.bridge.GetInputId(), CashSource.Normal);
                }
            }
        }
    }
}