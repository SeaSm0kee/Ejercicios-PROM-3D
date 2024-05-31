using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool arrowInventario;
    //private UserDataManager userDataManager;
    //private List<bool> piedrasActivadas;
    private Selected selected;
    [SerializeField] private GameObject flashlightPlayer;
    [SerializeField] private Light lightPilar;
    private int piedrasActivadas;
    private bool doorActivated;
    private ParticleSystem particleSystemDoor;
    private ParticleSystem fx_Door;
    [SerializeField] private GameObject textoAvisoDoor;
    [SerializeField] private GameObject canvasArrowInventario;
    private void Awake()
    {
        selected = GameObject.FindWithTag("MainCamera").GetComponent<Selected>();
        //userDataManager = GetComponent<UserDataManager>();
        //arrowInventario = userDataManager.arrowInventario;
        selected.Arrow += ArrowGuardada;
        selected.FlashLight += FlashLightUp;
        selected.ActivarDoor += TeleportDoor;
        piedrasActivadas = 0;
        particleSystemDoor = GameObject.FindWithTag("Door").GetComponent<ParticleSystem>();
        fx_Door = GameObject.Find("FX_Door").GetComponent<ParticleSystem>();
        
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
        arrowInventario = true;
        canvasArrowInventario.SetActive(true);
    }
    void FlashLightUp()
    {
        flashlightPlayer.SetActive(true);
        lightPilar.enabled = false;
    }
    void TeleportDoor()
    {
        if (doorActivated && arrowInventario)
            SceneManager.LoadScene(1);
        else
            StartCoroutine(CorActivarPanelAviso());
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

    //Esto avisa al jugador de que debe tener todas las piedras activadas y tener el arrow en el inventario para poder pasar
    IEnumerator CorActivarPanelAviso()
    {
        textoAvisoDoor.SetActive(true);
        yield return new WaitForSeconds(5);
        textoAvisoDoor.SetActive(false);
    }
}
