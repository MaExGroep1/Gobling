using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class SoundService : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private AudioClip[] audioClips; // Array of audio clips that can be played
        [SerializeField] private float volume; // Volume level for audio playback
        
        [Header("Audio Settings")]
        [SerializeField] private Transform soundObjectSpawn; // The position where sound objects spawn
        [SerializeField] private bool isRadio; // Determines if this is a radio sound system
        
        [Header("Radio Settings")]
        
        [ShowIf("isRadio")]
        [SerializeField] private AudioClip radioCallClip; // Audio clip for radio call sound
        
         
        private bool _hasFinishedNews; // Tracks if the news audio has finished playing
        private bool _hasSpawned; // Ensures the radio sound plays only once
        
        /// <summary>
        /// Starts the audio sequence, playing radio news first if applicable.
        /// </summary>
        private void Start()
        {
            if (!isRadio && _hasSpawned) return;
            _hasSpawned = true;

            StartCoroutine(PlayNewsThenRandom());
        }
        
        /// <summary>
        /// Plays a random footstep sound effect.
        /// </summary>
        public void FootStep()
        {
            SoundManager.Instance.PlayRandomClip(audioClips, soundObjectSpawn, volume);
        }
        
        /// <summary>
        /// Plays the radio call clip first, then switches to a random audio clip.
        /// </summary>
        private IEnumerator PlayNewsThenRandom()
        {
            SoundManager.Instance.PlaySoundClip(radioCallClip, soundObjectSpawn, volume);
            yield return new WaitForSeconds(radioCallClip.length);
            SoundManager.Instance.PlayRandomClip(audioClips, soundObjectSpawn, volume);
        }
    }
}