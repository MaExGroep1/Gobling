using UnityEngine;

namespace Difficulty
{
    public class DifficultyManager : MonoBehaviour
    {
        [SerializeField] private GameObject satisfaction, baseValue;

        public void SetDifficultyEasy()
        {
            satisfaction.SetActive(true);
            baseValue.SetActive(true);
        }

        public void SetDifficultyNormal()
        {
            satisfaction.SetActive(false);
            baseValue.SetActive(false);
        }
    }
}
