using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool arrowInventario;
    //private UserDataManager userDataManager;
    //private List<bool> piedrasActivadas;
    private Selected selected;
    [SerializeField] private GameObject flashlightPlayer;
    [SerializeField] private Light lightPilar;
    private int piedrasActivadas;
    private void Awake()
    {
        selected = GameObject.FindWithTag("MainCamera").GetComponent<Selected>();
        //userDataManager = GetComponent<UserDataManager>();
        //arrowInventario = userDataManager.arrowInventario;
        selected.Arrow += ArrowGuardada;
        selected.FlashLight += FlashLightUp;
        piedrasActivadas = 0;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ArrowGuardada()
    {
        arrowInventario = true;
    }
    void FlashLightUp()
    {
        flashlightPlayer.SetActive(true);
        lightPilar.enabled = false;
    }

    public void SumarPiedra()
    {
        piedrasActivadas++;
    }

    public void RestarPiedra()
    {
        piedrasActivadas--;
    }
}
