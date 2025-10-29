using UnityEngine;
using TMPro;

using Framework;

namespace Gameplay
{
    [RequireComponent(typeof(Timer))]
    public sealed class MoneyManager : Singleton<MoneyManager>
    {
        [SerializeField] private float timeLeft = 300f;
        [SerializeField] private int pluchePrice = 35;
        [SerializeField] private int deliveredPluchies;
        [SerializeField] private int timerExtra;
        [SerializeField] private int _moneyPerSecond = 15;
        [SerializeField] private int _totalPluchieAmount = 2;
        [SerializeField] private Timer timer;

        public int moneyAmount;

        private bool _deliveredAll;

        private void Update()
        {
            timerExtra = Mathf.RoundToInt(timer.GetCurrentTime()) * _moneyPerSecond;

            if (deliveredPluchies == _totalPluchieAmount 
                || timeLeft <= 0)
                timer.StopTimer();
        }
        public void AddMoney()
        {
            moneyAmount += pluchePrice;
            deliveredPluchies++;
        }

        public void AddTimeScore()
        {
            if (!_deliveredAll)
            {
                moneyAmount += timerExtra;
                _deliveredAll = true;
            }
        }
    }
}
