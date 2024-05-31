using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piedra : MonoBehaviour
{
    private GameManager gm;
    private Selected selected;
    private bool activada;
    private ParticleSystem vfx_Piedras;
    private void Awake()
    {
        activada = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        selected = GameObject.FindWithTag("MainCamera").GetComponent<Selected>();
        //selected.ActivarPiedra += InteractuaPiedra;
        vfx_Piedras = GetComponent<ParticleSystem>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InteractuaPiedra()
    {
        /*switch (gameObject.tag)
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
        }*/
        //ActivarDesactivar();
        if (!activada)
        {
            gm.SumarPiedra();
            activada = true;
            vfx_Piedras.Play();
        }
        else
        {
            gm.RestarPiedra();
            activada = false;
            vfx_Piedras.Stop();
        }

    }

    void ActivarDesactivar()
    {
        if (!activada)
        {
            gm.SumarPiedra();
            activada = true;
            vfx_Piedras.Play();
        }
        else
        {
            gm.RestarPiedra();
            activada = false;
            vfx_Piedras.Stop();
        }
    }

    private void OnDestroy()
    {
        //selected.ActivarPiedra -= InteractuaPiedra;
    }
}
