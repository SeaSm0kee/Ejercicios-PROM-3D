using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private Selected selected;
    [SerializeField] private GameObject canvasArrowInventario;
    private GameObject arrow;
    private UserDataManager userDataManager;
    private GameObject flashLight;
    private GameObject fx_Ring;
    [SerializeField] private GameObject winObject;

    private void Awake()
    {
        selected = GameObject.FindWithTag("MainCamera").GetComponent<Selected>();
        flashLight = GameObject.FindWithTag("FlashLight");
        arrow = GameObject.FindWithTag("Arrow");
        selected.ActivarPentagram += ActivarArrow;
        arrow.SetActive(false);
        userDataManager = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();
        if (userDataManager.flashLightInventario)
            flashLight.SetActive(true);
        else
            flashLight.SetActive(false);

        fx_Ring = GameObject.Find("FX_Ring");
        fx_Ring.SetActive(false);

    }
    void Start()
    {

    }

    void Update()
    {

    }

    void ActivarArrow()
    {
        fx_Ring.SetActive(true);
        canvasArrowInventario.SetActive(false);
        arrow.SetActive(true);
    }

    private void OnDestroy()
    {
        selected.ActivarPentagram -= ActivarArrow;
    }
    public void ActivarHUB()
    {
        winObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

}
