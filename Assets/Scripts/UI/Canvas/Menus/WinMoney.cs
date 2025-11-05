using TMPro;
using UnityEngine;

using Gameplay;

namespace UI.Canvas.Menus
{
    public sealed class WinMoney : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private void Update()
        {
            string rotationText = $"{VictoryMoney.Instance.Money}";
            text.text = rotationText;
        }
    }
}