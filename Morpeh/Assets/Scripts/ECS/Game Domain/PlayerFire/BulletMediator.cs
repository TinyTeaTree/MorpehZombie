using System;
using Misc;
using UnityEngine;

public class BulletMediator : MonoBehaviour, BulletMediator.IView
{
    [SerializeField] public float speed;
    [SerializeField] public float duration;
    
    public interface IView : IDisposable
    {
        void SetPos(Vector2 pos);
        void SetRot(float rot);
        
        Vector3 WorldPos { get; }
    }

    public void SetPos(Vector2 pos)
    {
        transform.localPosition = new Vector3(pos.x, pos.y, Layers.BulletZLayer);
    }
    
    public void SetWorldPos(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, Layers.BulletZLayer);
    }

    public void SetRot(float rot)
    {
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }

    public Vector3 WorldPos => transform.position;

    public void SetUp(ref BulletComponent bulletComponent)
    {
        bulletComponent.view = this;
        bulletComponent.pos = transform.position - transform.parent.position;
        bulletComponent.direction = transform.up;
        bulletComponent.speed = speed;
        bulletComponent.timeLeft = duration;
    }

    public void Dispose()
    {
        if (this != null)
        {
            Destroy(gameObject);
        }
    }
}
