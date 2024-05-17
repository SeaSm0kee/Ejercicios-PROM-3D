using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    private enum eStates { Ready, Playing, GameOver}
    private eStates state;
    public event Action<GameObject> SkyscraperDestroyed;
    public event Action StartSpawnAirplane;
    private int coins;
    [SerializeField] TextMeshProUGUI textCoins;
    [SerializeField] private GameObject uiDead;
    public event Action<bool> StartPlay;
    [SerializeField] private GameObject uiStartGame;
    [SerializeField] private GameObject uiCoins;
    private bool isPlaying;
    private bool cambioAplicado;
    public event Action<int> AumentaDificultad;
    private bool win;
    [SerializeField] private int nivelDificultadActual;

    private void Awake()
    {
        state = eStates.Ready;
        cambioAplicado = false;
        isPlaying = false;
        nivelDificultadActual = 0;
    }

    void Start()
    {
        uiDead.SetActive(false);
        uiCoins.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case eStates.Ready:
                UpdateReady();
                break;
            case eStates.Playing:
                UpdatePlaying();
                break;
            case eStates.GameOver:
                break;
        }
    }

    void UpdateReady()
    {
        if (isPlaying)
        {
            StartCoroutine(Coroutine());
            uiStartGame.SetActive(false);
            uiCoins.SetActive(true);
            StartPlay?.Invoke(true);
            state = eStates.Playing;
        }

        
    }

    void UpdatePlaying()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            coins += 50;
            textCoins.text = coins.ToString();
            Debug.Log("hi");
        }

        if (coins > 300 && nivelDificultadActual < 4)
        {
            win = true;
            nivelDificultadActual = 4;
            Debug.Log("win");
        }
        else if (coins >= 250 && nivelDificultadActual < 3)
        {
            cambioAplicado = false;
            nivelDificultadActual = 3;
            AumentarDificultad();
        }
        else if (coins >= 150 && nivelDificultadActual < 2)
        {
            cambioAplicado = false;
            nivelDificultadActual = 2;
            AumentarDificultad();
        }
        else if (coins >= 50 && nivelDificultadActual < 1)
        {
            nivelDificultadActual = 1;
            AumentarDificultad();
        }
    }

    void AumentarDificultad()
    {
        if (!cambioAplicado)
        {
            AumentaDificultad?.Invoke(coins);
            cambioAplicado = true;
        }
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

    public void SumarCoin(int n)
    {
        coins += n;
        textCoins.text = coins.ToString();
    }
    

    

    public void HelicopterDead()
    {
        state = eStates.GameOver;
    }

    public bool GetIsPlaying() => isPlaying;

    public void StarGame() => isPlaying = true;

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
