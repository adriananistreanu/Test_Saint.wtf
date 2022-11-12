using System.Collections.Generic;
using CodeBase.Resources;
using UnityEngine;

namespace CodeBase.Factory
{
    public class Warehouse : MonoBehaviour
    {
        [SerializeField] private Factory factory;
        [SerializeField] private FactoryUI factoryUI;
        [SerializeField] private int maxCapacity;
        [SerializeField] private Transform resourcesHolder;

        private float resourceYOffset;
        private const float resourceYOffsetAdd = 0.05f;
        private List<Resource> resources = new List<Resource>();

        public Transform ResourcesHolder => resourcesHolder;


        public void CreatedResource(Resource resource)
        {
            PlaceResource(resource);
            resources.Add(resource);
            factoryUI.UpdateResourceCountDisplay(resources.Count, maxCapacity);
        }
        
        private void PlaceResource(Resource resource)
        {
            var localPosition = resource.transform.localPosition;
            resource.transform.localPosition = new Vector3(localPosition.x, resourceYOffset, localPosition.z);
            resourceYOffset += resource.transform.localScale.y + resourceYOffsetAdd;
        }
        
        public List<Resource> GetResources(int count)
        {
            var resourcesToGet = new List<Resource>();

            for (int i = resources.Count - 1; i >= 0 && resourcesToGet.Count < count; i--)
            {
                resourcesToGet.Add(resources[i]);
                ClearResource(resources[i]);
            }

            return resourcesToGet;
        }
        
        private void ClearResource(Resource resource)
        {
            resources.Remove(resource);
            DecrementResourcePlaceOffset(resource);
            factoryUI.UpdateResourceCountDisplay(resources.Count, maxCapacity);
            factory.ResetProductionTime();
        }
        
        private void DecrementResourcePlaceOffset(Resource resource)
        {
            resourceYOffset -= resource.transform.localScale.y + resourceYOffsetAdd;
        }

        public bool CheckResourcesAvailability()
        {
            return resources.Count >= 0;
        }
        
        public bool ReachMaxCapacity() => resources.Count >= maxCapacity;
    }
}
