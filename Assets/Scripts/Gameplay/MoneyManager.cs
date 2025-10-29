using UnityEngine;
using TMPro;

using Framework;

namespace Gameplay
{
    public sealed class MoneyManager : Singleton<MoneyManager>
    {
        [SerializeField] private float timeLeft = 300f;
        [SerializeField] private int pluchePrice = 35;
        [SerializeField] private int deliveredPluchies;
        [SerializeField] private int timerExtra;

        public Timer _timer;
        public int moneyAmount;
        public static MoneyManager instance;

        private int _moneyPerSecond = 15;
        private int _totalPluchieAmount = 2;
        private bool _deliveredAll = false;

        private void Update()
        {
            timerExtra = Mathf.RoundToInt(_timer._currentTimer) * _moneyPerSecond;

            if (deliveredPluchies == _totalPluchieAmount || timeLeft <= 0)
            {
                _timer.StopTimer();
            }
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
