using TMPro;
using UnityEngine;

namespace UI.Canvas
{
    /// <summary>
    /// For debug purposes
    /// </summary>
    public sealed class TransformText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Transform theTransform;

        private void Update()
        {
            text.text = $"{theTransform.forward}";
        }
    }
}