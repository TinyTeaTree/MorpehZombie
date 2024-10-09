using System;
using System.Collections;
using Misc;
using Scellecs.Morpeh;
using UnityEngine;

public class ZombieMediator : MonoBehaviour, ZombieMediator.IView
{
    [SerializeField] private float speed = 3;
    [SerializeField] private int hp = 10;
    [SerializeField] private Transform body;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float length;
    [SerializeField] private SpriteRenderer spriteRender;
    
    public EntityId entityId { get; private set; }

    public interface IView : IDisposable
    {
        void SetPos(Vector2 pos);
        void SetRot(float rot);
        void SetVisibility(bool spawnerWithVisibility);
        
        Vector2 Dir { get; }
    }

    public void SetWorldPos(Vector3 worldPos)
    {
        transform.position = new Vector3(worldPos.x, worldPos.y, Layers.ZombieZLayer);
    }

    public void SetPos(Vector2 pos)
    {
        transform.localPosition = new Vector3(pos.x, pos.y, Layers.ZombieZLayer);
    }

    public void SetRot(float rot)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, rot);
    }

    public Vector2 Dir => body.right;

    public void SetUp(ref ZombieComponent zombie, EntityId entityId)
    {
        zombie.view = this;
        zombie.pos = transform.position - transform.parent.position;
        zombie.rot = transform.rotation.eulerAngles.z;
        zombie.speed = this.speed;
        zombie.hp = this.hp;

        zombie.curve = this.curve;
        zombie.normalizedTime = 0f;
        zombie.length = this.length;

        this.entityId = entityId;
    }

    public void GetHit()
    {
        StopAllCoroutines();
        StartCoroutine(GetHitRoutine());
    }

    public AnimationCurve hitCurve;
    private IEnumerator GetHitRoutine() //TODO: integrate with ECS
    {
        float timePassed = 0f;
        while (timePassed < 0.4f)
        {
            var evaluation = hitCurve.Evaluate(timePassed / 0.4f);

            spriteRender.color = Color.Lerp(Color.white, Color.red, evaluation);

            yield return null;
            timePassed += Time.deltaTime;
        }
    }

    public void Dispose()
    {
        if (this != null)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetVisibility(bool spawnerWithVisibility)
    {
        gameObject.SetActive(spawnerWithVisibility);
    }
}
