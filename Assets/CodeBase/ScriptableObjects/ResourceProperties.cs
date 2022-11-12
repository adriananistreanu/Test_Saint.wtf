using CodeBase.Resources;
using UnityEngine;

namespace CodeBase.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Resource", menuName = "ScriptableObjects/Resources", order = 0)]
    public class ResourceProperties : ScriptableObject
    {
        [SerializeField] private int number;
        [SerializeField] private ResourceType type;
        [SerializeField] private Material material;
        
        public int Number => number;

        public ResourceType Type => type;

        public Material Material => material;
    }
}