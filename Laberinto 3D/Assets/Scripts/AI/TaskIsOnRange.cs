using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class TaskIsOnRange : Node
{
    EnemyBT enemyBT;
    NavMeshAgent agent;
    public TaskIsOnRange(BTree bTree) : base(bTree)
    {
        enemyBT = bTree as EnemyBT;
        agent = enemyBT.transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, enemyBT.radius, enemyBT.layerMask);

        if (hitColliders.Length > 0)
        {
            enemyBT.SetData("target", hitColliders[0].transform);
            state = NodeState.SUCCESS;
            enemyBT.velocidad = enemyBT.maxSpeedAgent;
            agent.speed = enemyBT.maxSpeedAgent;
        }
        else
        {
            enemyBT.velocidad = enemyBT.minSpeedAgent;
            agent.speed = enemyBT.minSpeedAgent;
            state = NodeState.FAILURE;
        }
        enemyBT.ReloadAnimation();

        return state;
    }

    void Detector()
    {
        Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, enemyBT.radius, enemyBT.layerMask);

        if (hitColliders.Length > 0) Debug.Log(hitColliders[0].gameObject.name);
    }


}
