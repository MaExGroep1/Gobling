using UnityEngine;
using Util;

namespace Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioSource soundObject;
        [SerializeField] private AudioClip kachingSound;

        
        private static AudioSource staticSoundObject;
        private static Transform staticSoundObjectSpawn;
        
        private static AudioClip staticKachingSound;


        protected override void Awake()
        {
            staticSoundObject = soundObject;
            staticKachingSound = kachingSound;
        }

        public static void PlaySoundClip(AudioClip audioClip, Transform soundObjectSpawn, float volume)
        {
            AudioSource audioSource = Instantiate(staticSoundObject, soundObjectSpawn.position, Quaternion.identity);
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();
            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
        
        public static void PlayKachingSound()
        {
            PlaySoundClip(staticKachingSound, Camera.main?.transform, 0.4f);
        }
    }
}
