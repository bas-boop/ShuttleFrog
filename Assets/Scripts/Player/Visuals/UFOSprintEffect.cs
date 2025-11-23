using UnityEngine;

namespace Player.Visuals
{
    public sealed class UFOSprintEffect : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private ParticleSystem _sprintNoise;
        private float _defaultFOV;
        private Camera _playerCamera;
        private bool _wasSprinting;

        [SerializeField] private bool isSprinting; // Needs to be bound to actual sprint boolean 

        [SerializeField] private float targetFOV = 80f;

        private void Start()
        {
            _playerCamera = Camera.main;
            _defaultFOV = Camera.main.fieldOfView;
            _sprintNoise = GetComponentInChildren<ParticleSystem>();
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (isSprinting)
            {
                _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, targetFOV, Time.deltaTime * 5f);
            }
            else if (Mathf.Abs(_playerCamera.fieldOfView-_defaultFOV) > 1f)
            {
                _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, _defaultFOV, Time.deltaTime * 5f);
            }
            if (isSprinting && !_wasSprinting)
            {
                _particleSystem.Play();
                _wasSprinting = true;
            }
            else if (!isSprinting && _wasSprinting)
            {
                _particleSystem.Stop();
                _wasSprinting = false;
            }
        }
    }
}

