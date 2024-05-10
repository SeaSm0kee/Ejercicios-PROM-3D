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
    private GameManager gm;
    private MeshRenderer meshRenderer;
    private bool stopMove;
    private GameObject vfx;
    

    //public delegate void DelegateMoveAndDestroy();
    //public event DelegateMoveAndDestroy 

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        if (gameObject.CompareTag("Coin") || gameObject.CompareTag("Diamond"))
            particle = GetComponent<ParticleSystem>();
        stopMove = false;
        gm.StartPlay += ChangeStopMove;
        //falta poner un if aqui para buscar el objeto

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= endMap)
            DestruirObjeto();

    }

    private void FixedUpdate()
    {
        if(stopMove)
            rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Coin") || gameObject.CompareTag("Diamond"))
        {
            if (other.gameObject.CompareTag("Helicopter"))
                StartCoroutine(CorDestroyCoin());
        }
        else if (gameObject.CompareTag("Airplane"))
            if (other.gameObject.CompareTag("Helicopter"))
                Destroy(gameObject);
    }


    IEnumerator CorDestroyCoin()
    {
        gm.SumarCoin(gameObject.CompareTag("Coin") ? 1 : 10);
        meshRenderer.enabled = false;
        particle.Play();
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }

    void DestruirObjeto()
    {
        if (gameObject.CompareTag("Skyscrapers"))
            gm.DestruirSkyscraper(gameObject);
        else if (gameObject.CompareTag("Airplane"))
            gm.AirplaneDestroyed();
        Destroy(gameObject);
    }
    void ChangeStopMove() => stopMove = true;

    private void OnDestroy()
    {
        gm.StartPlay -= ChangeStopMove;
    }
}
