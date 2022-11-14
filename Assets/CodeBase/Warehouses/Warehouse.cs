using System.Collections.Generic;
using CodeBase.Production;
using CodeBase.Resources;
using TMPro;
using UnityEngine;

namespace CodeBase.Warehouses
{
    public class Warehouse : MonoBehaviour
    {
        [SerializeField] protected Factory factory;
        [SerializeField] protected int maxCapacity;
        [SerializeField] protected Transform resourcesHolder;
        [SerializeField] private TextMeshProUGUI resourcesCountText;

        private float resourceYOffset;
        private const float ResourceYOffsetAdd = 0.05f;
        protected List<Resource> resources = new List<Resource>();

        private void Start()
        {
            UpdateResourceCountDisplay();
        }

        protected void AddResource(Resource resource)
        {
            resources.Add(resource);
            UpdateResourceCountDisplay();
        }

        protected virtual void RemoveResource(Resource resource)
        {
            resources.Remove(resource);
            DecrementResourcePlaceOffset(resource);
            UpdateResourceCountDisplay();
        }

        public virtual bool CheckResourcesAvailability() => resources.Count > 0;

        public bool ReachMaxCapacity() => resources.Count >= maxCapacity;

        protected bool ReachMaxCapacity(float count) => count >= maxCapacity;

        protected virtual void PlaceResource(Resource resource)
        {
            resource.transform.parent = resourcesHolder;
        }

        protected Vector3 PlaceResourcePosition(Resource resource)
        {
            var placePosition = new Vector3(0f, resourceYOffset, 0f);
            resourceYOffset += resource.transform.localScale.y + ResourceYOffsetAdd;
            return placePosition;
        }

        private void DecrementResourcePlaceOffset(Resource resource)
        {
            resourceYOffset -= resource.transform.localScale.y + ResourceYOffsetAdd;
        }

        protected void RearrangeResources()
        {
            resourceYOffset = 0f;
            foreach (var resource in resources)
            {
                resource.transform.localPosition = PlaceResourcePosition(resource);
            }
        }

        private void UpdateResourceCountDisplay()
        {
            resourcesCountText.text = resources.Count + "/" + maxCapacity;
        }
    }
}