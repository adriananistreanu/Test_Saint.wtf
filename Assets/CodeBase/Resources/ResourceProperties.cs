using UnityEngine;

namespace CodeBase.Resources
{
    public enum ResourceType
    {
        Resource1,
        Resource2,
        Resource3
    }
    
    [CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resources", order = 0)]
    public class ResourceProperties : ScriptableObject
    {
        [SerializeField] private ResourceType type;
        [SerializeField] private Material material;
        
        public ResourceType Type => type;

        public Material Material => material;
    }
}