using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Prueba_NavAgent : MonoBehaviour
{
    [SerializeField] private GameObject target;
    NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) navMeshAgent.destination = target.transform.position;
    }
}
