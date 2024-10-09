using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletMovementSystem))]
public sealed class BulletMovementSystem : UpdateSystem
{
    private Filter bulletFilter;
    
    public override void OnAwake()
    {
        this.bulletFilter = this.World.Filter.With<BulletComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        foreach (var entity in this.bulletFilter)
        {
            ref var bullet = ref entity.GetComponent<BulletComponent>();

            bullet.pos = bullet.pos + bullet.direction * bullet.speed * deltaTime;
            
            bullet.view.SetPos(bullet.pos);

            bullet.timeLeft -= deltaTime;

            if (bullet.timeLeft < 0)
            {
                entity.RemoveComponent<BulletComponent>();
                this.World.RemoveEntity(entity);
            }
        }
    }
}