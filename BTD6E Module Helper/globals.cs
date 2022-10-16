global using System;
global using System.Collections.Generic;
global using System.IO;
global using System.Linq;
global using System.Reflection;
global using System.Reflection.Emit;
global using System.Security.Cryptography;
global using System.Text;
global using System.Threading.Tasks;

global using Assets.Scripts.Models;
global using Assets.Scripts.Models.GenericBehaviors;
global using Assets.Scripts.Models.Map;
global using Assets.Scripts.Models.Profile;
global using Assets.Scripts.Models.Towers;
global using Assets.Scripts.Models.Towers.Behaviors;
global using Assets.Scripts.Models.Towers.Behaviors.Attack;
global using Assets.Scripts.Models.Towers.Mods;
global using Assets.Scripts.Models.Towers.Projectiles;
global using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
global using Assets.Scripts.Models.Towers.Upgrades;
global using Assets.Scripts.Models.TowerSets;
global using Assets.Scripts.Simulation.Towers;
global using Assets.Scripts.Unity.Bridge;
global using Assets.Scripts.Unity.Display;
global using Assets.Scripts.Unity.Player;
global using Assets.Scripts.Unity.UI_New.InGame;
global using Assets.Scripts.Unity.UI_New.InGame.Stats;
global using Assets.Scripts.Unity.UI_New.Main.MapSelect;
global using Assets.Scripts.Unity.UI_New.Upgrade;
global using Assets.Scripts.Unity.Audio;
global using Assets.Scripts.Utils;

global using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
global using Assets.Scripts.Models.Towers.Behaviors.Emissions;
global using Assets.Scripts.Models.Towers.Weapons.Behaviors;

global using BTD6E_Module_Helper;

global using HarmonyLib;

global using MelonLoader;

global using NinjaKiwi.Common;

global using UnhollowerBaseLib;

global using UnhollowerRuntimeLib;

global using UnityEngine;
global using UnityEngine.UI;


global using Assets.Scripts.Models.Effects;
global using Assets.Scripts.Models.Towers.Behaviors.Abilities;
global using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
global using Assets.Scripts.Simulation.Towers.Behaviors.Abilities;
global using Assets.Scripts.Unity.UI_New.InGame.TowerSelectionMenu;
global using Assets.Scripts.Models.Towers.TowerFilters;
global using Assets.Scripts.Models.Towers.Weapons;
global using Assets.Scripts.Simulation.Towers.Weapons;
global using Assets.Scripts.Models.Towers.Filters;

global using Assets.Scripts.Models.Bloons.Behaviors;

global using UnhollowerBaseLib.Attributes;
global using Assets.Scripts.Unity.UI_New.InGame.StoreMenu;

global using static HarmonyLib.AccessTools;

global using SV3 = Assets.Scripts.Simulation.SMath.Vector3;
global using NKR = NinjaKiwi.Common.Random;
global using HashHelper = System.Security.Cryptography.MD5;
global using Object = UnityEngine.Object;
