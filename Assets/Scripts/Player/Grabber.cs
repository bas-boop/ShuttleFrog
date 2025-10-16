using UnityEngine;

using Framework.GrapplingSystem;

namespace Player
{
    public sealed class Grabber : MonoBehaviour
    {
        private GameObject _companyGameObject;
        private GameObject _deliveryPointGameObject;
        private GameObject _plushie;
        
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
            if (target.CompareTag("Company"))
                _companyGameObject = target;
        }
        
        public void SetDeliveryPoint(GameObject target)
        {
            if (target.CompareTag("DeliveryPoint"))
                _deliveryPointGameObject = target;
        }

        private void ActivateGrab()
        {
            Company c = _companyGameObject.GetComponent<Company>();
            _plushie = c.GetPlushie();
            SetPlushieTransformAndPosition();
            
            _isGrabbing = true;
        }
        
        private void ReleaseObject()
        {
            DeliveryPoint d = _deliveryPointGameObject.GetComponent<DeliveryPoint>();
            d.DoSomething();
            
            Destroy(_plushie);
            
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