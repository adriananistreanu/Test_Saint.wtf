using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Factory
{
    public class FactoryUI : MonoBehaviour
    {
        [SerializeField] private Image productionBar;
        [SerializeField] private TextMeshProUGUI stopProductionText;
        [SerializeField] private TextMeshProUGUI resourceCountText;

        private void Start()
        {
            ResetProductionBar();
        }

        private void ResetProductionBar()
        {
            productionBar.fillAmount = 0f;
        }

        public void UpdateProductionBar(float timePassed, float totalDuration)
        {
           var value = timePassed / totalDuration;
           productionBar.fillAmount = value;
        }
        
        
        public void DisplayStopHintText(string reason, int resourceNr)
        {
            stopProductionText.text = "Stopped resource nr. " + resourceNr + " production, " + reason;
            var textInstance = Instantiate(stopProductionText, stopProductionText.transform.parent);
            textInstance.gameObject.SetActive(true);
            textInstance.transform.DOMoveY(textInstance.transform.position.y + 50f, 3f);
            textInstance.DOFade(0f, 2f).SetDelay(1f).OnComplete(() => { Destroy(textInstance.gameObject); });;
        }
        
        public void UpdateResourceCountDisplay(int count, int maxCapacity)
        {
            resourceCountText.text = count + "/" + maxCapacity;
        }
        
    }
}