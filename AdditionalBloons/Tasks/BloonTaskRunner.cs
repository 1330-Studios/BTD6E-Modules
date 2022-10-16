
using AdditionalBloons.Utils;

using Assets.Scripts.Simulation.Bloons;
using Assets.Scripts.Simulation.Track;
using V3 = Assets.Scripts.Simulation.SMath.Vector3;

using static Assets.Scripts.Models.Bloons.Behaviors.StunTowersInRadiusActionModel;
using Assets.Scripts.Models.Bloons;

namespace AdditionalBloons.Tasks {
    internal sealed class BloonTaskRunner {
        public static BloonQueue bloonQueue = new();

        // Key of bloontask which stores all needed info, Value of KeyValuePair with key of amount sent out of total and value of time spent
        private static Dictionary<BloonTask, KeyValuePair<int, int>> bloonTasks = new();
        private static System.Random rand = new(DateTime.Now.Millisecond);

        private static readonly Dictionary<int, int> burnQueue = new(); // tower id key, tick value
        private static readonly Func<double, double> burnFunc = (tiers) => 1 + Math.Pow(2.71828183, 0.37701 * tiers);
        private static bool HasSpawnedFireBAD;

        internal static void Run() {
            var fps = Application.targetFrameRate.Map(60, 165, 60, 144);

            if (InGame.instance?.bridge != null) {

                foreach (var bloon in InGame.instance.bridge.GetAllBloons()) {
                    var bloonPosition = bloon.position;

                }

                #region Bloon Spawner

                //Add to dictionary from queue
                while (bloonQueue.Count > 0) bloonTasks.Add(bloonQueue.Dequeue(), new KeyValuePair<int, int>(0, 0));

                if (bloonTasks.Count > 0) {
                    List<BloonTask> remove = new();
                    Dictionary<BloonTask, KeyValuePair<int, int>> update = new();

                    foreach (var entry in bloonTasks) {
                        //exception for it updatesBetween is 0 so they get sent at the same time
                        bool shouldContinue = true;
                        if (entry.Key.updatesBetween is 0) {
                            for (int i = 0; i < entry.Key.amount; i++) {
                                InGame.instance.bridge.simulation.map.spawner.Emit(entry.Key.model,
                                    InGame.instance.bridge.simulation.GetCurrentRound(), 0);
                                remove.Add(entry.Key);
                                shouldContinue = false;
                            }
                        }

                        if (!shouldContinue) break;

                        //timing for if should be sent
                        if (entry.Value.Key < entry.Key.amount && (entry.Value.Value % entry.Key.updatesBetween == 0))
                            InGame.instance.bridge.simulation.map.spawner.Emit(entry.Key.model,
                                InGame.instance.bridge.simulation.GetCurrentRound(), 0);

                        //if expired, remove
                        if (entry.Value.Value > entry.Key.amount * entry.Key.updatesBetween) {
                            remove.Add(entry.Key);
                        } else {
                            //if it was sent add to the key
                            var keyNewVal = entry.Value.Key;
                            if (entry.Value.Value % entry.Key.updatesBetween == 0)
                                keyNewVal++;
                            //update it with new timing
                            var kvp = new KeyValuePair<int, int>(keyNewVal, entry.Value.Value + 1);
                            update.Add(entry.Key, kvp);
                        }
                    }

                    //remove entries in the list from the dictionary
                    foreach (var toRemove in remove) bloonTasks.Remove(toRemove);
                    bloonTasks = update;
                }

                #endregion
            }
        }

        internal static bool Damage(ref Bloon __instance, ref float totalAmount, Tower tower) {
            var model = __instance.bloonModel;

            if (model.icon.guidRef.Equals("FireBADIcon")) {
                if (tower == null)
                    return true;

                if (tower.towerModel.baseId.StartsWith("Ice")) {
                    totalAmount *= 2.2f;
                    __instance.Move(-0.01f);
                } else if (!tower.towerModel.baseId.StartsWith("Gwen")) {
                    __instance.Move(0.005f);
                } else {
                    totalAmount--;
                }
            }

            if (model.icon.guidRef.Equals("CoconutIcon")) {
                var healthPercent = model.maxHealth / __instance.health / 5f;

                if (__instance.spawnRound > 60)
                    healthPercent /= 2f;
                if (__instance.spawnRound > 100)
                    healthPercent /= 5f;

                __instance.Move(healthPercent);
                totalAmount = 1;
            }

            return true;
        }

        internal static bool Emit(ref Spawner __instance, ref BloonModel bloon) {
            if (__instance.CurrentRound == 139 && HasSpawnedFireBAD && bloon.baseId == "Bad" && bloon.maxHealth != 500_500_500) {
                return false;
            }
            if (__instance.CurrentRound == 139 && !HasSpawnedFireBAD) {
                HasSpawnedFireBAD = true;
                var fireBAD = BloonCreator.bloons.Find(a => a.icon.guidRef.Equals("Ui[AdditionalBloons.Resources.CopBAD.CopBADIcon.png]", StringComparison.Ordinal));
                if (fireBAD != null) {
                    __instance.Emit(fireBAD, __instance.currentRound.ValueInt, 0);
                }
                return false;
            }

            /*if (!__instance.isSandbox) {
                int i;
                if (__instance.CurrentRound < 50)
                    i = rand.Next(100);
                else if (__instance.CurrentRound < 100)
                    i = rand.Next(50);
                else if (__instance.CurrentRound < 200)
                    i = rand.Next(20);
                else
                    i = rand.Next(10);


                if (i == 4) {
                    if (BloonCreator.bloons == null || BloonCreator.bloons.Count! > 0 || BloonCreator.bloons.Any(a => a.icon.guidRef.Equals("CoconutIcon")))
                        return true;
                    var coconut = BloonCreator.bloons.Find(a => a.icon.guidRef.Equals("CoconutIcon", StringComparison.Ordinal))?.CloneCast();
                    if (coconut != null) {
                        if (__instance.currentRound.ValueInt > 20) coconut.maxHealth = Math.Min(__instance.currentRound.ValueInt, 50);
                        __instance.Emit(coconut, __instance.currentRound.ValueInt, 0);
                    }
                }
            }*/

            return true;
        }

        internal static void Quit() {
            bloonQueue.Clear();
            bloonTasks.Clear();
            rand = new(DateTime.Now.Millisecond);
            HasSpawnedFireBAD = false;
            GC.Collect();
        }
    }
}