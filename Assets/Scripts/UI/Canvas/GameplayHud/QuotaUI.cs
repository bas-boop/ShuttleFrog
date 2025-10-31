using TMPro;
using UnityEngine;

using Framework.DemandSystem;

namespace UI.Canvas.GameplayHud
{
    public sealed class QuotaUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private DemandManager demandManager;

        private int _totalQuota;
        private int _currentQuotaCount;

        private void Start()
        {
            _totalQuota = demandManager.GetDeliveryPointsCount();
            text.text = _currentQuotaCount + "/" + _totalQuota;
        }

        public void UpdateQuota()
        {
            _currentQuotaCount++;
            text.text = _currentQuotaCount + "/" + _totalQuota;
        }

        public void ResetQuota()
        {
            _currentQuotaCount = 0;
            text.text = _currentQuotaCount + "/" + _totalQuota;
        }
    }
}