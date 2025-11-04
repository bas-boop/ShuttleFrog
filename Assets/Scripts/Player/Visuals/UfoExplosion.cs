using UnityEngine;

namespace Player.Visuals
{
    public sealed class UfoExplosion : MonoBehaviour
    {
        private GameObject _playerCamera;
        private Animator _explosionAnimator;

        private void Start()
        {
            _playerCamera = GameObject.FindWithTag("MainCamera"); 
            transform.LookAt(_playerCamera.transform);
            _explosionAnimator = GetComponent<Animator>();
            Invoke(nameof(StopExplosion), 2);
        }

        private void Update()
        {
            if (_playerCamera == null)
                return;
            
            Quaternion targetRotation = Quaternion.LookRotation(_playerCamera.transform.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
        }


        public void StopExplosion()
        {
            _explosionAnimator.SetTrigger("Stop");
            Destroy(gameObject, 2);
        }
    }
}