using UnityEngine;

public class UFOAnimationEvents : MonoBehaviour
{
    public ParticleSystem LaunchParticleSystem;
    public ParticleSystem MovementParticle;
    //public Rigidbody PlayerRb;

    public void LaunchParticle()
    {
        LaunchParticleSystem.Play();
    }
    public void EnableMovementParticles()
    {
            MovementParticle.Play();
    
    }
    public void DisableMovementParticles()
    {
            MovementParticle.Stop();
    }
    public void LaunchPlayer()
    {
        //PlayerRb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        Debug.Log("Launch player upwards");
    }
}
