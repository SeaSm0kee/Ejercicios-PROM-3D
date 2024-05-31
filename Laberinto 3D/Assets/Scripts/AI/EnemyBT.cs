using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class EnemyBT : BTree
{
    public List<Transform> points;
    public LayerMask layerMask;
    public float radius;
    public Animator animator;
    public float velocidad = 0.0f;
    public float maxSpeedAgent = 6f;
    public float minSpeedAgent = 1f;

    protected override Node SetupTree()
    {
        Node root = new Selector(this, new List<Node>(){
            new Sequence(this,new List<Node>(){
                new TaskIsOnRange(this),
                new TaskGoToTarget(this)
            }),
            new TaskPatrol(this)
        });
        velocidad = 2f;
        ReloadAnimation();
        return root;
    }
    public void ReloadAnimation()
    {
        animator.SetFloat("MotionSpeed", velocidad);
    }
}
