using UnityEngine;

namespace CodeBase.Production
{
    public class Factory1 : Factory
    {
        protected override void CreateResources()
        {
            if (!producedWarehouse.ReachMaxCapacity())
            {
                ProductionTimePassed += Time.deltaTime;
                factoryUI.UpdateProductionBar(ProductionTimePassed, produceDuration);

                if (ProductionTimePassed >= produceDuration)
                    EndResourceProduction();
            }
        }

        protected override void EndResourceProduction()
        {
            InstantiateResource();
            GiveResourceToProduced();

            if (producedWarehouse.ReachMaxCapacity())
                factoryUI.DisplayStopHintText(StopReasons.MaxCapacity, number);

            ResetProductionTime();
        }
    }
}