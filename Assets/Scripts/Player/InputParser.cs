using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public sealed class InputParser : MonoBehaviour
    {
        [SerializeField] private UFOMovement ufoMovement;
        [SerializeField] private Grabber grabber;
        
        private PlayerInput _playerInput;
        private InputActionAsset _inputActionAsset;
        
        private void Awake()
        {
            GetReferences();
            Init();
        }

        private void FixedUpdate()
        {
            Vector2 moveInput = _inputActionAsset["Move"].ReadValue<Vector2>();
            ufoMovement.Move(moveInput);
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
            _inputActionAsset["SpeedBoost"].performed += SpeedBoostAction;
            _inputActionAsset["Grapple"].performed += GrappleAction;
        }

        private void RemoveListeners()
        {
            _inputActionAsset["SpeedBoost"].performed -= SpeedBoostAction;
            _inputActionAsset["Grapple"].performed -= GrappleAction;
        }
        
        #region Context

        private void SpeedBoostAction(InputAction.CallbackContext context) => ufoMovement.SpeedBoost();

        private void GrappleAction(InputAction.CallbackContext context) => grabber.DoGrab();

        #endregion
    }
}