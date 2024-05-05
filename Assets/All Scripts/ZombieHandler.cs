using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHandler : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    Transform target;
     
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        float _distance = Vector3.Distance(transform.position, target.position);
        
        if(_distance > 2)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(target.position);
        }
        else
        {
            navMeshAgent.isStopped = true;
        }
    }
}
