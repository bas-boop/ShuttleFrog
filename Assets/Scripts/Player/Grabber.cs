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
        
        public void EmptyCompany() => _companyGameObject = null;
        
        public void SetDeliveryPoint(GameObject target)
        {
            if (target == null)
                _deliveryPointGameObject = null;
            
            if (target.CompareTag(nameof(DeliveryPoint)))
                _deliveryPointGameObject = target;
        }

        public void EmptyDeliveryPoint() => _deliveryPointGameObject = null;

        private void ActivateGrab()
        {
            if (_companyGameObject == null)
                return;
            
            Company company = _companyGameObject.GetComponent<Company>();
            _plushie = company.GetPlushie();
            SetPlushieTransformAndPosition();
            
            _isGrabbing = true;
        }
        
        private void ReleaseObject()
        {
            if (_deliveryPointGameObject == null)
                return;
            DeliveryPoint d = _deliveryPointGameObject.GetComponent<DeliveryPoint>();
            
            DeliveryPoint deliveryPoint = _deliveryPointGameObject.GetComponent<DeliveryPoint>();

            if (deliveryPoint.GetPlushieType() != _plushie.Type)
            {
                onWrongRelease?.Invoke();
                return;
            }
            
            onSuccesfullRelease?.Invoke();
            deliveryPoint.DoDeliver();
            
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