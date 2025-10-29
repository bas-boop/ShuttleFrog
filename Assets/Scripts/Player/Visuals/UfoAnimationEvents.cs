using UnityEngine;

namespace Player.Visuals
{
    public sealed class UfoAnimationEvents : MonoBehaviour
    {
        [SerializeField] private ParticleSystem launchParticleSystem;
        [SerializeField] private ParticleSystem movementParticle;

        public void LaunchParticle() => launchParticleSystem.Play();

        public void EnableMovementParticles() => movementParticle.Play();

        public void DisableMovementParticles() => movementParticle.Stop();

        public void LaunchPlayer()
        {
            // todo: move player upward with the starting animation
            Debug.Log("Launch player upwards");
        }
    }
}
