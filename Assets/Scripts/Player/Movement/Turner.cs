using Framework.Extensions;
using UnityEngine;

namespace Player.Movement
{
    /// <summary>
    /// For desktop a way to turn the player. For testing without a phone.
    /// </summary>
    public sealed class Turner : MonoBehaviour
    {
        [SerializeField, Range(1, 200)] private float speed = 2;

        public void Turn(float input)
        {
            Vector3 a = transform.eulerAngles;
            a.AddY(input * speed * Time.deltaTime);
            transform.eulerAngles = a;
        }
    }
}