using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NpcBehaviourScript : MonoBehaviour
{
    private float _radius = 10.0f;
    private float _stoppingDistance = 0.1f;
    private NavMeshAgent _navMeshAgent;
    private NavMeshPath _navMeshPath;
    private float _movMaxTimer = 3.0f;
    private float _destTimer = 0.0f;
    //private float _sleepingTimer = 0.0f;
    private float speed = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        this._navMeshAgent = GetComponent<NavMeshAgent>();
        this._navMeshPath = new NavMeshPath(); 
        this._navMeshAgent.speed = this.speed;
        MoveToRandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        this._destTimer += Time.deltaTime;
        int action = Random.Range(0, 2);
        switch (action)
        {
            case 0:
                if (this._destTimer >= this._movMaxTimer)
                {
                    this._navMeshAgent.velocity = Vector3.zero;
                    this._navMeshAgent.ResetPath();
                    this._destTimer = 0.0f;
                }
                break;
            case 1 :
                if ((!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= this._stoppingDistance) || this._destTimer >= this._movMaxTimer)
                {
                    MoveToRandomPoint();
                    this._destTimer = 0.0f;
                }
                break;
            default:
                break;
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
