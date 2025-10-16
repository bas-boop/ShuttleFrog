using UnityEngine;

namespace Framework.GrapplingSystem
{
    public sealed class DeliveryPoint : MonoBehaviour
    {
        [SerializeField] private PlushieType type;

        public PlushieType GetPlushieType() => type;
        
        public void DoSomething()
        {
            // increase score or something
        }
    }
}