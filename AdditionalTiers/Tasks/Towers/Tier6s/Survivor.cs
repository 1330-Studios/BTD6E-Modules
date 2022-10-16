namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class Survivor : TowerTask {
        public static TowerModel survivor;
        private static int time = -1;
        public Survivor() {
            identifier = "Survivor";
            getTower = () => survivor;
            baseTower = AddedTierName.SURVIVOR;
            tower = AddedTierEnum.SURVIVOR;
            requirements += tts => tts.tower.towerModel.baseId.Equals("SniperMonkey") && tts.tower.towerModel.tiers[2] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(survivor);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                survivor = gm.towers.First(a => a.name.Contains("SniperMonkey-205")).Clone()
                    .Cast<TowerModel>();

                survivor.cost = 0;
                survivor.name = "Survivor";
                survivor.baseId = "SniperMonkey";
                survivor.display.guidRef = "Survivor";
                survivor.dontDisplayUpgrades = true;
                survivor.portrait = new("SurvivorIcon");
                survivor.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "Survivor";

                var beh = survivor.behaviors;

                for (var i = 0; i < beh.Length; i++)
                    if (beh[i].GetIl2CppType() == Il2CppType.Of<AttackModel>()) {
                        var am = beh[i].Cast<AttackModel>();

                        for (var j = 0; j < am.weapons.Length; j++) {
                            var we = am.weapons[j];
                            we.rate = 0;

                            am.weapons[j] = we;
                        }

                        beh[i] = am;
                    }

                survivor.behaviors = beh.Add(new OverrideCamoDetectionModel("OverrideCamoDetectionModel_", true));
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("Survivor", "1001186d3e8034b45929adb7ab6f048e", RendererType.SKINNEDMESHRENDERER));
        }
    }
}