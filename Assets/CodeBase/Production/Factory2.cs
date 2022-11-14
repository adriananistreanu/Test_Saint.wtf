using CodeBase.Warehouses;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Production
{
    public class Factory2 : Factory
    {
        [SerializeField] private ReceivingWarehouse receivingWarehouse;

        protected override void CreateResources()
        {
            if (!producedWarehouse.ReachMaxCapacity())
            {
                if (ProductionTimePassed == 0f && receivingWarehouse.CheckResourcesAvailability())
                    TakeResourceFromReceiving();

                if (CurrentResource)
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
            DestroyPreviousResource();
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
            CurrentResource = receivingWarehouse.GiveResourcesToFactory()[0];
            PlaceResource();
        }

        private void DestroyPreviousResource()
        {
            Destroy(CurrentResource.gameObject);
        }

        private void PlaceResource()
        {
            CurrentResource.transform.parent = resourceHolder;
            CurrentResource.transform.DOLocalMove(Vector3.zero, 0.3f);
        }
    }
}