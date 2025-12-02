using UnityEngine;

namespace Player.Visuals
{
    public sealed class UFOSprintEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private ParticleSystem _sprintNoise;
        [SerializeField] private float targetFOV = 80f;
        [SerializeField] private float fovLerpTime = 5f;
        
        private Camera _playerCamera;
        private float _defaultFOV;
        private bool _wasSprinting;
        private bool _isSprinting;

        private void Start()
        {
            _playerCamera = Camera.main;
            _defaultFOV = Camera.main.fieldOfView;
            _sprintNoise = GetComponentInChildren<ParticleSystem>();
            _particleSystem = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (_isSprinting)
            {
                _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovLerpTime);
            }
            else if (Mathf.Abs(_playerCamera.fieldOfView-_defaultFOV) > 1f)
            {
                _playerCamera.fieldOfView = Mathf.Lerp(_playerCamera.fieldOfView, _defaultFOV, Time.deltaTime * fovLerpTime);
            }
            
            if (_isSprinting
                && !_wasSprinting)
            {
                _particleSystem.Play();
                _wasSprinting = true;
            }
            else if (!_isSprinting
                     && _wasSprinting)
            {
                _particleSystem.Stop();
                _wasSprinting = false;
            }
        }

        public void SetParticle(bool target) => _isSprinting = target;
    }
}

