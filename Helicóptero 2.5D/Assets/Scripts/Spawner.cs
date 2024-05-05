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
    private float skyscraperInList;
    private GameManager gm;


    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        skyscrapersList = new List<GameObject>();
        firstSkyscraper = true;
        gm.SkyscraperDestroyed += Destruir;
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
        for (int i = 0; i < 5; i++)
        {
            skyscraper = Instantiate(skyscrapersPrefabs[Random.Range(0, 3)]);
            if (firstSkyscraper)
            {
                skyscraper.transform.position = new Vector3(posicionInicio, RandomHeight(), 35);
                firstSkyscraper = false;
            }
            else
                skyscraper.transform.position = new Vector3(skyscrapersList.Last().transform.position.x + diferencia, RandomHeight(),35);

            skyscrapersList.Add(skyscraper);
        }
    }

    void SpawnSkyscraper()
    {
        skyscraper = Instantiate(skyscrapersPrefabs[Random.Range(0, 3)]);
        skyscraper.transform.position = new Vector3(skyscrapersList.Last().transform.position.x + diferencia, RandomHeight(), 35);
        skyscrapersList.Add(skyscraper);
    }

    void Destruir(GameObject go)
    {
        skyscrapersList.Remove(go);
        SpawnSkyscraper();
        /*for (int i = 0; i < skyscrapersList.Count; i++)
        {
            if (skyscrapersList[i] == go)
                skyscrapersList.Remove(go);
        }*/
    }

    private void OnDisable()
    {
        gm.SkyscraperDestroyed -= Destruir;
    }

    float RandomHeight() => Random.Range(minY_Skyscraper, maxY_Skyscraper);
}
