using UnityEngine;

namespace Framework.GrapplingSystem
{
    public sealed class Company : MonoBehaviour
    {
        [SerializeField] private Plushie plushie;
        [SerializeField] private PlushieType type;

        private void Awake()
        {
            if (plushie.Type != type)
                Debug.LogWarning($"Plushie type ({plushie.Type}) and company type ({type}) are not the same!");
        }

        public Plushie GetPlushie()
        {
            Plushie p = Instantiate(plushie);
            p.transform.position += new Vector3(0, 1, 0);
            return p;
        }
    }
}