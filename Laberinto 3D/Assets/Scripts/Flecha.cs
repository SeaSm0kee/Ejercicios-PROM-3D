using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    private EndGame endGame;

    private void Awake()
    {
        endGame = GameObject.Find("EndGame").GetComponent<EndGame>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activar()
    {
        endGame.ActivarHUB();
    }
}
