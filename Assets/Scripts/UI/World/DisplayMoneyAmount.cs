using Gameplay;
using UnityEngine;
using TMPro;

namespace UI
{
    public sealed class DisplayMoneyAmount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Update()
        {
            scoreText.text = $"${MoneyManager.Instance.GetTotalMoney()}";
        }
    }
}
