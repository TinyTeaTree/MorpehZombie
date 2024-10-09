using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct InputComponent : IComponent
{
    public bool isPressingUp;
    public bool isPressingDown;
    public bool isPressingLeft;
    public bool isPressingRight;

    public Vector2 direction;

    public bool isFirePressed;
    public bool isFirePressStarted;
}