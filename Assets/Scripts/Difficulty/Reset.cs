using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Difficulty
{
    public class Reset : MonoBehaviour
    {
        [SerializeField] private float waitTime;
        private void Awake() => StartCoroutine(WaitToLoad());
        
        private IEnumerator WaitToLoad()
        {
            yield return new WaitForSeconds(waitTime);
            SceneManager.LoadScene("Gobling");
        }
    }
}
