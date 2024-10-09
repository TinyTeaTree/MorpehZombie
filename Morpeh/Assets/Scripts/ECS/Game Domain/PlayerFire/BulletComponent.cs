using System;
using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct BulletComponent : IComponent, IDisposable
{
    public BulletMediator.IView view;
    
    public Vector2 pos;
    public Vector2 direction;

    public float speed;
    public float timeLeft;

    public void Dispose()
    {
        if (view != null)
        {
            view.Dispose();
        }
    }
}