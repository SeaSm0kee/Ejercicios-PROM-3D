using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskGoToTarget : Node
{
    EnemyBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    public TaskGoToTarget(BTree btree) : base(btree)
    {
        ghostBT = bTree as EnemyBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        //Debug.Log(ghostBT.chompLayerMask);
        Transform target = (Transform)bTree.GetData("target");
        if (target != null) agent.destination = target.position;

        state = NodeState.RUNNING;
        return state;
    }

}
