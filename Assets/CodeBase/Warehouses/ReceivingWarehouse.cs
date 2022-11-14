using System.Collections.Generic;
using System.Linq;
using CodeBase.Resources;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Warehouses
{
    public class ReceivingWarehouse : Warehouse
    {
        [SerializeField] private List<ResourceType> necessaryResources;

        public List<Resource> ReceiveResourcesFromPlayer(List<Resource> playerResources)
        {
            var tempResList = new List<Resource>();
            foreach (var resource in playerResources)
            {
                if (necessaryResources.Contains(resource.Type) && !CheckMaxCountOfType(resource.Type, tempResList))
                {
                    tempResList.Add(resource);
                    PlaceResource(resource);

                    if (ReachMaxCapacity(tempResList.Count + resources.Count)) return tempResList;
                }
            }
            return tempResList;
        }

        private bool CheckMaxCountOfType(ResourceType resourceType, List<Resource> tempList)
        {
            var totalResources = new List<Resource>();
            totalResources.AddRange(tempList);
            totalResources.AddRange(resources);
            var maxCount = maxCapacity / necessaryResources.Count;
            var totalOfType = totalResources.Count(resource => resource.Type == resourceType);
          
            return totalOfType >= maxCount;
        }


        public List<Resource> GiveResourcesToFactory()
        {
            var factoryNecessaryResources = FactoryNecessaryResources();
            foreach (var resource in factoryNecessaryResources)
            {
                RemoveResource(resource);
            }

            RearrangeResources();
            return factoryNecessaryResources;
        }

        public override bool CheckResourcesAvailability()
        {
            var factoryNecessaryResources = FactoryNecessaryResources();
            return factoryNecessaryResources.Count > 0;
        }

        private List<Resource> FactoryNecessaryResources()
        {
            var factoryNecessaryResources = new List<Resource>();
            var matchResources = necessaryResources.Select(necessaryResource => resources.Find(x => x.Type == necessaryResource));
            foreach (var matchResource in matchResources)
            {
                if (matchResource && !factoryNecessaryResources.Exists(x => x.Type == matchResource.Type))
                    factoryNecessaryResources.Add(matchResource);

                if (factoryNecessaryResources.Count >= necessaryResources.Count)
                    return factoryNecessaryResources;
            }

            factoryNecessaryResources.Clear();
            return factoryNecessaryResources;
        }

        protected override void PlaceResource(Resource resource)
        {
            base.PlaceResource(resource);
            resource.transform.DOLocalMove(PlaceResourcePosition(resource), 0.3f).OnComplete(() => { AddResource(resource); });
            resource.transform.DOLocalRotate(Vector3.zero, 0.3f);
        }
    }
}