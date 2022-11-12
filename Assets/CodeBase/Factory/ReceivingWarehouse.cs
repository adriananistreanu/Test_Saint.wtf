using System.Collections.Generic;
using CodeBase.Resources;
using UnityEngine;

namespace CodeBase.Factory
{
    public class ReceivingWarehouse : MonoBehaviour
    {
        [SerializeField] private Factory2 factory2;
        [SerializeField] private List<ResourceType> neccesaryResources;
        
        private List<Resource> resources = new List<Resource>();

        public void ReceiveResources(List<Resource> playerResources)
        {
            foreach (var resource in playerResources)
            {
                if (neccesaryResources.Contains(resource.Type))
                {
                    resources.Add(resource);
                }
            }
        }
    }
}