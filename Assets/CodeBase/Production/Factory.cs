using CodeBase.Resources;
using CodeBase.Warehouses;
using UnityEngine;

namespace CodeBase.Production
{
    public abstract class Factory : MonoBehaviour
    {
        [SerializeField] protected float produceDuration;
        [SerializeField] protected int number;
        [SerializeField] protected ResourceProperties resourceProperties;
        [SerializeField] private Resource resourcePrefab;
        [SerializeField] protected Transform resourceHolder;
        [SerializeField] protected FactoryUI factoryUI;
        [SerializeField] protected ProducedWarehouse producedWarehouse;

        protected Resource CurrentResource;
        protected float ProductionTimePassed = 0f;

        private void Update()
        {
            CreateResources();
        }

        protected abstract void CreateResources();

        protected abstract void EndResourceProduction();

        protected void InstantiateResource()
        {
            CurrentResource = Instantiate(resourcePrefab, resourceHolder);
            CurrentResource.Configure(resourceProperties);
        }

        protected void GiveResourceToProduced()
        {
            var resource = CurrentResource;
            producedWarehouse.ReceiveResourceFromFactory(resource);
            CurrentResource = null;
        }

        public void ResetProductionTime()
        {
            ProductionTimePassed = 0f;
            factoryUI.UpdateProductionBar(ProductionTimePassed, produceDuration);
        }
    }
}