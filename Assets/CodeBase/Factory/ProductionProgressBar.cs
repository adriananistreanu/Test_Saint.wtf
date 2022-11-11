using System;
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
            fillBar.fillAmount = 1f;
        }

        public void UpdateBar(float duration)
        {
            ResetBar();
            fillBar.DOFillAmount(0f, duration).SetEase(Ease.Linear);
        }
        
        
    }
}