using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ControlPanelSystem))]
public sealed class ControlPanelSystem : UpdateSystem
{
    private Filter filter;
    private Filter spawnFilter;
    private Filter zombieFilter;
    public override void OnAwake()
    {

        filter = this.World.Filter.With<ControlPanelComponent>().Build();
        spawnFilter = this.World.Filter.With<SpawnComponent>().Build();
        zombieFilter = this.World.Filter.With<ZombieComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        ref var controlPanel = ref filter.FirstOrDefault().GetComponent<ControlPanelComponent>();

        if (!controlPanel.isAssigned)
        {
            controlPanel.add10ZombiesPerSecondButton.onClick.AddListener(() =>
            {
                ref var spawnComponent = ref spawnFilter.FirstOrDefault().GetComponent<SpawnComponent>();
                spawnComponent.spawnRate += 10f;
            });
            
            controlPanel.add100ZombiesPerSecondButton.onClick.AddListener(() =>
            {
                ref var spawnComponent = ref spawnFilter.FirstOrDefault().GetComponent<SpawnComponent>();
                spawnComponent.spawnRate += 100f;
            });
            
            controlPanel.toggleVisibilityButton.onClick.AddListener(() =>
            {
                ref var spawner = ref spawnFilter.FirstOrDefault().GetComponent<SpawnComponent>();
                spawner.withVisibility = !spawner.withVisibility;

                foreach (var zombie in zombieFilter)
                {
                    zombie.GetComponent<ZombieComponent>().view.SetVisibility(spawner.withVisibility);
                }
            });

            controlPanel.isAssigned = true;
        }
    }
}