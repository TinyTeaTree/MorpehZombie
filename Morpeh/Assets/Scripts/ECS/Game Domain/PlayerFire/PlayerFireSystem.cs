using Misc;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.DivideByZeroChecks, false)]
[CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerFireSystem))]
public sealed class PlayerFireSystem : UpdateSystem
{
    static readonly Vector2 up = new Vector2(0f, 1f);
    
    private Filter inputFilter;
    private Filter playerGunFilter;
    
    public override void OnAwake()
    {
        this.inputFilter = this.World.Filter.With<InputComponent>().Build();
        this.playerGunFilter = this.World.Filter.With<PlayerComponent>().With<GunComponent>().Build();
    }

    public override void OnUpdate(float deltaTime)
    {
        var inputEntity = this.inputFilter.FirstOrDefault();

        var input = inputEntity.GetComponent<InputComponent>();
        if (!input.isFirePressed)
        {
            return;
        }

        foreach (var entity in this.playerGunFilter)
        {
            ref var gun = ref entity.GetComponent<GunComponent>();
            
            if (!gun.isLoaded)
            {
                if (gun.startReloadTime + gun.reloadTime > Time.time)
                {
                    gun.isLoaded = true;
                    gun.ammoLeft = gun.bulletCapacity;
                }
                else
                {
                    continue;
                }
            }

            var timePassedSinceLastFire = Time.time - gun.lastFireTime;
            var timeToFireAgain = 1f / gun.fireRate;

            if (timeToFireAgain <= timePassedSinceLastFire) //This code might be susceptible to Frame Rate drops. Maybe a Fixed Update system is better here.
            {
                gun.view.Recoil();
                var spawnPoint = gun.view.SpawnPoint;
                var direction = gun.view.Direction;
                var parent = SceneTransformCache.get(SceneIds.BulletRoot);
                
                //TODO: Add a pooling mechanism. 
                var bulletMediator = UnityEngine.Object.Instantiate(UnityEngine.Resources.Load<BulletMediator>(GunComponent.BulletPrefabPath), parent);
                ref var newBullet = ref this.World.CreateEntity().AddComponent<BulletComponent>();
                bulletMediator.SetWorldPos(spawnPoint);
                bulletMediator.SetRot(Vector2.SignedAngle(up, direction));
                bulletMediator.SetUp(ref newBullet);

                gun.lastFireTime = gun.lastFireTime + timeToFireAgain;
                if (input.isFirePressStarted)
                {
                    gun.lastFireTime = Time.time;
                }
                
                gun.ammoLeft--;
                if (gun.ammoLeft == 0)
                {
                    gun.isLoaded = false;
                    gun.startReloadTime = Time.time;
                }
            }

        }

    }
}