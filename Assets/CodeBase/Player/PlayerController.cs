using System.Collections.Generic;
using CodeBase.Resources;
using CodeBase.Warehouses;
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
        private const float ResourceYOffsetAdd = 0.05f;

        private List<Resource> resources = new List<Resource>();

        private void Start()
        {
            UpdateCountDisplay();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ProducedWarehouse producedWarehouse))
            {
                if (producedWarehouse.CheckResourcesAvailability() && NecessaryNrOfResources() > 0)
                {
                    GetResources(producedWarehouse);
                }
            }

            if (other.TryGetComponent(out ReceivingWarehouse receivingWarehouse) && AvailableResources())
            {
                var gaveResources = receivingWarehouse.ReceiveResourcesFromPlayer(resources);
                if (gaveResources.Count > 0)
                    ClearGaveResources(gaveResources);
            }
        }
        private void GetResources(ProducedWarehouse producedWarehouse)
        {
            var collectedResources = producedWarehouse.GiveResourcesToPlayer(NecessaryNrOfResources());
            resources.AddRange(collectedResources);
            PlaceResources(collectedResources);
            UpdateCountDisplay();
        }

        private bool AvailableResources() => resources.Count > 0;
        private int NecessaryNrOfResources() => maxResourcesCapacity - resources.Count;

        private void ClearGaveResources(List<Resource> gaveResources)
        {
            foreach (var gaveResource in gaveResources)
            {
                resources.Remove(gaveResource);
            }

            RearrangeResources();
            UpdateCountDisplay();
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
            resourceYOffset += resource.transform.localScale.y + ResourceYOffsetAdd;
            return placePosition;
        }

        private void RearrangeResources()
        {
            resourceYOffset = 0f;
            foreach (var resource in resources)
            {
                resource.transform.localPosition = PlaceResourcePosition(resource);
            }
        }

        private void UpdateCountDisplay()
        {
            resourceCountText.text = resources.Count + "/" + maxResourcesCapacity;
        }
    }
}