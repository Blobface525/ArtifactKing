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
            On.EntityStates.BrotherMonster.WeaponSlam.FixedUpdate += (orig, self) =>
            {
                BaseState RefBase = new BaseState();
                float DamStat = RefBase.GetFieldValue<float>("damageStat");
                CharacterBody RefBody = new CharacterBody();//RefBody.corePosition
                Transform transform = RefBase.InvokeMethod<Transform>("FindModelChild", WeaponSlam.muzzleString);
                Vector3 position = transform.position;

                ProjectileManager.instance.FireProjectile(WeaponSlam.pillarProjectilePrefab, position, Quaternion.identity, base.gameObject, DamStat * WeaponSlam.pillarDamageCoefficient, 0f, Util.CheckRoll(RefBase.GetFieldValue<float>("critStat"), RefBase.GetPropertyValue<int>("master")), DamageColorIndex.Default, null, -1f);
                
                orig(self);
            };
        }
    }
}