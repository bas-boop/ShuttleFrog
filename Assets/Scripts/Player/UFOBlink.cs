using UnityEngine;

public class UFOBlink : MonoBehaviour
{
    public Transform[] eyes;
    public float blinkDuration = 0.1f;
    public float minBlinkInterval = 2f;
    public float maxBlinkInterval = 5f;
    private Vector3 originalScale;
    private bool isBlinking = false;

    void Start()
    {
        originalScale = eyes[0].localScale;
        StartCoroutine(BlinkRoutine());
    }

    System.Collections.IEnumerator BlinkRoutine()
    {
        while (true)
        {
            float waitTime = Random.Range(minBlinkInterval, maxBlinkInterval);
            yield return new WaitForSeconds(waitTime);

            yield return StartCoroutine(BlinkOnce());
        }
    }

    System.Collections.IEnumerator BlinkOnce()
    {
        if (isBlinking) yield break;
        isBlinking = true;

        float t = 0f;
        while (t < blinkDuration)
        {
            t += Time.deltaTime;
            float progress = t / blinkDuration;
            float scaleY = Mathf.Lerp(originalScale.y, 0f, progress);
            eyes[0].localScale = new Vector3(originalScale.x, scaleY, originalScale.z);
            eyes[1].localScale = new Vector3(originalScale.x, scaleY, originalScale.z);
            yield return null;
        }

        yield return new WaitForSeconds(0.05f);

        t = 0f;
        while (t < blinkDuration)
        {
            t += Time.deltaTime;
            float progress = t / blinkDuration;
            float scaleY = Mathf.Lerp(0f, originalScale.y, progress);
            eyes[0].localScale = new Vector3(originalScale.x, scaleY, originalScale.z);
            eyes[1].localScale = new Vector3(originalScale.x, scaleY, originalScale.z);
            yield return null;
        }

        eyes[0].localScale = originalScale;
        eyes[1].localScale = originalScale;
        isBlinking = false;
    }
}