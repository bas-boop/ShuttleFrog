using UnityEngine;

using Framework.GrapplingSystem;

namespace Framework.DemandSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class TypeIcon : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private PlushieType type;
        [SerializeField] private Sprite[] icons;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            UpdateIcon();
        }

        public void SetType(PlushieType type)
        {
            this.type = type;
            UpdateIcon();
        }

        private void UpdateIcon() => spriteRenderer.sprite = icons[(int) type];
    }
}