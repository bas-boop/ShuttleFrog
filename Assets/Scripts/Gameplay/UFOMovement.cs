using UnityEngine;
using UnityEngine.InputSystem;

public class UFOMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Rigidbody _ufoRigidbody;
    private PlayerControls _controls;
    private bool _canBoost = true;
    private float speedboostCooldown = 5;

    [SerializeField] private float _maxSpeed = 20f;
    [SerializeField] private float _accelerateSpeed = 10f;

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.UFOMovement.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.UFOMovement.Move.canceled += ctx => _moveInput = Vector2.zero;
        _controls.UFOMovement.SpeedBoost.performed += ctx => BoostSpeed();
    }

    void OnEnable() => _controls.Enable();
    void OnDisable() => _controls.Disable();

    void Start()
    {
        _ufoRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
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
        {
            _ufoRigidbody.AddForce(Vector3.zero);
        }
    }

    private void Update()
    {
        speedboostCooldown = Mathf.Max(speedboostCooldown - Time.deltaTime, 0f);

        if (speedboostCooldown <= 0)
            _canBoost = true;
        else
            _canBoost = false;
    }

    void BoostSpeed()
    {
        if (_canBoost)
        {
            Vector3 movementDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
            _ufoRigidbody.AddForce(movementDirection * 350, ForceMode.Acceleration);

            speedboostCooldown = 5f;
        }
    }
}
