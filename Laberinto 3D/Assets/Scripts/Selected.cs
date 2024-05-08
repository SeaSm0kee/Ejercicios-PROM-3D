using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class Selected : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float distancia;
    private RaycastHit hit;
    [SerializeField] private GameObject textDetect;
    private GameObject ultimoReconocido;
    [SerializeField] private Texture2D puntero;
    void Start()
    {

    }


    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, layerMask))
        {
            Deselect();
            SelectedObject(hit.transform);
            if (hit.collider.CompareTag("Arrow"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Arrow");
                }
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }
        else
        {
            Deselect();
        }
    }

    void SelectedObject(Transform transform)
    {
        //transform.GetComponent<MeshRenderer>().material.color = Color.green;
        ultimoReconocido = transform.gameObject;
    }
    void Deselect()
    {
        if (ultimoReconocido)
        {
            //ultimoReconocido.GetComponent<MeshRenderer>().material.color = Color.white;
            ultimoReconocido = null;
        }
    }

    private void OnGUI()
    {
        Rect rect = new Rect(Screen.width / 2, Screen.height / 2, puntero.width / 6, puntero.height / 6);
        GUI.DrawTexture(rect, puntero);

        if (ultimoReconocido)
            textDetect.SetActive(true);
        else
            textDetect.SetActive(false);
    }
}
