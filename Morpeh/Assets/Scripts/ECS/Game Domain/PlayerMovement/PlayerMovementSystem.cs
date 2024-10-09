using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerMovementSystem))]
public sealed class PlayerMovementSystem : UpdateSystem
{
    private Filter playerFilter;
    private Filter inputFilter;

    private float speed;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    
    public override void OnAwake()
    {
        this.playerFilter = this.World.Filter.With<PlayerComponent>().Build();
        this.inputFilter = this.World.Filter.With<InputComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
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

        var vector = Vector2.zero; //vector = movement
        if (input.isPressingDown)
        {
            vector.y -= 1f;
        }

        if (input.isPressingUp)
        {
            vector.y += 1f;
        }

        if (input.isPressingLeft)
        {
            vector.x -= 1f;
        }

        if (input.isPressingRight)
        {
            vector.x += 1f;
        }

        vector = vector * this.speed * Time.deltaTime; //vector = relative movement
        player.pos = player.pos + vector; //player.pos = final pos for this frame
        
        player.view.SetPos(player.pos);
    }
}