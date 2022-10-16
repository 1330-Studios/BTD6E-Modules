namespace AdditionalTiers.Utils.Attack {
    public static class ProjectileModelExt {
        public static float DamageMultiplier = 1;

        public static void RebuildBehaviors(this ProjectileModel projectile, params Model[] behaviors) => projectile.RebuildBehaviorsA(a => { return false; }, behaviors);
        public static void RebuildBehaviorsA(this ProjectileModel projectile, Func<Model, bool> removalAction, params Model[] behaviors) => projectile.behaviors = projectile.behaviors.Remove(removalAction).Add(behaviors);
        public static bool HasBehavior<T>(this ProjectileModel projectile) => projectile.behaviors.Any(m => m.GetIl2CppType().Equals(Il2CppType.Of<T>()));

        public static void AddDamageModel(this ProjectileModel projectile, DamageModelCreation dmc, params object[] args) {
            DamageModel dm = dmc switch {
                DamageModelCreation.Standard => new("DamageModel_Gen_Standard", (int)args[0] * DamageMultiplier, (int)args[0] * DamageMultiplier, true, (bool)args[1], true, (BloonProperties)(int)args[2], BloonProperties.None),
                DamageModelCreation.None => new("DamageModel_Gen_None", (int)args[0] * DamageMultiplier, (int)args[0] * DamageMultiplier, (bool)args[1], (bool)args[2], (bool)args[3], (BloonProperties)(int)args[4], BloonProperties.None),
                DamageModelCreation.Full => new("DamageModel_Gen_Full", (int)args[0] * DamageMultiplier, (int)args[0] * DamageMultiplier, true, true, true, BloonProperties.None, BloonProperties.None),
                _ => throw new Exception("How did we get here?")
            };

            projectile.RebuildBehaviors(dm);
        }

        public static void ModifyDamageModel(this ProjectileModel projectile, DamageChange damageChange) {
            var damage = (from behavior in projectile.behaviors where behavior is not null && Il2CppType.Of<DamageModel>().IsAssignableFrom(behavior.GetIl2CppType()) select behavior.Cast<DamageModel>()).FirstOrDefault();
            if (damage == null)
                return;
            if (!damageChange.set) {
                if (damageChange.multiply) {
                    damage.damage *= damageChange.damage * DamageMultiplier;
                    damage.maxDamage *= damageChange.maxDamage * DamageMultiplier;
                }
                damage.damage += damageChange.damage * DamageMultiplier;
                damage.maxDamage += damageChange.maxDamage * DamageMultiplier;
                damage.immuneBloonProperties |= damageChange.immuneBloonProperties;
            } else {
                damage.damage = damageChange.damage * DamageMultiplier;
                damage.maxDamage = damageChange.maxDamage * DamageMultiplier;
                damage.immuneBloonProperties = damageChange.immuneBloonProperties;
            }

            if (damageChange.cappedDamage > 0)
                damage.CapDamage(damageChange.cappedDamage);
        }

        public static void AddKnockbackModel(this ProjectileModel projectile) {
            var knockback = new KnockbackModel($"{projectile.name}_KnockbackModel_", 0.7f, 1f, 1.3f, 0.5f, "KnockbackKnockback");
            projectile.behaviors = projectile.behaviors.Add(knockback);
        }

        public static void SetDisplay(this ProjectileModel projectile, string display) {
            projectile.display = new() { guidRef = display };
            if (projectile.HasBehavior<DisplayModel>())
                projectile.behaviors.First(m => m.GetIl2CppType().Equals(Il2CppType.Of<DisplayModel>())).Cast<DisplayModel>().display = new() { guidRef = display };
        }
    }
}
