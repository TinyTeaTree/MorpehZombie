using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(CountZombiesSystem))]
public sealed class CountZombiesSystem : UpdateSystem
{
    private Filter zombieFilter;
    private Filter healthComponentFilter;
    public override void OnAwake()
    {
        zombieFilter = this.World.Filter.With<ZombieComponent>().Build();
        healthComponentFilter = this.World.Filter.With<HealthComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        int count = 0;
        foreach (var zombie in zombieFilter)
        {
            count++;
        }

        healthComponentFilter.FirstOrDefault().GetComponent<HealthComponent>().zombiesText.text = $"Zombies : {count}";
    }
}