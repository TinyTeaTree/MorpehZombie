using Scellecs.Morpeh;
using TMPro;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Serialization;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct HealthComponent : IComponent
{
    public TMP_Text healthText;
    public TMP_Text zombiesText; //I know it should not be here, but I'm just checking something

    public int healthPoints;
}