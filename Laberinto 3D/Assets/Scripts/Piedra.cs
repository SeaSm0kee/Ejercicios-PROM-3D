using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piedra : MonoBehaviour
{
    private GameManager gm;
    private Selected selected;
    private bool activada;
    private ParticleSystem particleSystem;
    private void Awake()
    {
        activada = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        selected = GameObject.FindWithTag("MainCamera").GetComponent<Selected>();
        selected.ActivarPiedra += InteractuaPiedra;
        particleSystem = GetComponent<ParticleSystem>();
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
        switch (gameObject.tag)
        {
            case "Piedra1":
                ActivarDesactivar();
                break;
            case "Piedra2":
                ActivarDesactivar();
                break;
            case "Piedra3":
                ActivarDesactivar();
                break;
            case "Piedra4":
                ActivarDesactivar();
                break;
        }
        
            
    }

    void ActivarDesactivar()
    {
        if (!activada)
        {
            gm.SumarPiedra();
            activada = true;
            particleSystem.Play();
        }
        else
        {
            gm.RestarPiedra();
            activada = false;
            particleSystem.Stop();
        }
    }

    private void OnDestroy()
    {
        selected.ActivarPiedra -= InteractuaPiedra;
    }
}
