using BepInEx;
using RoR2;
using RoR2.Projectile;
using EntityStates.LunarGolem;
using EntityStates.LunarWisp;
using EntityStates.BrotherMonster;
using EntityStates.BrotherMonster.Weapon;
using Unity;
using UnityEngine;
using UnityEngine.Networking;
using System;
using R2API;
using R2API.Utils;
using EntityStates;


namespace Blobface
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("com.Blobface.ArtifactKing", "Artifact of the King", "1.0.0")]
    public class ArtifactKing : BaseUnityPlugin
    {
        public void Awake()
        {
            On.EntityStates.BrotherMonster.BaseSlideState.OnEnter += (orig, self) =>
            {
                BaseState RefBase = new BaseState();
                var DamStat = RefBase.GetFieldValue<float>("damageStat");
                Ray aimRay = RefBase.InvokeMethod<Ray>("GetAimRay");
                ProjectileManager.instance.FireProjectile(FireTwinShots.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, RefBase.GetFieldValue<float>("damageStat") * FireTwinShots.damageCoefficient, FireTwinShots.force, Util.CheckRoll(RefBase.GetFieldValue<float>("critStat"), RefBase.GetPropertyValue<int>("master")), DamageColorIndex.Default, null, -1f);
                orig(self);
            };
        }
    }
}