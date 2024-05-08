using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action<GameObject> SkyscraperDestroyed;
    public event Action StartSpawnAirplane;
    //public delegate void DelegateMoveAndDestroy();
    //public event DelegateMoveAndDestroy d;

    void Start()
    {
        StartCoroutine(Coroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DestruirSkyscraper(GameObject go)
    {
        SkyscraperDestroyed?.Invoke(go);
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(15);
        AirplaneDestroyed();
    }

    public void AirplaneDestroyed()
    {
        StartSpawnAirplane?.Invoke();
    }
}
