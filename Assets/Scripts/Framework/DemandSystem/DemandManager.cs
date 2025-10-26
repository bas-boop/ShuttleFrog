using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

using Framework.GrapplingSystem;

namespace Framework.DemandSystem
{
    public sealed class DemandManager : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        [SerializeField] private List<DeliveryPoint> deliveryPoints;

        private void Start() => ResetDemanding();

        public void RandomDemandEvent()
        {
            List<DeliveryPoint> availablePoints = deliveryPoints
                .Where(point => !point.IsDemanding)
                .ToList();

            if (availablePoints.Count == 0)
                return;

            int randomIndex = Random.Range(0, availablePoints.Count);
            DeliveryPoint selectedPoint = availablePoints[randomIndex];

            selectedPoint.DemandPlushie();
        }

        public void RemoveDeliveryPoint(DeliveryPoint target)
        {
            if (deliveryPoints.Contains(target))
                deliveryPoints.Remove(target);
        }

        public void SetDeliveryPointsDemanding(PlushieType? targetType)
        {
            foreach (DeliveryPoint deliveryPoint in deliveryPoints)
            {
                if (deliveryPoint.GetPlushieType() == targetType
                    && !deliveryPoint.HasPlushie)
                    deliveryPoint.DemandPlushie();
                else 
                    deliveryPoint.UnDemandPlushie();
            }
        }
        
        public void SetAllDemanding()
        {
            foreach (DeliveryPoint deliveryPoint in deliveryPoints.Where(deliveryPoint => !deliveryPoint.HasPlushie))
                deliveryPoint.DemandPlushie();
        }

        public void ResetDemanding()
        {
            foreach (DeliveryPoint deliveryPoint in deliveryPoints)
            {
                deliveryPoint.HasPlushie = false;
                deliveryPoint.DemandPlushie();
            }
        }
    }
}