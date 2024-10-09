using System;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct ZombieComponent : IComponent, IDisposable
{
    public static string ZombiePrefabPath = "Zombie";
    
    public ZombieMediator.IView view;
    
    public Vector2 pos;
    public float rot;

    public int hp;
    public float speed;

    public AnimationCurve curve;
    public float normalizedTime;
    public float length;

    public void Dispose()
    {
        if (view != null)
        {
            view.Dispose();
        }
    }
}