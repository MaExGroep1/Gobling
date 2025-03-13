using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Difficulty
{
    public class Reset : MonoBehaviour
    {
        [SerializeField] private float waitTime;    // the time between going to the main menu
        
        /// <summary>
        /// Waits to go to the main menu
        /// </summary>
        private void Awake() => StartCoroutine(WaitToLoad());
        
        /// <summary>
        /// Waits to go to the main menu
        /// </summary>
        private IEnumerator WaitToLoad()
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene("Gobling");
        }
    }
}
