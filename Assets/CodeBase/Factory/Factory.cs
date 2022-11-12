using System.Collections.Generic;
using CodeBase.ScriptableObjects;
using CodeBase.Resources;
using UnityEngine;

namespace CodeBase.Factory
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] protected float produceDuration;
        [SerializeField] protected ResourceProperties resourceProperties;
        [SerializeField] private Resource resourcePrefab;
        [SerializeField] protected FactoryUI factoryUI;
        [SerializeField] protected Warehouse warehouse;

        protected float productionTimePassed = 0f;
        protected const string maxCapacityStopReason = "maximum capacity reached !";
        private void Update()
        {
            CreateResources();
        }

        protected virtual void CreateResources()
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

        protected void InstantiateResource()
        {
            var resource = Instantiate(resourcePrefab, warehouse.ResourcesHolder);
            resource.Configure(resourceProperties);
            warehouse.CreatedResource(resource);
        }
        

        public void ResetProductionTime() => productionTimePassed = 0f;
        
    }
}