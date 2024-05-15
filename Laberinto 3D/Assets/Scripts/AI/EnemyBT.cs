using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class EnemyBT : BTree
{
    public List<Transform> points;
    public LayerMask layerMask;
    public float radius;

    protected override Node SetupTree()
    {
        Node root = new Selector(this, new List<Node>(){
            new Sequence(this,new List<Node>(){
                new TaskIsOnRange(this),
                new TaskGoToTarget(this)
            }),
            new TaskPatrol(this)
        });
        return root;
    }
}
