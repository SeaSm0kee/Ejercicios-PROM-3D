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
    [SerializeField] private GameObject vfx;
    
    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        if (gameObject.CompareTag("Coin") || gameObject.CompareTag("Diamond"))
            particle = GetComponent<ParticleSystem>();
        if (gm.GetIsPlaying())
            stopMove = true;
        else
            stopMove = false;
        gm.StartPlay += ChangeStopMove;
        if (gameObject.CompareTag("Airplane"))
        {
            vfx = transform.GetChild(0).gameObject;
            vfx.SetActive(false);
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= endMap)
            DestroyGameObject();
    }

    private void FixedUpdate()
    {
        if(stopMove)
            rb.velocity = new Vector2(speed * -1, rb.velocity.y);
        else
            rb.velocity = new Vector2(speed * 0, rb.velocity.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Coin") || gameObject.CompareTag("Diamond"))
        {
            if (other.gameObject.CompareTag("Helicopter"))
                DestroyCoin();
        }
        else if (gameObject.CompareTag("Airplane"))
            if (other.gameObject.CompareTag("Helicopter"))
                DestroyAirplane();
    }

    void DestroyCoin()
    {
        gm.SumarCoin(gameObject.CompareTag("Coin") ? 1 : 10);
        meshRenderer.enabled = false;
        particle.Play();
        Destroy(gameObject, timeToDestroy);
    }

    void DestroyGameObject()
    {
        if (gameObject.CompareTag("Skyscrapers"))
            gm.DestruirSkyscraper(gameObject);
        else if (gameObject.CompareTag("Airplane"))
            gm.AirplaneDestroyed();
        Destroy(gameObject);
    }

    void DestroyAirplane()
    {
        meshRenderer.enabled = false;
        vfx.SetActive(true);
        gm.AirplaneDestroyed();
        Destroy(gameObject, 3f);
    }
    void ChangeStopMove(bool value) => stopMove = value;
    private void OnDestroy()
    {
        gm.StartPlay -= ChangeStopMove;
    }
}
