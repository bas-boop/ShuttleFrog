using UnityEngine;
using UnityEngine.InputSystem;

namespace Framework.MobileSensor
{
    public sealed class AttitudeReader : MonoBehaviour
    {
        private const int NINETY_DEGREES = 90;
        
        [SerializeField] private Transform horizontalTurner;
        [SerializeField] private Rigidbody rigidbodyToTurn;
        [SerializeField] private Quaternion fullTurner;
        [SerializeField] private bool useRigidbody;
        
        private void Start()
        {
            if (AttitudeSensor.current != null)
                InputSystem.EnableDevice(AttitudeSensor.current);
        }
        
        private void LateUpdate()
        {
            if (horizontalTurner == null
                || AttitudeSensor.current == null)
                return;

            UpdateRotation();
        }

        private void UpdateRotation()
        {
            Quaternion deviceAttitude = AttitudeSensor.current.attitude.ReadValue();
            Quaternion correction = Quaternion.Euler(NINETY_DEGREES, 0, -NINETY_DEGREES);
            Quaternion adjustedAttitude = correction * deviceAttitude;

            fullTurner = adjustedAttitude;
            Vector3 euler = fullTurner.eulerAngles;
            Quaternion horizontalRotation = Quaternion.Euler(0, euler.y, 0);

            if (useRigidbody)
                rigidbodyToTurn.MoveRotation(horizontalRotation);
            else
                horizontalTurner.rotation = horizontalRotation;
        }
    }
}