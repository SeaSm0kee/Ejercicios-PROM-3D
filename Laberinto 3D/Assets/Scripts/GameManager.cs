using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool arrowInventario;
    private Selected selected;
    [SerializeField] private GameObject flashlightPlayer;
    [SerializeField] private Light lightPilar;
    private int piedrasActivadas;
    private bool doorActivated;
    private ParticleSystem particleSystemDoor;
    private ParticleSystem fx_Door;
    [SerializeField] private GameObject textoAvisoDoor;
    [SerializeField] private GameObject canvasArrowInventario;
    [SerializeField] private GameObject primeraPista;
    [SerializeField] private GameObject segundaPista;
    private UserDataManager userDataManager;
    private void Awake()
    {
        selected = GameObject.FindWithTag("MainCamera").GetComponent<Selected>();
        userDataManager = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();
        selected.Arrow += ArrowGuardada;
        selected.FlashLight += FlashLightUp;
        selected.ActivarDoor += TeleportDoor;
        piedrasActivadas = 0;
        particleSystemDoor = GameObject.FindWithTag("Door").GetComponent<ParticleSystem>();
        fx_Door = GameObject.Find("FX_Door").GetComponent<ParticleSystem>();

    }
    void Start()
    {
        StartCoroutine(CorActivarPanelAviso(primeraPista, 4.5f));
    }

    void Update()
    {
        if (piedrasActivadas == 4)
        {
            if (!doorActivated)
            {
                doorActivated = true;
                particleSystemDoor.Play();
                fx_Door.Play();
            }

        }
        else
        {
            if (doorActivated)
            {
                doorActivated = false;
                particleSystemDoor.Stop();
                fx_Door.Stop();
            }
        }
    }

    void ArrowGuardada()
    {
        userDataManager.arrowInventario = true;
        arrowInventario = true;
        canvasArrowInventario.SetActive(true);
        StartCoroutine(CorActivarPanelAviso(segundaPista, 10));
    }
    void FlashLightUp()
    {
        userDataManager.flashLightInventario = true;
        flashlightPlayer.SetActive(true);
        lightPilar.enabled = false;
    }
    void TeleportDoor()
    {
        if (doorActivated && arrowInventario)
            SceneManager.LoadScene(1);
        else
            StartCoroutine(CorActivarPanelAviso(textoAvisoDoor, 5f));
    }

    public void SumarPiedra()
    {
        piedrasActivadas++;
    }

    public void RestarPiedra()
    {
        piedrasActivadas--;
    }
    private void OnDestroy()
    {
        selected.Arrow -= ArrowGuardada;
        selected.FlashLight -= FlashLightUp;
        selected.ActivarDoor -= TeleportDoor;
    }
    IEnumerator CorActivarPanelAviso(GameObject ob, float time)
    {
        ob.SetActive(true);
        yield return new WaitForSeconds(time);
        ob.SetActive(false);
    }
}
