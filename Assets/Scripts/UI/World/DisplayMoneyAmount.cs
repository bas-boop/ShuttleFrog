using UnityEngine;
using TMPro;

using Gameplay;

public class DisplayMoneyAmount : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = $"${MoneyManager.Instance.moneyAmount}";
    }
}
