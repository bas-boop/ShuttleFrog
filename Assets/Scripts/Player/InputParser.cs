using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class InputParser : MonoBehaviour
    {
        [SerializeField] private Grabber grabber;
        
        private PlayerInput _playerInput;
        private InputActionAsset _inputActionAsset;
        
        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void Update()
        {
            // use for continues input reading
        }

        private void OnEnable() => AddListeners();

        private void OnDisable() => RemoveListeners();

        private void GetReferences()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Init() => _inputActionAsset = _playerInput.actions;

        private void AddListeners()
        {
            _inputActionAsset["Grapple"].performed += GrappleAction;
        }

        private void RemoveListeners()
        {
            _inputActionAsset["Grapple"].performed -= GrappleAction;
        }
        
        #region Context

        private void GrappleAction(InputAction.CallbackContext context) => grabber.DoGrab();

        #endregion
    }
}