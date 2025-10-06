using UnityEngine;
using UnityEngine.InputSystem;

namespace Framework.MobileSensor
{
    public sealed class AttitudeReader : MonoBehaviour
    {
        private const int NINETY_DEGREES = 90;
        
        [SerializeField] private Transform horizontalTurner;
        [SerializeField] private Quaternion fullTurner;
        
        private void Start()
        {
            if (AttitudeSensor.current != null)
                InputSystem.EnableDevice(AttitudeSensor.current);
        }
        
        private void Update()
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

            horizontalTurner.rotation = horizontalRotation;
        }
    }
}