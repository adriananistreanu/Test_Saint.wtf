using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Factory
{
    public class ProductionProgressBar : MonoBehaviour
    {
        [SerializeField] private Image fillBar;

        private void Start()
        {
            ResetBar();
        }

        private void ResetBar()
        {
            fillBar.fillAmount = 0f;
        }

        public void UpdateBar(float duration)
        {
            ResetBar();
            fillBar.DOFillAmount(1f, duration).SetEase(Ease.Linear);
        }
        
        
    }
}