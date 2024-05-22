using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Prueba_NavAgent : MonoBehaviour
{
    [SerializeField] private GameObject target;
    NavMeshAgent navMeshAgent;
    Animator animator;
    private float velocity;
    private float speed = 0.5f;
    [SerializeField] private bool perseguir;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        perseguir = false;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && perseguir)
        {
            navMeshAgent.destination = target.transform.position;
            if (velocity < 1.0f)
                velocity += Time.deltaTime * speed;
        }
        else
        {
            if (velocity > 0.0f)
                velocity -= Time.deltaTime * speed;
        }

        if (velocity < 0.0f)
            velocity = 0f;

        if (Input.GetKeyDown(KeyCode.F))
            if (perseguir)
                perseguir = false;
            else
                perseguir = true;

        animator.SetFloat("Velocity", velocity);
    }
}
