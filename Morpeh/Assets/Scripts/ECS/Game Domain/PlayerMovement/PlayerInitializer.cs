using ECS.Game_Domain.PlayerMovement;
using Misc;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Initializers/" + nameof(PlayerInitializer))]
public sealed class PlayerInitializer : Initializer
{
    [SerializeField] private string playerPrefabPath = "Dude";
    [SerializeField] private float playerSpeed = 5f;
    
    public override void OnAwake() {
        var playerEntity = this.World.CreateEntity();
        ref var player = ref playerEntity.AddComponent<PlayerComponent>();
        ref var gun = ref playerEntity.AddComponent<GunComponent>();

        var parent = SceneTransformCache.get(SceneIds.SceneRoot);
        PlayerMediator playerMediator = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<PlayerMediator>(playerPrefabPath), parent);
        playerMediator.Gun.SetUp(ref gun);
        
        player.view = playerMediator;
        
        playerMediator.SetPos(player.pos);
        playerMediator.SetRot(player.rot);

        var systemGroup = this.World.CreateSystemsGroup();

        var movementSystem = ScriptableObject.CreateInstance<PlayerMovementSystem>();
        movementSystem.SetSpeed(playerSpeed);
        
        systemGroup.AddSystem(movementSystem);
        systemGroup.AddSystem(ScriptableObject.CreateInstance<PlayerRotationSystem>());
        systemGroup.AddSystem(ScriptableObject.CreateInstance<PlayerFireSystem>());
        systemGroup.AddSystem(ScriptableObject.CreateInstance<BulletMovementSystem>());
        systemGroup.AddSystem(ScriptableObject.CreateInstance<SpawnSystem>());
        systemGroup.AddSystem(ScriptableObject.CreateInstance<ZombieMovementSystem>());
        systemGroup.AddSystem(ScriptableObject.CreateInstance<BulletCollisionSystem>());
        systemGroup.AddSystem(ScriptableObject.CreateInstance<ControlPanelSystem>());
        systemGroup.AddSystem(ScriptableObject.CreateInstance<CountZombiesSystem>());
        this.World.AddSystemsGroup(2, systemGroup);
        
        World.GetStash<BulletComponent>().AsDisposable();
        World.GetStash<ZombieComponent>().AsDisposable();
    }

    public override void Dispose() {
    }
}