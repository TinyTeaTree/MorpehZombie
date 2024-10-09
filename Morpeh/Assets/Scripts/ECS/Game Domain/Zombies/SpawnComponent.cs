using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

[System.Serializable]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
public struct SpawnComponent : IComponent
{
    public SpawnMediator view;

    public float spawnRate; //zombies per second
    public float lastSpawnTime;

    public bool withVisibility; //For debug purposes
}