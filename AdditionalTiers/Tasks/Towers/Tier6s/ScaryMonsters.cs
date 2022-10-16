namespace AdditionalTiers.Tasks.Towers.Tier6s {
    public sealed class ScaryMonsters : TowerTask {
        public static TowerModel scaryMonsters;
        private static int time = -1;
        public ScaryMonsters() {
            identifier = "Scary Monsters";
            getTower = () => scaryMonsters;
            baseTower = AddedTierName.SCARYMONSTERS;
            tower = AddedTierEnum.SCARYMONSTERS;
            requirements += tts => tts.tower.towerModel.baseId.Equals("BoomerangMonkey") && tts.tower.towerModel.tiers[0] == 5;
            onComplete += tts => {
                if (time < 50) {
                    time++;
                    return;
                }
                TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
                tts.tower.worth = 0;
                tts.tower.UpdateRootModel(scaryMonsters);
                tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
                AbilityMenu.instance.TowerChanged(tts);
                AbilityMenu.instance.RebuildAbilities();
            };
            gameLoad += gm => {
                scaryMonsters = gm.towers.First(a => a.name.Contains("BoomerangMonkey-502")).Clone().Cast<TowerModel>();

                scaryMonsters.range = 100;
                scaryMonsters.cost = 0;
                scaryMonsters.name = "Scary Monsters";
                scaryMonsters.baseId = "BoomerangMonkey";
                scaryMonsters.display.guidRef = "ScaryMonsters";
                scaryMonsters.dontDisplayUpgrades = true;
                scaryMonsters.portrait = new("ScaryMonstersIcon");
                scaryMonsters.behaviors.First(a => a.GetIl2CppType() == Il2CppType.Of<DisplayModel>()).Cast<DisplayModel>().display.guidRef = "ScaryMonsters";
                var beh = scaryMonsters.behaviors;
                ProjectileModel orbitProj = null;

                for (var i = 0; i < beh.Length; i++)
                    if (beh[i].Is<AttackModel>(out var am)) {
                        var wm = am.weapons[0];

                        wm.projectile.scale *= 1.01f;
                        wm.projectile.ignorePierceExhaustion = true;
                        wm.projectile.ignoreBlockers = true;
                        wm.projectile.display.guidRef = "ScaryMonstersProj";

                        wm.projectile.behaviors = wm.projectile.behaviors.Add(new DisplayModel("DM_", new("2171756de70c7744cb59ec3302393d2a"), 0, new(), 0.7f, true, 0));

                        orbitProj = wm.projectile.Clone().Cast<ProjectileModel>();

                        for (var k = 0; k < wm.projectile.behaviors.Length; k++)
                            if (wm.projectile.behaviors[k].Is<DamageModel>(out var dm))
                                dm.damage *= 1000;

                        am.weapons[0] = wm;

                        am.range = 100;
                        break;
                    }

                scaryMonsters.behaviors = beh.Add(new OverrideCamoDetectionModel("OCDM_", true)).Remove(a => a.Is<OrbitModel>(out _));
            };
            recurring += tts => { };
            onLeave += () => { time = -1; };
            assetsToRead.Add(new("ScaryMonsters", "c611c5e568a4bde4bbe28f565aa440e8", RendererType.SKINNEDMESHRENDERER));
            assetsToRead.Add(new("ScaryMonstersProj", "ae8cebf807b15984daf0219b66f42897", RendererType.SPRITERENDERER));
        }
    }
}