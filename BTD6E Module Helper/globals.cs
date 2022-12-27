global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Reflection.Emit;
global using System.Security.Cryptography;
global using System.Text;
global using System.Threading.Tasks;

global using Il2Cpp;

global using Il2CppAssets.Scripts.Models;
global using Il2CppAssets.Scripts.Models.GenericBehaviors;
global using Il2CppAssets.Scripts.Models.Map;
global using Il2CppAssets.Scripts.Models.Profile;
global using Il2CppAssets.Scripts.Models.Towers;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
global using Il2CppAssets.Scripts.Models.Towers.Mods;
global using Il2CppAssets.Scripts.Models.Towers.Projectiles;
global using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
global using Il2CppAssets.Scripts.Models.Towers.Upgrades;
global using Il2CppAssets.Scripts.Models.TowerSets;
global using Il2CppAssets.Scripts.Simulation.Towers;
global using Il2CppAssets.Scripts.Unity.Bridge;
global using Il2CppAssets.Scripts.Unity.Display;
global using Il2CppAssets.Scripts.Unity.Player;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.Stats;
global using Il2CppAssets.Scripts.Unity.UI_New.Main.MapSelect;
global using Il2CppAssets.Scripts.Unity.UI_New.Upgrade;
global using Il2CppAssets.Scripts.Unity.Audio;
global using Il2CppAssets.Scripts.Utils;

global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
global using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;

global using BTD6E_Module_Helper;

global using HarmonyLib;

global using MelonLoader;

global using Il2CppInterop;
global using Il2CppInterop.Common;
global using Il2CppInterop.Common.Attributes;
global using Il2CppInterop.Common.Host;
global using Il2CppInterop.Common.Maps;
global using Il2CppInterop.Common.XrefScans;
global using Il2CppInterop.Runtime;
global using Il2CppInterop.Runtime.Attributes;
global using Il2CppInterop.Runtime.Injection;
global using Il2CppInterop.Runtime.InteropTypes;
global using Il2CppInterop.Runtime.InteropTypes.Arrays;
global using Il2CppInterop.Runtime.InteropTypes.Fields;
global using Il2CppInterop.Runtime.Runtime;
global using Il2CppInterop.Runtime.Startup;
global using Il2CppInterop.Runtime.XrefScans;

global using UnityEngine;
global using UnityEngine.UI;


global using Il2CppAssets.Scripts.Models.Effects;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
global using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
global using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
global using Il2CppAssets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;
global using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
global using Il2CppAssets.Scripts.Models.Towers.Weapons;
global using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
global using Il2CppAssets.Scripts.Models.Towers.Filters;

global using Il2CppAssets.Scripts.Models.Bloons.Behaviors;

global using Il2CppAssets.Scripts.Unity.UI_New.InGame.StoreMenu;

global using static HarmonyLib.AccessTools;

global using SV3 = Il2CppAssets.Scripts.Simulation.SMath.Vector3;
global using HashHelper = System.Security.Cryptography.MD5;
global using Object = UnityEngine.Object;
