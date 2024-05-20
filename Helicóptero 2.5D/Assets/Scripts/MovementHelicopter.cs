using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementHelicopter : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject vfx;
    [SerializeField] private GameObject blades;
    [SerializeField] private GameObject body;
    private bool canMove;
    private GameManager gm;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        vfx = transform.GetChild(2).gameObject;
        vfx.SetActive(false);
        blades = transform.GetChild(0).gameObject;
        body = transform.GetChild(1).gameObject;
        canMove = false;
        gm.StartPlay += SetCanMove;
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
        if(canMove)
            Move();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        if (direction != Vector3.zero) direction.Normalize();
    }

    void Move()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Skyscrapers") || other.gameObject.CompareTag("Airplane"))
        {
            StartCoroutine(PruebaExplosion());
        }
    }

    IEnumerator PruebaExplosion()
    {
        canMove = false;
        rb.velocity = Vector3.zero;
        blades.SetActive(false);
        body.SetActive(false);
        vfx.SetActive(true);
        gm.HelicopterDead();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    void SetCanMove(bool value) => canMove = value;

    private void OnDestroy()
    {
        gm.StartPlay -= SetCanMove;
    }
}
