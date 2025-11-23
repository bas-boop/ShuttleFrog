using UnityEngine;

using Framework.Extensions;

namespace UI.Canvas.GameplayHud
{
    public sealed class SpeedBoostUI : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float width = 2;
        [SerializeField] private float fillTime = 0.25f;
        [SerializeField] private float depleteTime = 0.25f;

        private bool _isDepleting;
        private bool _isFilling;
        private float _currentWidth = 1;
        
        private void Update()
        {
            if (_isDepleting && !_isFilling)
            {
                _currentWidth -= Time.deltaTime * depleteTime;
                
                if (_currentWidth <= 0f)
                {
                    _currentWidth = 0f;
                    _isDepleting = false;
                }

                SetWidth(_currentWidth);
                return;
            }

            if (_isFilling && !_isDepleting)
            {
                _currentWidth += Time.deltaTime * fillTime;
                
                if (_currentWidth >= 1f)
                {
                    _currentWidth = 1f;
                    _isFilling = false;
                }

                SetWidth(_currentWidth);
                return;
            }
        }

        public void SetWidth(float target)
        {
            target = Mathf.Clamp01(target);
            
            Vector3 newScale = rectTransform.localScale;
            newScale.SetX(target * width);
            rectTransform.localScale = newScale;
        }

        public void DepleteWidthOverTime()
        {
            _isFilling = false;
            _isDepleting = true;
        }

        public void FillWidthOverTime()
        {
            _isDepleting = false;
            _isFilling = true;
        }
    }
}