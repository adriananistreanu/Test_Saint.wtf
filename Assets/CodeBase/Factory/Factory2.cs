using System.Collections.Generic;
using CodeBase.Resources;
using UnityEngine;

namespace CodeBase.Factory
{
    public class Factory2 : Factory
    {
       private const string noResourcesStopReason = "necessary resources run out !";
        
        protected override void CreateResources()
        {
            if (!warehouse.ReachMaxCapacity())
            {
                productionTimePassed += Time.deltaTime;
                factoryUI.UpdateProductionBar(productionTimePassed, produceDuration);

                if (productionTimePassed >= produceDuration)
                {
                    InstantiateResource();
                    
                    if (warehouse.ReachMaxCapacity())
                        factoryUI.DisplayStopHintText(maxCapacityStopReason, resourceProperties.Number);

                    ResetProductionTime();
                }
            }
        }
    }
}