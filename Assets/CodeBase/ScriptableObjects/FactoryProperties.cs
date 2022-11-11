using UnityEngine;

namespace CodeBase.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Factory", menuName = "ScriptableObjects/Factory", order = 1)]
    public class FactoryProperties : ScriptableObject
    {
        [SerializeField] private float produceDuration;
        [SerializeField] private int resourceNr;
        [SerializeField] private int maxCapacity;

        public int MaxCapacity => maxCapacity;

        public int ResourceNr => resourceNr;

        public float ProduceDuration => produceDuration;
    }
}