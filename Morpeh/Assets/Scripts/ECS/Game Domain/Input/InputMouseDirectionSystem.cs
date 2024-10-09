using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(InputMouseDirectionSystem))]
public sealed class InputMouseDirectionSystem : UpdateSystem {
    private Filter inputFilter;
    private Filter playerFilter;
    private Filter cameraFilter;
    
    public override void OnAwake()
    {
        this.inputFilter = this.World.Filter.With<InputComponent>().Build();
        this.playerFilter = this.World.Filter.With<PlayerComponent>().Build();
        this.cameraFilter = this.World.Filter.With<CameraComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        var inputEntity = inputFilter.FirstOrDefault();
        if (inputEntity == null)
        {
            Debug.LogWarning($"{nameof(InputMouseDirectionSystem)} has no entity to work with, consider deactivating the system when no Input is needed or provided");
            return;
        }

        var playerEntity = this.playerFilter.FirstOrDefault();
        if (playerEntity == null)
        {
            Debug.LogWarning($"{nameof(InputMouseDirectionSystem)} has no entity to work with, consider deactivating the system when no {nameof(PlayerComponent)} is needed or provided");
            return;
        }

        var cameraEntity = this.cameraFilter.FirstOrDefault();
        if (cameraEntity == null)
        {
            Debug.LogWarning($"{nameof(InputMouseDirectionSystem)} has no entity to work with, consider deactivating the system when no {nameof(CameraComponent)} is needed or provided");
            return;
        }
        
        ref var player = ref playerEntity.GetComponent<PlayerComponent>();
        ref var input = ref inputEntity.GetComponent<InputComponent>();
        var camera = cameraEntity.GetComponent<CameraComponent>().camera;
        Vector2 screenPoint = camera.WorldToScreenPoint(player.view.WorldPos);
        Vector2 mousePixelCoordinates = Input.mousePosition;
        
        var delta = mousePixelCoordinates - screenPoint;
        input.direction = delta.normalized;
    }
}