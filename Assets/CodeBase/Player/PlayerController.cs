using System;
using System.Collections.Generic;
using CodeBase.Factory;
using CodeBase.Resources;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private int maxResourcesCapacity;
        [SerializeField] private Transform resourcesHolder;
        [SerializeField] private TextMeshPro resourceCountText;
        
        private float resourceYOffset;

        private List<Resource> resources = new List<Resource>();

        private void Start()
        {
            UpdateCountDisplay();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Warehouse warehouse))
            {
                if (warehouse.CheckResourcesAvailability() && NecessaryNrOfResources() > 0)
                {
                    GetResources(warehouse);
                }
            }

            if (other.TryGetComponent(out ReceivingWarehouse receivingWarehouse) && AvailableResources())
            {
                receivingWarehouse.ReceiveResources(resources);
            }
        }

        private void GetResources(Warehouse warehouse)
        {
            var collectedResources = warehouse.GetResources(NecessaryNrOfResources());
            resources.AddRange(collectedResources);
            PlaceResources(collectedResources);
            UpdateCountDisplay();
        }

        private int NecessaryNrOfResources()
        {
            return maxResourcesCapacity - resources.Count;
        }

        private bool AvailableResources()
        {
            return resources.Count > 0;
        }

        private void PlaceResources(List<Resource> collectedResources)
        {
            foreach (var resource in collectedResources)
            {
                resource.transform.parent = resourcesHolder;
                resource.transform.DOLocalMove(PlaceResourcePosition(resource), 0.3f);
                resource.transform.DOLocalRotate(Vector3.zero, 0.3f);
            }
        }
        
        private Vector3 PlaceResourcePosition(Resource resource)
        {
            var placePosition = new Vector3(0f, resourceYOffset, 0f);
            resourceYOffset += resource.transform.localScale.y + 0.05f;
            return placePosition;
        }

        private void UpdateCountDisplay()
        {
            resourceCountText.text = resources.Count + "/" + maxResourcesCapacity;
        }
    }
}