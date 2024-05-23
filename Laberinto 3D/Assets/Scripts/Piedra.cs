using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piedra : MonoBehaviour
{
    private GameManager gm;
    private Selected selected;
    private bool activada;
    private void Awake()
    {
        activada = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        selected = GameObject.FindWithTag("MainCamera").GetComponent<Selected>();
        selected.ActivarPiedra += InteractuaPiedra;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void InteractuaPiedra()
    {
        if (activada)
            gm.RestarPiedra();
        else
            gm.SumarPiedra();
    }
}
