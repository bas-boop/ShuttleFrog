using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Canvas.Menus
{
    public sealed class StoryPanels : MonoBehaviour
    {
        [SerializeField] private Image panel;
        [SerializeField] private Sprite[] panels;

        [SerializeField] private UnityEvent onLastPanel = new();

        private int _currentPanelToShow = -1;

        private void Start() => SetNextPanel();

        public void SetNextPanel()
        {
            _currentPanelToShow++;

            if (_currentPanelToShow > panels.Length - 1)
            {
                onLastPanel?.Invoke();
                return;
            }
            
            panel.sprite = panels[_currentPanelToShow];
        }
    }
}