using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class UFOMovement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 20f;
        [SerializeField] private float _accelerateSpeed = 10f;
        [SerializeField] private float _currentCooldown = 5f;

        private Vector2 _moveInput;
        private Rigidbody _ufoRigidbody;
        private PlayerControls _controls;
        private bool _canBoost = true;
        private float _SpeedboostCooldown = 5f;
        private float _boostSpeed = 350f;

        private void Awake()
        {
            _controls = new PlayerControls();
            _controls.UFOMovement.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _controls.UFOMovement.Move.canceled += ctx => _moveInput = Vector2.zero;
            _controls.UFOMovement.SpeedBoost.performed += ctx => SpeedBoost();
        }

        private void OnEnable() => _controls.Enable();
        private void OnDisable() => _controls.Disable();

        private void Start()
        {
            _ufoRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 movementDirection = new Vector3(_moveInput.x, 0, _moveInput.y);

            if (movementDirection != Vector3.zero)
            {
                if (_ufoRigidbody.linearVelocity.magnitude > _maxSpeed)
                    _ufoRigidbody.linearVelocity = _ufoRigidbody.linearVelocity.normalized * _maxSpeed;
                else
                    _ufoRigidbody.AddForce(movementDirection * _accelerateSpeed, ForceMode.Acceleration);
            }
            else
                _ufoRigidbody.AddForce(Vector3.zero);
        }

        private void Update()
        {
            _currentCooldown = Mathf.Max(_currentCooldown - Time.deltaTime, 0f);

            _canBoost = _currentCooldown <= 0;
        }

        private void SpeedBoost()
        {
            if (!_canBoost)
            {
                return;
            }

            Vector3 movementDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
            _ufoRigidbody.AddForce(movementDirection * _boostSpeed, ForceMode.Acceleration);

            _currentCooldown = _SpeedboostCooldown;
        }
    }
}

