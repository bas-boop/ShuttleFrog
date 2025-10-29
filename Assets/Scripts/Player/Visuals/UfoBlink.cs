using System.Collections;
using UnityEngine;

namespace Player.Visuals
{
    public sealed class UfoBlink : MonoBehaviour
    {
        [SerializeField] private Transform[] eyes;
        [SerializeField] private float blinkDuration = 0.1f;
        [SerializeField] private float minBlinkInterval = 2f;
        [SerializeField] private float maxBlinkInterval = 5f;
        
        private Vector3 _originalScale;
        private bool _isBlinking;

        private void Start()
        {
            _originalScale = eyes[0].localScale;
            StartCoroutine(BlinkRoutine());
        }

        private IEnumerator BlinkRoutine()
        {
            while (true)
            {
                float waitTime = Random.Range(minBlinkInterval, maxBlinkInterval);
                yield return new WaitForSeconds(waitTime);

                yield return StartCoroutine(BlinkOnce());
            }
        }

        private IEnumerator BlinkOnce()
        {
            if (_isBlinking)
                yield break;
            
            _isBlinking = true;

            float time = 0f;
            
            while (time < blinkDuration)
            {
                time += Time.deltaTime;
                float progress = time / blinkDuration;
                float scaleY = Mathf.Lerp(_originalScale.y, 0f, progress);
                eyes[0].localScale = new Vector3(_originalScale.x, scaleY, _originalScale.z);
                eyes[1].localScale = new Vector3(_originalScale.x, scaleY, _originalScale.z);
                yield return null;
            }

            yield return new WaitForSeconds(0.05f);

            time = 0f;
            
            while (time < blinkDuration)
            {
                time += Time.deltaTime;
                float progress = time / blinkDuration;
                float scaleY = Mathf.Lerp(0f, _originalScale.y, progress);
                eyes[0].localScale = new Vector3(_originalScale.x, scaleY, _originalScale.z);
                eyes[1].localScale = new Vector3(_originalScale.x, scaleY, _originalScale.z);
                yield return null;
            }

            eyes[0].localScale = _originalScale;
            eyes[1].localScale = _originalScale;
            _isBlinking = false;
        }
    }
}