using UnityEngine;

using Framework.Animation;
using Environment;

namespace Player.Movement
{
    public sealed class UFOMovement : MonoBehaviour
    {
        private const float NO_VELOCITY_THRESHOLD = 0.2f;
        private const float MAGNITUDE_CAP = 0.01f;
        private const float MOVING_ANIMATION_CAP = 0.5f;
        
        [SerializeField] private AnimationController animationController;

        [Header("Attributes")]
        [SerializeField] private float maxSpeed = 20f;
        [SerializeField, Range(1, 100)] private float accelerateSpeed = 10f;
        [SerializeField, Range(0.1f, 10)] private float decelerateSpeed = 10f;
        [SerializeField] private float boostSpeed = 350f;
        [SerializeField] private float currentCooldown = 5f;

        private Rigidbody _ufoRigidbody;
        private bool _canBoost = true;
        private float _speedBoostCooldown = 5f;

        private void Start() => _ufoRigidbody = GetComponent<Rigidbody>();

        private void Update()
        {
            animationController.PlayAnimation("Moving", _ufoRigidbody.linearVelocity.magnitude > MOVING_ANIMATION_CAP);

            currentCooldown = Mathf.Max(currentCooldown - Time.deltaTime, 0f);
            _canBoost = currentCooldown <= 0;
        }

        public void Move(Vector2 input)
        {
            Vector3 moveDirection = transform.forward * input.y + transform.right * input.x;

            if (moveDirection.sqrMagnitude > MAGNITUDE_CAP)
            {
                moveDirection.Normalize();

                if (_ufoRigidbody.linearVelocity.magnitude > maxSpeed)
                    _ufoRigidbody.linearVelocity = _ufoRigidbody.linearVelocity.normalized * maxSpeed;
                else
                    _ufoRigidbody.AddForce(moveDirection * accelerateSpeed, ForceMode.Acceleration);
                
                return;
            }

            if (_ufoRigidbody.linearVelocity.sqrMagnitude <= MAGNITUDE_CAP)
                return;
            
            Vector3 force = -_ufoRigidbody.linearVelocity.normalized * decelerateSpeed;
            _ufoRigidbody.AddForce(force, ForceMode.Acceleration);

            if (_ufoRigidbody.linearVelocity.magnitude < NO_VELOCITY_THRESHOLD)
                _ufoRigidbody.linearVelocity = Vector3.zero;
        }

        public void SpeedBoost()
        {
            if (!_canBoost)
                return;

            Vector3 movementDirection = _ufoRigidbody.linearVelocity.normalized;
            _ufoRigidbody.AddForce(movementDirection * boostSpeed, ForceMode.Acceleration);
            currentCooldown = _speedBoostCooldown;
            
            if (SoundManager.Exist)
                SoundManager.Instance.ActivateSpeedBoost();
        }
    }
}