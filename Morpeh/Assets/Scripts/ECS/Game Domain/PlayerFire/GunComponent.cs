using ECS.Game_Domain.PlayerFire;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Serialization;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct GunComponent : IComponent
{
    public static string BulletPrefabPath = "Bullet";
    
    public GunMediator.IView view;
    
    //Shoot
    public float lastFireTime;
    public float fireRate; //Per Second
    
    //Bullets
    public int ammoLeft;
    public int bulletCapacity;

    //Reload
    public bool isLoaded;
    public float startReloadTime;
    public float reloadTime;
}