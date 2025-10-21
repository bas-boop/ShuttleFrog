using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Framework.GrapplingSystem;

namespace Framework.DemandSystem
{
    public sealed class DemandManager : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        [SerializeField] private List<DeliveryPoint> deliveryPoints;

        public void RandomDemandEvent()
        {
            List<DeliveryPoint> availablePoints = deliveryPoints
                .Where(point => !point.IsDemanding && !point.IsNoMore)
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
    }
}