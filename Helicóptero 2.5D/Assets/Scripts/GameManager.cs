using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action<GameObject> SkyscraperDestroyed;
    public delegate void DelegateMoveAndDestroy();
    //public event DelegateMoveAndDestroy d;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestruirSkyscraper(GameObject go)
    {
        SkyscraperDestroyed?.Invoke(go);
    }
}
