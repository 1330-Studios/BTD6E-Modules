namespace AdditionalTiers.Tasks.Towers.Tier6s;

internal class PaintItBlack : TowerTask {
    public static TowerModel pib;
    private static int time = -1;
    public PaintItBlack() {
        identifier = "Paint It Black";
        getTower = () => pib;
        baseTower = AddedTierName.PAINTITBLACK;
        tower = AddedTierEnum.PAINTITBLACK;
        requirements += tts => tts.tower.towerModel.baseId.Equals(AddedTierName.PAINTITBLACK.Split('-')[0]) && tts.tower.towerModel.tiers[1] == 5;
        onComplete += tts => {
            if (time < 50) {
                time++;
                return;
            }
            TransformationManager.VALUE.Add(new(identifier, tts.tower.Id.Id));
            tts.tower.worth = 0;
            tts.tower.UpdateRootModel(pib);
            tts.sim.simulation.CreateTextEffect(new(tts.position), new("UpgradedText"), 10, "Upgraded!", false);
            AbilityMenu.instance.TowerChanged(tts);
            AbilityMenu.instance.RebuildAbilities();
        };
        gameLoad += gm => {
            pib = gm.towers.First(a => a.name.Contains(AddedTierName.PAINTITBLACK)).CloneCast();

            pib.cost = 0;
            pib.name = "Paint It Black";
            pib.baseId = AddedTierName.PAINTITBLACK.Split('-')[0];
            pib.dontDisplayUpgrades = true;
            pib.portrait = new("PIBI");

            var beh = pib.behaviors;

            for (int i = 0; i < beh.Length; i++) {
                if (beh[i].Is<AttackAirUnitModel>(out var aaum)) {
                    for (int k = 0; k < aaum.weapons.Length; k++) {
                        aaum.weapons[k].Rate = 0.01f;
                        if (aaum.weapons[k].projectile.behaviors.Any(a=>a.Is<DamageModel>()))
                            aaum.weapons[k].projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 500});
                        else {
                            var cproj = aaum.weapons[k].projectile.behaviors.First(a=>a.Is<CreateProjectileOnExhaustFractionModel>()).Cast<CreateProjectileOnExhaustFractionModel>();
                            cproj.projectile.ModifyDamageModel(new DamageChange() { multiply = true, damage = 500 });
                        }
                    }
                }
                if (beh[i].Is<AbilityModel>(out var am)) {
                    am.icon = new("PIBAA");
                    am.cooldown /= 2;
                    for (int j = 0; j < am.behaviors.Length; j++) {
                        if (am.behaviors[j].Is<ActivateAttackModel>(out var aam)) {
                            aam.attacks[0].weapons[0].projectile.behaviors.First(b => b.Is<DamageModel>()).Cast<DamageModel>().damage = 0x06000000;
                        }
                    }
                }
                if (beh[i].Is<AirUnitModel>(out var aum)) {
                    aum.display.guidRef = "PIB";
                }
            }
            
            pib.behaviors = beh;
        };
        recurring += _ => { };
        onLeave += () => time = -1;
        assetsToRead.Add(new("PIB", "e2860c192e81e7944a3830b8c41f1390", RendererType.SKINNEDMESHRENDERER));
    }
}