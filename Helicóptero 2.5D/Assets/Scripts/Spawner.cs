using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> skyscrapersPrefabs;
    private List<GameObject> skyscrapers;
    private GameObject skyscraper;
    [SerializeField] private float maxY_Skyscraper;
    [SerializeField] private float minY_Skyscraper;

    [SerializeField] private bool firstSkyscraper;
    [SerializeField] private float posicionInicio;
    [SerializeField] private float diferencia;
    private float skyscraperInList;


    private void Awake()
    {
        skyscrapers = new List<GameObject>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn()
    {
        skyscraper = Instantiate(skyscrapersPrefabs[Random.Range(0, 3)]);
    }
}
