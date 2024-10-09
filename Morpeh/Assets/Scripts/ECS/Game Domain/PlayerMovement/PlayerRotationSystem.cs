using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerRotationSystem))]
public sealed class PlayerRotationSystem : UpdateSystem {
    static readonly Vector2 up = new Vector2(0f, 1f);
        
    private Filter playerFilter;
    private Filter inputFilter;
    
    public override void OnAwake() {
        this.playerFilter = this.World.Filter.With<PlayerComponent>().Build();
        this.inputFilter = this.World.Filter.With<InputComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        var playerEntity = this.playerFilter.FirstOrDefault();
        if (playerEntity == null)
        {
            Debug.LogWarning($"{nameof(PlayerMovementSystem)} has no entity to work with, consider deactivating the system when no {nameof(PlayerComponent)} is needed or provided");
            return;
        }

        var inputEntity = this.inputFilter.FirstOrDefault();
        if (inputFilter == null)
        {
            Debug.LogWarning($"{nameof(PlayerMovementSystem)} can't work without {nameof(InputComponent)}, make sure its registered");
        }

        var input = inputEntity.GetComponent<InputComponent>();
        ref var player = ref playerEntity.GetComponent<PlayerComponent>();

        var angle = Vector2.SignedAngle(up, input.direction);
        
        player.view.SetRot(angle);
    }
}