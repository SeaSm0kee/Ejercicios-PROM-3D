using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class TaskIsOnRange : Node
{
    EnemyBT ghostBT;
    NavMeshAgent agent;
    public TaskIsOnRange(BTree bTree) : base(bTree)
    {
        ghostBT = bTree as EnemyBT;
        agent = ghostBT.transform.GetComponent<NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, ghostBT.radius, ghostBT.layerMask);

        if (hitColliders.Length > 0)
        {
            ghostBT.SetData("target", hitColliders[0].transform);
            state = NodeState.SUCCESS;
        }
        else state = NodeState.FAILURE;

        return state;
    }

    void Detector()
    {
        Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, ghostBT.radius, ghostBT.layerMask);

        if (hitColliders.Length > 0) Debug.Log(hitColliders[0].gameObject.name);
    }


}
