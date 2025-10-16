using UnityEngine;

namespace Framework.GrapplingSystem
{
    public sealed class Plushie : MonoBehaviour
    {
        [SerializeField] private PlushieType type;
        public PlushieType Type { get => type; private set => type = value; }
    }
}