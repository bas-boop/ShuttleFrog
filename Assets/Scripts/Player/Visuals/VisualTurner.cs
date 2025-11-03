using UnityEngine;

namespace Player.Visuals
{
    public sealed class VisualTurner : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float rotationSpeed = 10;
        [SerializeField] private float correction = 90;

        private Vector3 _lastDirection = Vector3.forward;

        private void Update()
        {
            Vector3 moveDirection = rigidbody.linearVelocity;

            if (moveDirection.sqrMagnitude > 0.5f)
                _lastDirection = moveDirection.normalized;

            if (_lastDirection.sqrMagnitude <= 0.001f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(_lastDirection);
            targetRotation *= Quaternion.Euler(0, correction, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}