using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CodeBase.Factory
{
    public class Factory : MonoBehaviour
    {
        [SerializeField] private FactoryProperties factoryProperties;
        [SerializeField] private GameObject resourcePrefab;
        [SerializeField] private Transform resourcesHolder;
        [SerializeField] private ProductionProgressBar progressBar;
        [SerializeField] private TextMeshProUGUI stopProductionText;
        
        private float resourceYOffset;
        private const string maxCapacityStopReason = "maximum capacity reached !";
        private const string noResourcesStopReason = "necessary resources run out !";
        private List<GameObject> resources = new List<GameObject>();

        private void Start()
        {
            StartCoroutine(CreateResource());
        }

        private IEnumerator CreateResource()
        {
            while (resources.Count < factoryProperties.MaxCapacity)
            {
                var produceDuration = factoryProperties.ProduceDuration;
                progressBar.UpdateBar(produceDuration);
                yield return new WaitForSeconds(produceDuration);
                var resource = Instantiate(resourcePrefab, resourcesHolder);
                PlaceResource(resource);
                resources.Add(resource);
            }
            DisplayStopHintText(maxCapacityStopReason);
        }

        private void PlaceResource(GameObject resource)
        {
            var localPosition = resource.transform.localPosition;
            resource.transform.localPosition = new Vector3(localPosition.x, resourceYOffset, localPosition.z);
            resourceYOffset += resourcePrefab.transform.localScale.y + 0.05f;
        }
        
        public void DisplayStopHintText(string reason)
        {
            stopProductionText.text = "Stopped resource nr. " + factoryProperties.ResourceNr + " production, " + reason;
            var textInstance = Instantiate(stopProductionText, stopProductionText.transform.parent);
            textInstance.gameObject.SetActive(true);
            textInstance.transform.DOMoveY(textInstance.transform.position.y + 50f, 3f);
            textInstance.DOFade(0f, 2f).SetDelay(1f).OnComplete(() => { Destroy(textInstance.gameObject); });;
        }
 
    }
}
