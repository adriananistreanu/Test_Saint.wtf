using UnityEngine;

namespace CodeBase.Resources
{
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