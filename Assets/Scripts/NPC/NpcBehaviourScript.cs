using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    private  float _radius = 10.0f;
    private float _stoppingDistance = 0.1f;
    private NavMeshAgent _navMeshAgent;
    private NavMeshPath _navMeshPath;
    
    // Start is called before the first frame update
    void Start()
    {
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._navMeshPath = new NavMeshPath();
        MoveToRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= this._stoppingDistance)
        {
            MoveToRandomPoint();
        }
    }
    
    void MoveToRandomPoint()
    {
        Vector3 randomPoint;
        if (GetRandomPos(transform.position, this._radius, out randomPoint))
        {
            _navMeshAgent.SetDestination(randomPoint);
        }
    }

    bool GetRandomPos(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomPos = center + new Vector3(
                Random.Range(-range, range), 
                0, 
                Random.Range(-range, range)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, range, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

}
