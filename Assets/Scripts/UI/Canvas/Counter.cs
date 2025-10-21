using TMPro;
using UnityEngine;

namespace UI.Canvas
{
    public sealed class Counter : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private int count;

        private void Start() => text.text = $"Points: {count}";

        public void Count()
        {
            count++;
            text.text = $"Points: {count}";
        }
    }
}