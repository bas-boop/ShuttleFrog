using UnityEngine;
using TMPro;

using Framework;

namespace UI.Canvas
{
    public sealed class TimerUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Timer timer;
        [SerializeField] private string textPrefix;

        private bool _isShowing;

        private void Update()
        {
            if (!timer.IsCounting)
                return;

            if (timer.GetCurrentTime() <= 9.99f)
                //|| timer.GetCurrentTime() <= 99.99f)
            {
                text.text = textPrefix + 0 + timer.GetCurrentTime().ToString("F2");
                return;
            }

            text.text = textPrefix + timer.GetCurrentTime().ToString("F2");
        }
    }
}