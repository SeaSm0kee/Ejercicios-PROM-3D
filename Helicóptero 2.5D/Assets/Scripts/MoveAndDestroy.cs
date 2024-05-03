using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndDestroy : MonoBehaviour
{

    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float timeToDestroy;
    private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float endMap;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (gameObject.CompareTag("Coin") || gameObject.CompareTag("Diamond"))
            particle = GetComponent<ParticleSystem>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= endMap)
            Destroy(gameObject);

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Coin") || gameObject.CompareTag("Diamond"))
        {
            if (other.gameObject.CompareTag("Helicopter"))
                StartCoroutine(CorDestroyCoin());
        }

    }


    IEnumerator CorDestroyCoin()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        particle.Play();
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);

    }
}
