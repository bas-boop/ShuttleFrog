using UnityEngine;

namespace Player
{
    public class UFOMovement : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 20f;
        [SerializeField] private float accelerateSpeed = 10f;
        [SerializeField] private float boostSpeed = 350f;
        [SerializeField] private float currentCooldown = 5f;

        private Rigidbody _ufoRigidbody;
        private Vector2 _moveInput;
        private bool _canBoost = true;
        private float _speedBoostCooldown = 5f;
        
        private void Start() => _ufoRigidbody = GetComponent<Rigidbody>();
        
        private void Update()
        {
            currentCooldown = Mathf.Max(currentCooldown - Time.deltaTime, 0f);
            _canBoost = currentCooldown <= 0;
        }
        
        public void Move(Vector2 input)
        {
            Vector3 movementDirection = new (input.x, 0, input.y);

            if (movementDirection != Vector3.zero)
            {
                if (_ufoRigidbody.linearVelocity.magnitude > maxSpeed)
                    _ufoRigidbody.linearVelocity = _ufoRigidbody.linearVelocity.normalized * maxSpeed;
                else
                    _ufoRigidbody.AddForce(movementDirection * accelerateSpeed, ForceMode.Acceleration);
            }
            else
                _ufoRigidbody.AddForce(Vector3.zero);
        }

        public void SpeedBoost()
        {
            if (!_canBoost)
                return;

            Vector3 movementDirection = new (_moveInput.x, 0, _moveInput.y);
            _ufoRigidbody.AddForce(movementDirection * boostSpeed, ForceMode.Acceleration);
            currentCooldown = _speedBoostCooldown;
        }
    }
}

