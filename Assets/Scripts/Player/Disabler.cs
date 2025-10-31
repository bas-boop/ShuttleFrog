using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public sealed class Disabler : MonoBehaviour
    {
        [SerializeField] private UnityEvent onDisablePlayer = new();

        public void DisablePlayer() => onDisablePlayer?.Invoke();
    }
}