using UnityEngine;
using TMPro;

using Framework;

namespace Gameplay
{
    [RequireComponent(typeof(Timer))]
    public sealed class MoneyManager : Singleton<MoneyManager>
    {
        [SerializeField] private float timeLeft = 300f;
        [SerializeField] private int plushiePrice = 35;
        [SerializeField] private int dropPenalty = 10;
        [SerializeField] private int deliveredPlushies;
        [SerializeField] private int timerExtra;
        [SerializeField] private int moneyPerSecond = 15;
        [SerializeField] private int totalPlushieAmount = 14;
        [SerializeField] private Timer timer;
        [SerializeField] private int moneyAmount;

        private bool _deliveredAll;

        private void Update()
        {
            timerExtra = Mathf.RoundToInt(timer.GetCurrentTime()) * moneyPerSecond;

            if (deliveredPlushies == totalPlushieAmount 
                || timeLeft <= 0)
                timer.StopTimer();
        }

        public int totalMoney()=> moneyAmount;

        public void AddMoney()
        {
            moneyAmount += plushiePrice;
            deliveredPlushies++;
        }

        public void RemoveMoney()
        {
            moneyAmount -= dropPenalty;
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
