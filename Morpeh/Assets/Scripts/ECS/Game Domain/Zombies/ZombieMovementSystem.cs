using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ZombieMovementSystem))]
public sealed class ZombieMovementSystem : UpdateSystem
{
    static readonly Vector2 up = new Vector2(0f, 1f);

    private Filter zombieFilter;
    private Filter playerFilter;
    
    public override void OnAwake()
    {
        zombieFilter = this.World.Filter.With<ZombieComponent>().Build();
        playerFilter = this.World.Filter.With<PlayerComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        var playerPos = playerFilter.FirstOrDefault().GetComponent<PlayerComponent>().pos;
        
        foreach (var zombieEntity in zombieFilter)
        {
            ref var zombie = ref zombieEntity.GetComponent<ZombieComponent>();

            var dir = (playerPos - zombie.pos).normalized;

            var angle = Vector2.SignedAngle(up, dir);

            var evaluated = zombie.curve.Evaluate(zombie.normalizedTime);
            zombie.normalizedTime += deltaTime / zombie.length;
            if (zombie.normalizedTime > 1f)
            {
                zombie.normalizedTime -= 1f;
            }
            
            zombie.rot = Mathf.LerpAngle(zombie.rot, angle, deltaTime);
            zombie.view.SetRot(zombie.rot);
            
            zombie.pos = zombie.pos + zombie.view.Dir * zombie.speed * evaluated * deltaTime;
            
            zombie.view.SetPos(zombie.pos);
        }
    }
}