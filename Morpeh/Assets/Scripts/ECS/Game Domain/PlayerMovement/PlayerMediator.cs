using ECS.Game_Domain.PlayerFire;
using Misc;
using UnityEngine;

namespace ECS.Game_Domain.PlayerMovement
{
    public class PlayerMediator : MonoBehaviour, PlayerMediator.IView
    {
        [SerializeField] private GunMediator gun;
        public GunMediator Gun => gun;
        
        public interface IView
        {
            void SetPos(Vector2 pos);
            void SetRot(float rot);
            
            Vector3 WorldPos { get; } //For camera calculations
        }

        public void SetPos(Vector2 pos)
        {
            transform.localPosition = new Vector3(pos.x, pos.y, Layers.DudeZLayer);
        }

        public void SetRot(float rot)
        {
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
        
        public Vector3 WorldPos => transform.position;
    }
}