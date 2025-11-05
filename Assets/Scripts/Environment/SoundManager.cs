using Framework;

using UnityEngine;

namespace Environment
{
    public sealed class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioSource sfxSource;
        [SerializeField]
        private AudioClip
            grabPlushie,
            collectCash,
            speedBoost,
            plushieSqueek,
            explosionSound,
            startSpin;

        public void ActivateGrabPlushie() => sfxSource.PlayOneShot(grabPlushie);

        public void ActivateCollectCash() => sfxSource.PlayOneShot(collectCash);

        public void ActivateSpeedBoost() => sfxSource.PlayOneShot(speedBoost);

        public void ActivatePlushieSqueek() => sfxSource.PlayOneShot(plushieSqueek);

        public void ActivateExplosionSound() => sfxSource.PlayOneShot(explosionSound);

        private void Start() => sfxSource.PlayOneShot(startSpin);

    }
}

