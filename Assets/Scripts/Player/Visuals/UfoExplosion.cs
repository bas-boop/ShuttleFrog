using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

using Environment;

namespace Player.Visuals
{
    public sealed class UfoExplosion : MonoBehaviour
    {
        private GameObject _playerCamera;
        private Animator _explosionAnimator;
        private bool _playSound = true;

        private void Start()
        {
            // using the main camera as reference
            _playerCamera = GameObject.FindWithTag("MainCamera"); 
            transform.LookAt(_playerCamera.transform);
            _explosionAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            // Smoothly rotate to face the camera (creates a 3d look when moving the camera. Can be replaced with transform.Lookat)
            if (_playerCamera != null) 
            {
                Quaternion targetRotation = Quaternion.LookRotation(_playerCamera.transform.position - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
                if (!_playSound)
                    return;

                    SoundManager.Instance.ActivateExplosionSound();
                    _playSound = false;
            }
            // transform.LookAt(_playerCamera.transform);
        }


        // Call this method to stop the explosion animation and destroy the object
        public void StopExplosion()
        {
            _explosionAnimator.SetTrigger("Stop");
            Destroy(gameObject, 2f);
        }
    }
}