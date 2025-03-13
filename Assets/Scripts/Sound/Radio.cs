using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class Radio : MonoBehaviour
    {
        [SerializeField] private AudioClip[] audioClips;        // Array of audio clips that can be played
        [SerializeField] private float volume;                  // Volume level for audio playback
        
        [SerializeField] private AudioClip startUpClip;       // Audio clip for radio call sound

        private void Awake() => StartCoroutine(PlayClip(startUpClip));

        // ReSharper disable once FunctionRecursiveOnAllPaths
        private IEnumerator PlayClip(AudioClip audioClip)
        {
            SoundManager.Instance.PlaySoundClip(audioClip,transform,volume);
            
            yield return new WaitForSeconds(audioClip.length);

            StartCoroutine(PlayClip(GetNextClip(audioClip)));
        }

        private AudioClip GetNextClip(AudioClip previousClip)
        {
            var clips = audioClips.ToList();
            clips.Remove(previousClip);
            var clip = clips[Random.Range(0, clips.Count)];
            return clip;
        }
    }
}
