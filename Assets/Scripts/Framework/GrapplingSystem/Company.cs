using UnityEngine;

namespace Framework.GrapplingSystem
{
    public sealed class Company : MonoBehaviour
    {
        [SerializeField] private GameObject plushie;

        public GameObject GetPlushie()
        {
            GameObject p = Instantiate(plushie);
            p.transform.position += new Vector3(0, 1, 0);
            return p;
        }
    }
}