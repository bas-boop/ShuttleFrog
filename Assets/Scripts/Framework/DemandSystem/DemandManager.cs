using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

using Framework.Extensions;
using Framework.GrapplingSystem;

namespace Framework.DemandSystem
{
    public sealed class DemandManager : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        [SerializeField] private List<DeliveryPoint> deliveryPoints;
        [SerializeField] private List<Company> compies;
        [SerializeField] private List<GameObject> plushies;

        private void Awake() => Setup();

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

        private void Setup()
        {
            foreach (DeliveryPoint deliveryPoint in deliveryPoints)
            {
                PlushieType type = EnumExtensions.GetRandomEnumValue<PlushieType>();

                while (type == PlushieType.NONE)
                {
                    type = EnumExtensions.GetRandomEnumValue<PlushieType>();
                    
                    if (type != PlushieType.NONE)
                        break;
                }

                switch (type)
                {
                    case PlushieType.RAT:
                        deliveryPoint.SetPlushieToDemand(plushies[0]);
                        break;
                    case PlushieType.FUN:
                        deliveryPoint.SetPlushieToDemand(plushies[1]);
                        break;
                    case PlushieType.CAKE:
                        deliveryPoint.SetPlushieToDemand(plushies[2]);
                        break;
                    case PlushieType.CLOWN:
                        deliveryPoint.SetPlushieToDemand(plushies[3]);
                        break;
                    case PlushieType.CAR:
                        deliveryPoint.SetPlushieToDemand(plushies[4]);
                        break;
                    case PlushieType.NONE:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}