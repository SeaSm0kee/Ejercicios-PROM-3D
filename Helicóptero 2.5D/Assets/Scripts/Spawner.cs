using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> skyscrapersPrefabs;
    private List<GameObject> skyscrapersList;
    private GameObject skyscraper;
    [SerializeField] private float maxY_Skyscraper;
    [SerializeField] private float minY_Skyscraper;

    private bool firstSkyscraper;
    [SerializeField] private float posicionInicio;
    [SerializeField] private float diferencia;
    [SerializeField] private float skyscraperInList;

    [SerializeField] private GameObject coinPrefab;
    private GameObject coin;
    [Range(0, 1)]
    [SerializeField] private float chance;
    private GameManager gm;

    [SerializeField] private GameObject airplanePrefab;
    private GameObject airplane;
    private bool firstAirplane;


    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        skyscrapersList = new List<GameObject>();
        firstSkyscraper = true;
        gm.SkyscraperDestroyed += DestroySkyscraper;
        //gm.StartSpawnAirplane += SpawnAirplane;
        firstAirplane = true;
    }

    void Start()
    {
        SpawnSkyscrapersList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnSkyscrapersList()
    {
        for (int i = 0; i < skyscraperInList; i++)
        {
            skyscraper = Instantiate(skyscrapersPrefabs[Random.Range(0, 3)]);
            if (firstSkyscraper)
            {
                skyscraper.transform.position = new Vector3(posicionInicio, RandomHeight(), 35);
                firstSkyscraper = false;
            }
            else
                skyscraper.transform.position = new Vector3(skyscrapersList.Last().transform.position.x + diferencia, RandomHeight(), 35);

            skyscrapersList.Add(skyscraper);
        }
    }

    void SpawnSkyscraper()
    {
        skyscraper = Instantiate(skyscrapersPrefabs[Random.Range(0, 3)]);
        skyscraper.transform.position = new Vector3(skyscrapersList.Last().transform.position.x + diferencia, RandomHeight(), 35);
        skyscrapersList.Add(skyscraper);
        SpawnCoin();
    }

    void DestroySkyscraper(GameObject go)
    {
        skyscrapersList.Remove(go);
        SpawnSkyscraper();
    }

    void SpawnCoin()
    {
        if (Random.value < chance)
        {
            coin = Instantiate(coinPrefab);
            coin.transform.position = new Vector3(skyscraper.transform.position.x, skyscraper.transform.position.y + 19, skyscraper.transform.position.z);
        }
    }

    void SpawnAirplane()
    {
        //if (firstAirplane)
        StartCoroutine(CorAirplane());
    }

    IEnumerator CorAirplane()
    {
        yield return new WaitForSeconds(Random.Range(0, 6));
        airplane = Instantiate(airplanePrefab);
        airplane.transform.position = new Vector3(posicionInicio, 32.3f, 35);
        firstAirplane = false;
    }

    float RandomHeight() => Random.Range(minY_Skyscraper, maxY_Skyscraper);

    private void OnDisable()
    {
        gm.SkyscraperDestroyed -= DestroySkyscraper;
    }
}
