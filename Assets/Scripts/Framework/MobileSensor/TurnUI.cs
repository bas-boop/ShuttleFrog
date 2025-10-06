using TMPro;
using UnityEngine;

namespace Framework.MobileSensor
{
    public sealed class TurnUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private Transform turnObject;
        
        private void Update()
        {
            string rotationText = $"{turnObject.localRotation}";
            text.text = rotationText;
        }
    }
}