using CodeBase.ScriptableObjects;
using UnityEngine;

namespace CodeBase.Resources
{
    public enum ResourceType
    {
        Resource1,
        Resource2,
        Resource3
    }
    public class Resource : MonoBehaviour
    {
        [SerializeField] private MeshRenderer meshRenderer;

        private ResourceType type;
        private ResourceProperties properties;

        public ResourceType Type => type;

        public void Configure(ResourceProperties resourceProperties)
        {
            meshRenderer.material = resourceProperties.Material;
            properties = resourceProperties;
            type = properties.Type;
        }
    }
}