using UnityEngine;
using UnityEngine.Events;

using Environment;
using Framework.DemandSystem;
using Framework.GrapplingSystem;
using Gameplay;

namespace Player
{
    public sealed class Grabber : MonoBehaviour
    {
        [SerializeField] private DemandManager demandManager;
        [SerializeField] private Transform itemTransformParent;
        
        [SerializeField] private UnityEvent onGrab = new();
        [SerializeField] private UnityEvent onDeliver = new();
        [SerializeField] private UnityEvent onWrongRelease = new();
        
        private GameObject _companyGameObject;
        private GameObject _deliveryPointGameObject;
        private Plushie _plushie;

        [SerializeField] private Animator _playerAnimator;
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

            if (SoundManager.Exist)
                SoundManager.Instance.ActivateGrabPlushie();
            
            Company company = _companyGameObject.GetComponent<Company>();
            _plushie = company.GetPlushie();
            SetPlushieTransformAndPosition();
            
            demandManager.SetDeliveryPointsDemanding(_plushie.Type);
            _isGrabbing = true;
            _playerAnimator.SetBool("HasItem", true);
            onGrab?.Invoke();
        }
        
        public void DropObject()
        {
            if (_deliveryPointGameObject != null)
                return;

            if (_plushie == null)
                return;

            if (SoundManager.Exist)
                SoundManager.Instance.ActivatePlushieSqueek();

            MoneyManager.Instance.RemoveMoney();
            demandManager.SetAllDemanding();

            Destroy(_plushie.gameObject);

            _plushie = null;
            _isGrabbing = false;
            _playerAnimator.SetBool("HasItem", false);
        }

        private void ReleaseObject()
        {
            if (_deliveryPointGameObject == null)
                return;

            DeliveryPoint deliveryPoint = _deliveryPointGameObject.GetComponent<DeliveryPoint>();

            if (deliveryPoint.GetPlushieType() != _plushie.Type)
            {
                onWrongRelease?.Invoke();
                return;
            }

            MoneyManager.Instance.AddMoney();
            demandManager.SetAllDemanding();
            onDeliver?.Invoke();
            deliveryPoint.DoDeliver();
            
            Destroy(_plushie.gameObject);
            
            _plushie = null;
            _isGrabbing = false;
            _playerAnimator.SetBool("HasItem", false);
        }

        private void SetPlushieTransformAndPosition()
        {
            _plushie.transform.SetParent(itemTransformParent);
            _plushie.transform.position = itemTransformParent.position;
            _plushie.transform.position += new Vector3(0, -1.5f, 0);
        }
    }
}