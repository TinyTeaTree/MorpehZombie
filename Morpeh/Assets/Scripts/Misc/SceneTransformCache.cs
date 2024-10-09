using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    //I don't have a very good way to do this, I think this simple solution is suffecient for the project at hand.
    public static class SceneTransformCache
    {
        private static Dictionary<string, Transform> cache = new();

        public static void Set(string id, Transform transform)
        {
            cache[id] = transform;
        }
        
        public static void Remove(string id)
        {
            cache.Remove(id);
        }

        public static Transform get(string id)
        {
            return cache[id];
        }
    }
}