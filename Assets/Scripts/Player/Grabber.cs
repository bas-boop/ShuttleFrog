using UnityEngine;
using UnityEngine.Events;

using Framework.GrapplingSystem;

namespace Player
{
    public sealed class Grabber : MonoBehaviour
    {
        [SerializeField] private UnityEvent onSuccesfullRelease = new();
        [SerializeField] private UnityEvent onWrongRelease = new();
        
        private GameObject _companyGameObject;
        private GameObject _deliveryPointGameObject;
        private Plushie _plushie;
        
        private bool _canGrab;
        private bool _isGrabbing;
        
        public void DoGrab()
        {
            if (_canGrab
                && !_isGrabbing)
                ActivateGrab();
            else if (_isGrabbing)
                ReleaseObject();
        }

        public void SetCanGrab(bool target)
        {
            _canGrab = target;

            if (!_canGrab)
                _companyGameObject = null;
        }

        public void SetCompany(GameObject target)
        {
            if (target.CompareTag(nameof(Company)))
                _companyGameObject = target;
        }
        
        public void SetDeliveryPoint(GameObject target)
        {
            if (target.CompareTag(nameof(DeliveryPoint)))
                _deliveryPointGameObject = target;
        }

        private void ActivateGrab()
        {
            Company company = _companyGameObject.GetComponent<Company>();
            _plushie = company.GetPlushie();
            Debug.Log($"{_plushie.Type}");
            SetPlushieTransformAndPosition();
            
            _isGrabbing = true;
        }
        
        private void ReleaseObject()
        {
            DeliveryPoint deliveryPoint = _deliveryPointGameObject.GetComponent<DeliveryPoint>();

            Debug.Log($"{deliveryPoint.GetPlushieType()} - {_plushie.Type}");
            if (deliveryPoint.GetPlushieType() != _plushie.Type)
            {
                onWrongRelease?.Invoke();
                return;
            }
            
            onSuccesfullRelease?.Invoke();
            deliveryPoint.DoSomething();
            
            Destroy(_plushie.gameObject);
            
            _plushie = null;
            _isGrabbing = false;
        }

        private void SetPlushieTransformAndPosition()
        {
            _plushie.transform.SetParent(transform);
            _plushie.transform.position = transform.position;
            _plushie.transform.position += new Vector3(0, -1.5f, 0);
        }
    }
}