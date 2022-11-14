using System.Collections.Generic;
using CodeBase.Resources;
using DG.Tweening;

namespace CodeBase.Warehouses
{
    public class ProducedWarehouse : Warehouse
    {
        public void ReceiveResourceFromFactory(Resource resource)
        {
            PlaceResource(resource);
            AddResource(resource);
        }

        public List<Resource> GiveResourcesToPlayer(int count)
        {
            var resourcesToGet = new List<Resource>();

            for (int i = resources.Count - 1; i >= 0 && resourcesToGet.Count < count; i--)
            {
                resourcesToGet.Add(resources[i]);
                RemoveResource(resources[i]);
            }

            return resourcesToGet;
        }

        protected override void RemoveResource(Resource resource)
        {
            base.RemoveResource(resource);
            factory.ResetProductionTime();
        }

        protected override void PlaceResource(Resource resource)
        {
            base.PlaceResource(resource);
            resource.transform.DOLocalMove(PlaceResourcePosition(resource), 0.3f);
        }
    }
}
