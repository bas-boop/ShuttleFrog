using System.Collections;
using UnityEngine;
using UnityEngine.Events;

using Framework.Extensions;

namespace Player.Movement
{
    public sealed class MainMenuMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform playerTransform;

        [Header("Movement Settings")]
        [SerializeField] private float startAnimationDuration = 2f;
        [SerializeField] private float moveDistance = 5f;
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float stopWaitTime = 2f;

        [Header("Rotation Settings")]
        [SerializeField] private float rotationCorrectionAngle = 90f;

        [Header("Events")]
        [SerializeField] private UnityEvent onStartMoving = new();
        [SerializeField] private UnityEvent onStopMoving = new();

        private bool _canMove;
        private bool _isMoving;
        private bool _movingForward = true;
        private bool _isStarted;
        private Vector3 _startPos;
        private Vector3 _targetPos;
        private Quaternion _startRotation;

        private void Start()
        {
            _startPos = playerTransform.position;
            _startRotation = playerTransform.rotation;
            _targetPos = _startPos + playerTransform.forward * moveDistance;

            Invoke(nameof(AllowMove), startAnimationDuration);
        }

        private void Update()
        {
            if (!_canMove || !_isMoving)
                return;

            MoveBackAndForth();
        }

        private void MoveBackAndForth()
        {
            if (!_isStarted)
            {
                onStartMoving?.Invoke();
                _isStarted = true;
            }
            
            playerTransform.position = Vector3.MoveTowards(
                playerTransform.position,
                _targetPos,
                moveSpeed * Time.deltaTime
            );

            Vector3 moveDir = (_targetPos - playerTransform.position).normalized;
            
            if (moveDir != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(moveDir);
                targetRot *= Quaternion.Euler(0f, rotationCorrectionAngle, 0f);

                playerTransform.rotation = Quaternion.Slerp(
                    playerTransform.rotation,
                    targetRot,
                    rotationSpeed * Time.deltaTime
                );
            }

            if (_targetPos.IsWithinRange(playerTransform.position, 0.1f))
                StartCoroutine(StopAndReverseRoutine());
        }

        private IEnumerator StopAndReverseRoutine()
        {
            _isMoving = false;
            _isStarted = false;
            onStopMoving?.Invoke();

            Quaternion initialRot = playerTransform.rotation;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * rotationSpeed;
                playerTransform.rotation = Quaternion.Slerp(initialRot, _startRotation, t);
                yield return null;
            }

            yield return new WaitForSeconds(stopWaitTime);

            _movingForward = !_movingForward;
            _targetPos = _movingForward
                ? _startPos + playerTransform.forward * moveDistance
                : _startPos - playerTransform.forward * (moveDistance * 0.01f);

            _isMoving = true;
        }

        private void AllowMove()
        {
            _canMove = true;
            _isMoving = true;
        }
    }
}
