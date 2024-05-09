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
    //private GameObject coin;
    [SerializeField] private GameObject diamondPrefab;
    //private GameObject diamond;
    [Range(0, 1)]
    [SerializeField] private float chanceCoin;
    [Range(0,1)]
    [SerializeField] private float chanceDiamond;
    private GameManager gm;

    [SerializeField] private GameObject airplanePrefab;
    private GameObject airplane;
    private bool firstAirplane;
    private readonly float positionZ = 35f;


    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        skyscrapersList = new List<GameObject>();
        firstSkyscraper = true;
        gm.SkyscraperDestroyed += DestroySkyscraper;
        gm.StartSpawnAirplane += SpawnAirplane;
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
                skyscraper.transform.position = new Vector3(skyscrapersList.Last().transform.position.x + diferencia, RandomHeight(), positionZ);

            skyscrapersList.Add(skyscraper);
        }
    }

    void SpawnSkyscraper()
    {
        skyscraper = Instantiate(skyscrapersPrefabs[Random.Range(0, 3)]);
        skyscraper.transform.position = new Vector3(skyscrapersList.Last().transform.position.x + diferencia, RandomHeight(), positionZ);
        skyscrapersList.Add(skyscraper);
        SpawnCoinOrDiamond();
    }

    void DestroySkyscraper(GameObject go)
    {
        skyscrapersList.Remove(go);
        SpawnSkyscraper();
    }

    void SpawnCoinOrDiamond()
    {
        Vector3 vectorSkyscraper = skyscraper.transform.position;
        if (Random.value < chanceDiamond)
        {
            Instantiate(diamondPrefab).transform.position = new Vector3(vectorSkyscraper.x, vectorSkyscraper.y + 19, positionZ);
            //coin = Instantiate(coinPrefab);
            //coin.transform.position = new Vector3(skyscraper.transform.position.x, skyscraper.transform.position.y + 19, skyscraper.transform.position.z);
        }else if(Random.value < chanceCoin)
            Instantiate(coinPrefab).transform.position = new Vector3(vectorSkyscraper.x, vectorSkyscraper.y + 19, positionZ);
        
    }

    void SpawnAirplane()
    {
        if (firstAirplane)
        {
            InstantiateAirplane();
            firstAirplane = false;
        }
        else
            StartCoroutine(CorAirplane());
        
            
    }

    IEnumerator CorAirplane()
    {
        yield return new WaitForSeconds(Random.Range(3, 10));
        InstantiateAirplane();
    }

    void InstantiateAirplane()
    {
        airplane = Instantiate(airplanePrefab);
        airplane.transform.position = new Vector3(posicionInicio, 32f, positionZ);
    }

    float RandomHeight() => Random.Range(minY_Skyscraper, maxY_Skyscraper);

    private void OnDisable()
    {
        gm.SkyscraperDestroyed -= DestroySkyscraper;
        gm.StartSpawnAirplane -= SpawnAirplane;
    }
}
