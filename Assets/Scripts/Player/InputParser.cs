using UnityEngine;
using UnityEngine.InputSystem;

using Player.Movement;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(UFOMovement))]
    [RequireComponent(typeof(Grabber))]
    public sealed class InputParser : MonoBehaviour
    {
        [SerializeField] private UFOMovement ufoMovement;
        [SerializeField] private Grabber grabber;
        [SerializeField] private Turner turner;
        
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

            if (turner)
            {
                float turnInput = _inputActionAsset["Turn"].ReadValue<float>();
                turner.Turn(turnInput);
            }
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
            _inputActionAsset["Drop"].performed += DropAction;
        }

        private void RemoveListeners()
        {
            _inputActionAsset["SpeedBoost"].performed -= SpeedBoostAction;
            _inputActionAsset["Grapple"].performed -= GrappleAction;
            _inputActionAsset["Drop"].performed -= DropAction;
        }
        
        #region Context

        private void SpeedBoostAction(InputAction.CallbackContext context) => ufoMovement.SpeedBoost();

        private void GrappleAction(InputAction.CallbackContext context) => grabber.DoGrab();

        private void DropAction(InputAction.CallbackContext context) => grabber.DropObject();

        #endregion
    }
}