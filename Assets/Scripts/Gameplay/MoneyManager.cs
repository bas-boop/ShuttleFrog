using System;
using UnityEngine;

using Framework;
using UnityEngine.Events;

namespace Gameplay
{
    [RequireComponent(typeof(Timer))]
    public sealed class MoneyManager : Singleton<MoneyManager>
    {
        [SerializeField] private Timer timer;
        
        [SerializeField] private int plushiePrice = 35;
        [SerializeField] private int dropPenalty = 10;
        [SerializeField] private int deliveredPlushies;
        [SerializeField] private int timerExtra;
        [SerializeField] private int moneyPerSecond = 15;
        [SerializeField] private int totalPlushieAmount = 14;
        [SerializeField] private int moneyAmount;

        [SerializeField] private UnityEvent onAllDelivered = new();
        [SerializeField] private UnityEvent onFailed = new();

        private bool _deliveredAll;

        private void Update()
        {
            timerExtra = Mathf.RoundToInt(timer.GetCurrentTime()) * moneyPerSecond;

            if (deliveredPlushies == totalPlushieAmount)
                onAllDelivered?.Invoke();
            
            if (timer.GetCurrentTime() <= 0)
                onFailed?.Invoke();
        }

        public int GetTotalMoney()=> moneyAmount;

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

        public void Reset()
        {
            _deliveredAll = false;
            deliveredPlushies = 0;
        }
    }
}
