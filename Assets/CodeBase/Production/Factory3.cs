using System.Collections.Generic;
using CodeBase.Resources;
using CodeBase.Warehouses;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Production
{
    public class Factory3 : Factory
    {
        [SerializeField] private ReceivingWarehouse receivingWarehouse;

        private List<Resource> currentResources = new List<Resource>();

        protected override void CreateResources()
        {
            if (!producedWarehouse.ReachMaxCapacity())
            {
                if (ProductionTimePassed == 0f && receivingWarehouse.CheckResourcesAvailability())
                    TakeResourceFromReceiving();

                if (currentResources.Count > 0)
                {
                    ProductionTimePassed += Time.deltaTime;
                    factoryUI.UpdateProductionBar(ProductionTimePassed, produceDuration);

                    if (ProductionTimePassed >= produceDuration)
                        EndResourceProduction();
                    
                }
            }
        }

        protected override void EndResourceProduction()
        {
            DestroyPreviousResources();
            InstantiateResource();
            GiveResourceToProduced();

            if (producedWarehouse.ReachMaxCapacity())
                factoryUI.DisplayStopHintText(StopReasons.MaxCapacity, number);
            else if (!receivingWarehouse.CheckResourcesAvailability())
                factoryUI.DisplayStopHintText(StopReasons.NoResources, number);
            ResetProductionTime();
        }

        private void TakeResourceFromReceiving()
        {
            currentResources = receivingWarehouse.GiveResourcesToFactory();
            PlaceResources();
        }

        private void DestroyPreviousResources()
        {
            foreach (var resource in currentResources)
            {
                Destroy(resource.gameObject);
            }

            currentResources.Clear();
        }

        private void PlaceResources()
        {
            foreach (var resource in currentResources)
            {
                resource.transform.parent = resourceHolder;
                resource.transform.DOLocalMove(Vector3.zero, 0.3f);
            }
        }
    }
}