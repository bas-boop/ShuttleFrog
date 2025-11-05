using UnityEngine;
using Random = UnityEngine.Random;

namespace Framework.GrapplingSystem
{
    public sealed class Plushie : MonoBehaviour
    {
        [SerializeField] private PlushieType type;
        [SerializeField] private GameObject[] plushieModels;
        public PlushieType Type { get => type; private set => type = value; }

        private void OnEnable()
        {
            foreach (GameObject plushieModel in plushieModels)
                plushieModel.SetActive(false);
            
            int randomIndex = Random.Range(0, plushieModels.Length);
            plushieModels[randomIndex].SetActive(true);
        }
    }
}