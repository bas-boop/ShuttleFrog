using UnityEngine;
using TMPro;
using System.Collections;

namespace UI.Canvas.Menus
{
    public class FadeText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float fadeDuration = 1.0f;
        [SerializeField] private float stayVisibleTime = 1.0f;
        [SerializeField] private float delayFading = 5f;

        private void Start()
        {
            SetAlpha(0);
            Invoke(nameof(StartFading), delayFading);
        }

        private void StartFading() => StartCoroutine(FadeLoop());

        private IEnumerator FadeLoop()
        {
            while (true)
            {
                yield return Fade(0f, 1f);
                yield return new WaitForSeconds(stayVisibleTime);
                yield return Fade(1f, 0f);
                yield return new WaitForSeconds(stayVisibleTime);
            }
        }

        private IEnumerator Fade(float startAlpha, float endAlpha)
        {
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
                SetAlpha(alpha);
                yield return null;
            }

            SetAlpha(endAlpha);
        }

        private void SetAlpha(float alpha)
        {
            Color c = text.color;
            c.a = alpha;
            text.color = c;
        }
    }
}