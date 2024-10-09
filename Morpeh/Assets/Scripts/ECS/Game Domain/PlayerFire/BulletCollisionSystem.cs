using Misc;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(BulletCollisionSystem))]
public sealed class BulletCollisionSystem : UpdateSystem
{
    private Filter bulletFilter;
    
    public override void OnAwake()
    {
        bulletFilter = this.World.Filter.With<BulletComponent>().Build();
    }

    public override void OnUpdate(float deltaTime) {
        
        foreach (var bulletEntity in bulletFilter)
        {
            var bulletPos = bulletEntity.GetComponent<BulletComponent>().view.WorldPos;

            var collidedWith = Physics2D.OverlapCircle(new Vector2(bulletPos.x, bulletPos.y), 1f, LayerMask.GetMask("Enemy"), -100f, 100f);
            if(collidedWith != null)
            {
                var zombieMediator = collidedWith.transform.parent.GetComponent<ZombieMediator>();
                zombieMediator.GetHit();
                var zombieId = zombieMediator.entityId;

                /*
                 *Is this Correct? It works, but Is this how we should work in ECS?
                 *
                 *When creating Entities using Providers, Providers have Cached Entities. However I dont like that approach
                 *
                 * Because I feel like Systems are better places to create Entities then for them to be magically created when stuff gets Instantiated.
                 *
                 * Either case, should we cache Entity? or EntityId or work with Custom Ids? or maybe some other solution?
                 *
                 * This seemed like the most clean solution.
                 * 
                 */
                if(this.World.TryGetEntity(zombieId, out var zombieEntity)) 
                {
                    ref var zombie = ref zombieEntity.GetComponent<ZombieComponent>();
                    zombie.hp--;

                    if (zombie.hp <= 0)
                    {
                        zombieEntity.RemoveComponent<ZombieComponent>();
                        this.World.RemoveEntity(zombieEntity);
                    }
                }

                bulletEntity.RemoveComponent<BulletComponent>();
                this.World.RemoveEntity(bulletEntity);
            }
        }
    }
}