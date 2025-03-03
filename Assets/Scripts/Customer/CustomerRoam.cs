using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Customer
{
    public class CustomerRoam : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        
        // Update is called once per frame
        void Update()
        {
            if (agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                
            }
        }

        public void SetTarget(Transform target)
        {
            agent.SetDestination(target.position);
        }

        public IEnumerator GetToDestination(Action onComplete)
        {
            yield return new WaitUntil(() => agent.pathStatus == NavMeshPathStatus.PathComplete);
            onComplete.Invoke();
        }
    }
}
