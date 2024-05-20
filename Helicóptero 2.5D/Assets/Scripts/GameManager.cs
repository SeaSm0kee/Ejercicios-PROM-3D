using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum eStates { Ready, Playing, GameOver}
    private eStates state;

    public event Action<GameObject> SkyscraperDestroyed;
    public event Action StartSpawnAirplane;
    public event Action<bool> StartPlay;
    public event Action<int> AumentaDificultad;
    public event Action HelicopterDeath;
    public event Action HelicopterWin;

    private int coins;

    [SerializeField] TextMeshProUGUI textCoins;
    [SerializeField] private GameObject uiStartGame;
    [SerializeField] private GameObject uiCoins;
    
    private bool isPlaying;
    private bool cambioAplicado;
    
    private bool win;
    private int nivelDificultadActual;

    private void Awake()
    {
        state = eStates.Ready;
        cambioAplicado = false;
        isPlaying = false;
        nivelDificultadActual = 0;
    }

    void Start()
    {
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
        }

        if (coins >= 300 && nivelDificultadActual < 4)
        {
            nivelDificultadActual = 4;
            Win();
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
        if (!win)
        {
            isPlaying = false;
            HelicopterDeath?.Invoke();
            StartPlay?.Invoke(false);
            uiCoins.SetActive(false);
            state = eStates.GameOver;
        }
    }

    private void Win()
    {
        uiCoins.SetActive(false);
        win = true;
        HelicopterWin?.Invoke();
        StartPlay?.Invoke(false);
        state = eStates.GameOver;
        Destroy(GameObject.FindWithTag("Helicopter"), 2f);
    }

    public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
