using UnityEngine;
using UnityEngine.Serialization;

namespace ECS.Game_Domain.PlayerFire
{
    public class GunMediator : MonoBehaviour, GunMediator.IView
    {
        [SerializeField] private Animation anim;
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private float fireRate;
        [SerializeField] private int bulletCapacity;
        [SerializeField] private float reloadDuration;
        
        public interface IView
        {
            void Recoil();
            Vector3 SpawnPoint { get; }
            Vector2 Direction { get; }
        }

        public void Recoil()
        {
            anim.Play("Recoil");
        }

        public Vector3 SpawnPoint => spawnPoint.position;
        public Vector2 Direction => spawnPoint.up;

        public void SetUp(ref GunComponent gun)
        {
            gun.view = this;
            gun.lastFireTime = 0f;
            gun.fireRate = this.fireRate;
            gun.bulletCapacity = this.bulletCapacity;
            gun.reloadTime = this.reloadDuration;

            gun.ammoLeft = gun.bulletCapacity;
            gun.isLoaded = true;
            gun.startReloadTime = 0f;
        }
    }
}