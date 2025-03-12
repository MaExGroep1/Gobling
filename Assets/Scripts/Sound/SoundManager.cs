using UnityEngine;
using Util;

namespace Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioSource soundObject;
        [SerializeField] private AudioClip kachingSound;
        
        [Header("Customer Clips")]
        [SerializeField] private AudioClip[] audioClipsBye;
        [SerializeField] private AudioClip[] audioClipsHello;
        [SerializeField] private AudioClip[] audioClipsGrunt;
        
        private static AudioClip[] byeClips;
        private static AudioClip[] helloClips;
        private static AudioClip[] gruntClips;

        
        private static AudioSource staticSoundObject;
        private static Transform staticSoundObjectSpawn;
        
        private static AudioClip staticKachingSound;


        protected override void Awake()
        {
            staticSoundObject = soundObject;
            staticKachingSound = kachingSound;

            byeClips = audioClipsBye;
            gruntClips = audioClipsGrunt;
            helloClips = audioClipsHello;
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
        
        public static void PlayRandomClip(AudioClip[] audioClips, Transform soundObjectSpawn, float volume)
        {
            if (audioClips.Length == 0) return;
            
            var randClip = audioClips[Random.Range(0, audioClips.Length)];
            PlaySoundClip(randClip, soundObjectSpawn, volume);
        }
        
        public static void PlayKachingSound()
        {
            PlaySoundClip(staticKachingSound, Camera.main?.transform, 0.4f);
        }

        public static void IsAtCounter()
        {
            SoundManager.PlayRandomClip(helloClips, Camera.main?.transform, 1f);
        }

        public static void CustomerLeave()
        {
            SoundManager.PlayRandomClip(byeClips, Camera.main?.transform, 1f);
        }

        public static void OnCustomerGrunt()
        {
            SoundManager.PlayRandomClip(gruntClips, Camera.main?.transform, 1f);
        }
    }
}
