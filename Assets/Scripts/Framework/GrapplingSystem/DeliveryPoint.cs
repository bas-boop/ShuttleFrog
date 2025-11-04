using Gameplay;
using UnityEngine;
using UnityEngine.Events;

using Framework.DemandSystem;

namespace Framework.GrapplingSystem
{
    public sealed class DeliveryPoint : MonoBehaviour
    {
        //[SerializeField] private Timer timer;
        [SerializeField] private GameObject notification;
        [SerializeField] private TypeIcon typeIcon;
        //[SerializeField] private Material noMorePlushieMat;
        [SerializeField] private PlushieType type;

        [SerializeField] private UnityEvent onDeliver = new ();

        public bool IsDemanding { get; private set; }
        public bool IsNoMore { get; private set; }
        public bool HasPlushie { get; set; }

        private void Awake()
        {
            notification.SetActive(false);
        }

        public PlushieType GetPlushieType() => type;
        
        public void DoDeliver()
        {
            HasPlushie = true;
            notification.SetActive(false);
            DestroyNotificationPlushie();
            //timer.StopTimer();
            onDeliver?.Invoke();
        }

        public void DestroyNotificationPlushie()
        {
            return;
            
            if (notification.transform.childCount > 0)
                Destroy(notification.transform.GetChild(0).gameObject);
        }

        public void DemandPlushie()
        {
            IsDemanding = true;
            notification.SetActive(true);
            //timer.StartTimer();
        }
        
        public void UnDemandPlushie()
        {
            IsDemanding = false;
            notification.SetActive(false);
            //timer.StartTimer();
        }

        public void NoMorePlushies()
        {
            IsNoMore = true;
            IsDemanding = false;
            Destroy(notification);
            //GetComponent<MeshRenderer>().material = noMorePlushieMat;
        }

        public void SetPlushieToDemand(PlushieType plushieType)
        {
            type = plushieType;
            typeIcon.SetType(type);
        }
    }
}