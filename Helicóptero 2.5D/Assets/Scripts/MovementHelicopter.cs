using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementHelicopter : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody rb;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        if (direction != Vector3.zero) direction.Normalize();
        //direction.Normalize();
    }

    void Move()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Skyscrapers") || other.gameObject.CompareTag("Airplane"))
        {
            Destroy(gameObject);
        }
    }
}
