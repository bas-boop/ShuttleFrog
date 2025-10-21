using UnityEngine;

namespace Player.Movement
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
            Debug.Log(transform.forward);
            Vector3 moveDirection = transform.forward * input.y + transform.right * input.x;

            if (moveDirection.sqrMagnitude <= 0.01f)
                return;
            
            moveDirection.Normalize();

            if (_ufoRigidbody.linearVelocity.magnitude > maxSpeed)
                _ufoRigidbody.linearVelocity = _ufoRigidbody.linearVelocity.normalized * maxSpeed;
            else
                _ufoRigidbody.AddForce(moveDirection * accelerateSpeed, ForceMode.Acceleration);
        }


        public void SpeedBoost()
        {
            if (!_canBoost)
                return;

            Vector3 movementDirection = new (_ufoRigidbody.linearVelocity.x, 0, _ufoRigidbody.linearVelocity.y);
            _ufoRigidbody.AddForce(movementDirection * boostSpeed, ForceMode.Acceleration);
            currentCooldown = _speedBoostCooldown;
        }
    }
}

