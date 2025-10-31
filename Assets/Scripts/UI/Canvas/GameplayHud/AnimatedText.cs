using System.Collections;
using TMPro;
using UnityEngine;

namespace UI.Canvas.GameplayHud
{
    public class AnimatedText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private float displayDuration = 2f;
        [SerializeField] private float rotationAmount = 15f;
        [SerializeField] private float rotationSpeed = 2f;

        private RectTransform _rectTransform;
        private bool _isActive;

        private void Awake()
        {
            _rectTransform = text.GetComponent<RectTransform>();
            text.alpha = 0;
        }

        public void Animate()
        {
            _isActive = true;

            StartCoroutine(AnimateText());
            StartCoroutine(RotateBackAndForth());
        }

        private IEnumerator AnimateText()
        {
            text.alpha = 0f;

            yield return StartCoroutine(FadeText(0f, 1f, fadeDuration));
            yield return new WaitForSeconds(displayDuration);
            yield return StartCoroutine(FadeText(1f, 0f, fadeDuration));
            _isActive = false;
        }

        private IEnumerator FadeText(float startAlpha,
            float endAlpha,
            float duration)
        {
            float time = 0f;
            
            while (time < duration)
            {
                float t = time / duration;
                text.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
                time += Time.deltaTime;
                yield return null;
            }
            
            text.alpha = endAlpha;
        }

        private IEnumerator RotateBackAndForth()
        {
            float time = 0f;

            while (_isActive)
            {
                float angle = Mathf.Sin(time * rotationSpeed) * rotationAmount;
                _rectTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
                time += Time.deltaTime;
                yield return null;
            }

            _rectTransform.localRotation = Quaternion.identity;
        }
    }
}
