using TMPro;
using UnityEngine;

using Gameplay;

namespace UI.Canvas.GameplayHud
{
    public sealed class DisplayMoneyAmount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Update()
        {
            scoreText.text = $"{MoneyManager.Instance.GetTotalMoney()}$";
        }
    }
}
