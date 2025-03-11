using UnityEngine;
using Util;

namespace Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioSource soundObject;
        [SerializeField] private Transform soundObjectSpawn;

        private static AudioSource staticSoundObject;
        private static Transform staticSoundObjectSpawn;

        protected override void Awake()
        {
            staticSoundObject = soundObject;
            staticSoundObjectSpawn = soundObjectSpawn;
        }

        public static void PlaySoundClip(AudioClip audioClip, float volume)
        {
            AudioSource audioSource = Instantiate(staticSoundObject, staticSoundObjectSpawn.position, Quaternion.identity);
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();
            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
    }
}
