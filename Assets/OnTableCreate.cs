using Meta.XR.MRUtilityKit;
using UnityEngine;
using UnityEngine.Events;

namespace Meta.XR.MRUtilityKit.BuildingBlocks
{
    public class OnTableCreate : MonoBehaviour
    {
        public GameObject prefab;

        private EffectMesh _effectMesh;
        void Awake()
        {
            MRUK.Instance.SceneLoadedEvent.AddListener(YourStartupCode);
        }

        public void YourStartupCode()
        {
            // query for anchors, build your game world, do game initialization
        }
    }
}

