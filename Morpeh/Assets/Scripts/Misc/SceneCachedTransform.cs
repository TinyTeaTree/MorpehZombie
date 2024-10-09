using System;
using UnityEngine;

namespace Misc
{
    public class SceneCachedTransform : MonoBehaviour
    {
        [SerializeField] private string id;
        private void Awake()
        {
            SceneTransformCache.Set(id, transform);
        }

        private void OnDestroy()
        {
            SceneTransformCache.Remove(id);
        }
    }
}