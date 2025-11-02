using UnityEngine;

namespace UI.World
{
    public class YAxisBillboardSprites : MonoBehaviour
    {
        private Camera _mainCamera;
    
        private void Start() => _mainCamera = Camera.main;

        private void Update() => transform.rotation = Quaternion.Euler(0, _mainCamera.transform.eulerAngles.y, 0);
    }
}
