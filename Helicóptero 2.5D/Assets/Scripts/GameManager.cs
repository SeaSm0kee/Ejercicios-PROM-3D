using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public event Action<GameObject> SkyscraperDestroyed;
    public event Action StartSpawnAirplane;
    private int coins;
    [SerializeField] TextMeshProUGUI textCoins;
    [SerializeField] private GameObject uiDead;
    public event Action StartPlay;
    [SerializeField] private GameObject uiStartGame;
    [SerializeField] private GameObject uiCoins;
    private bool isPlaying;

    void Start()
    {
        uiDead.SetActive(false);
        uiCoins.SetActive(false);
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

    public void SumarCoin(int n)
    {
        coins += n;
        textCoins.text = coins.ToString();
    }
    public void StarGame()
    {
        StartCoroutine(Coroutine());
        uiStartGame.SetActive(false);
        uiCoins.SetActive(true);
        StartPlay?.Invoke();
        isPlaying = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void HelicopterDead()
    {

    }

    public bool GetIsPlaying() => isPlaying;
}
