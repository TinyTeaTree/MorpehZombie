using Misc;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SpawnSystem))]
public sealed class SpawnSystem : UpdateSystem
{
    static readonly Vector2 up = new Vector2(0f, 1f);

    private Filter spawnFilter;
    
    public override void OnAwake()
    {
        spawnFilter = this.World.Filter.With<SpawnComponent>().Build();

    }

    public override void OnUpdate(float deltaTime)
    {
        ref var spawner = ref spawnFilter.FirstOrDefault().GetComponent<SpawnComponent>();

        var spawnTime = 1f / spawner.spawnRate;
        while (spawner.lastSpawnTime + spawnTime < Time.time)
        {
            var spawnPoint = spawner.view.GetRandomSpawnPoint();
            var parent = SceneTransformCache.get(SceneIds.ZombiesRoot);
            
            //TODO: Add a pooling mechanism. 
            var zombieMediator = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<ZombieMediator>(ZombieComponent.ZombiePrefabPath), parent);
            var newEntity = this.World.CreateEntity();
            ref var newZombie = ref newEntity.AddComponent<ZombieComponent>();
            zombieMediator.SetWorldPos(spawnPoint);
            zombieMediator.SetRot(UnityEngine.Random.Range(0f, 360f));
            zombieMediator.SetUp(ref newZombie, newEntity.ID);
            zombieMediator.SetVisibility(spawner.withVisibility);
            
            
            spawner.lastSpawnTime += spawnTime;
        }
    }
}