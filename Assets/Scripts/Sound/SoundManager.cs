using UnityEngine;
using UnityEngine.Serialization;
using Util;

namespace Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        [Header("Main Settings")]
        [SerializeField] private AudioSource soundObject; // The AudioSource used for sound playback
        [SerializeField] private AudioClip kachingSound; // The "kaching" sound for successful transactions
        
        [Header("Customer Clips")]
        [SerializeField] private AudioClip[] audioClipsBye; // Array of goodbye audio clips
        [SerializeField] private AudioClip[] audioClipsHello; // Array of hello audio clips
        [SerializeField] private AudioClip[] audioClipsGrunt; // Array of grunt audio clips
        
        [Header("Customer Clips")]
        [SerializeField] private AudioClip doorSoundClip; 
        [SerializeField] private AudioClip doorBellClip; 
        
        private static AudioClip[] byeClips; // Static reference for goodbye clips
        private static AudioClip[] helloClips; // Static reference for hello clips
        private static AudioClip[] gruntClips; // Static reference for grunt clips
        private static AudioClip doorClip; // Static reference for grunt clips
        private static AudioClip bellClip; // Static reference for grunt clips
        private static AudioSource staticSoundObject; // Static reference to the AudioSource
        private static Transform staticSoundObjectSpawn; // Static reference to spawn location of the sound object
        private static AudioClip staticKachingSound; // Static reference for the "kaching" sound


        /// <summary>
        /// Initializes static references for sound objects and clips.
        /// </summary>
        protected override void Awake()
        {
            staticSoundObject = soundObject;
            staticKachingSound = kachingSound;

            byeClips = audioClipsBye;
            gruntClips = audioClipsGrunt;
            helloClips = audioClipsHello;

            doorClip = doorSoundClip;
            doorClip = doorBellClip;
        }

        /// <summary>
        /// Plays a specific sound clip at the given spawn point.
        /// </summary>
        /// <param name="audioClip">The audio clip to be played</param>
        /// <param name="soundObjectSpawn">The transform position where the sound should be played</param>
        /// <param name="volume">The volume level for the sound</param>
        public static void PlaySoundClip(AudioClip audioClip, Transform soundObjectSpawn, float volume)
        {
            AudioSource audioSource = Instantiate(staticSoundObject, soundObjectSpawn.position, Quaternion.identity);
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();
            float clipLength = audioSource.clip.length;
            Destroy(audioSource.gameObject, clipLength);
        }
        
        /// <summary>
        /// Plays a random clip from an array of audio clips at the specified location.
        /// </summary>
        /// <param name="audioClips">Array of audio clips to choose from</param>
        /// <param name="soundObjectSpawn">The transform position where the sound should be played</param>
        /// <param name="volume">The volume level for the sound</param>
        public static void PlayRandomClip(AudioClip[] audioClips, Transform soundObjectSpawn, float volume)
        {
            if (audioClips.Length == 0) return;
            
            var randClip = audioClips[Random.Range(0, audioClips.Length)];
            PlaySoundClip(randClip, soundObjectSpawn, volume);
        }
        
        /// <summary>
        /// Plays the "kaching" sound to signify a successful transaction.
        /// </summary>
        public static void PlayKachingSound() =>
            PlaySoundClip(staticKachingSound, Camera.main?.transform, 0.4f);

        /// <summary>
        /// Plays a random hello sound when a customer is at the counter.
        /// </summary>
        public static void IsAtCounter() =>
            PlayRandomClip(helloClips, Camera.main?.transform, 1f);

        /// <summary>
        /// Plays a random goodbye sound when a customer leaves.
        /// </summary>
        public static void CustomerLeave() =>
            PlayRandomClip(byeClips, Camera.main?.transform, 1f);

        /// <summary>
        /// Plays a random grunt sound when a customer grunts.
        /// </summary>
        public static void OnCustomerGrunt() =>
            PlayRandomClip(gruntClips, Camera.main?.transform, 1f);

        public static void OnDoorSound()
        {
            PlaySoundClip(doorClip, Camera.main?.transform, 1f);
            PlaySoundClip(bellClip, Camera.main?.transform, 1f );
        }
    }
}
