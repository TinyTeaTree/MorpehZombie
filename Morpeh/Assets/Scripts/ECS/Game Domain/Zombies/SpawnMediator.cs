using System.Collections.Generic;
using Scellecs.Morpeh;
using UnityEngine;

public class SpawnMediator : MonoBehaviour, SpawnMediator.IView
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private float spawnRate;

    public interface IView
    {
        Vector3 GetRandomSpawnPoint();
    }

    public Vector3 GetRandomSpawnPoint()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
    }

    private void Start() //I have no idea if this is ok to do it like this, But I dont want to prefab + Initialize this.
    {
        ref var component = ref World.Default.CreateEntity().AddComponent<SpawnComponent>();

        component.view = this;
        component.spawnRate = this.spawnRate;
        component.lastSpawnTime = 0f;
        component.withVisibility = true;
    }
}
