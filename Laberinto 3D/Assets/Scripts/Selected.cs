using System;
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
    public event Action Arrow;
    public event Action FlashLight;
    //public event Action ActivarPiedra;
    public event Action ActivarDoor;
    public event Action ActivarPentagram;
    void Start()
    {

    }


    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, layerMask))
        {
            Deselect();
            SelectedObject(hit.transform);

            /*if (hit.collider.CompareTag("Arrow"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                    Debug.Log("Arrow");
            }
            else if (hit.collider.CompareTag("Roca"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                    Debug.Log("Roca");
            }*/

            /*switch (hit.collider.tag)
            {
                case "Arrow":
                    break;
                case "Roca":
                    break;

            }*/
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact();
            }


            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distancia, Color.red);
        }
        else
        {
            Deselect();
        }
    }
    void Interact()
    {
        switch (hit.collider.tag)
        {
            case "Arrow":
                Arrow?.Invoke();
                Destroy(hit.transform.gameObject);
                break;
            /*case "Roca":
                ActivarPiedra?.Invoke();
                break;*/
            case "FlashLight":
                FlashLight?.Invoke();
                Destroy(hit.transform?.gameObject);
                break;
            /*case "Piedra1":
                hit.collider.gameObject.GetComponent<Piedra>().InteractuaPiedra();
                break;
            case "Piedra2":
                hit.collider.gameObject.GetComponent<Piedra>().InteractuaPiedra();
                break;
            case "Piedra3":
                hit.collider.gameObject.GetComponent<Piedra>().InteractuaPiedra();
                break;
            case "Piedra4":
                hit.collider.gameObject.GetComponent<Piedra>().InteractuaPiedra();
                break;*/
            case "Piedra":
                hit.collider.gameObject.GetComponent<Piedra>().InteractuaPiedra();
                break;
            case "Pentagram":
                ActivarPentagram?.Invoke();
                break;
            /*case "Piedra1":
                ActivarPiedra?.Invoke();
                break;
            case "Piedra2":
                ActivarPiedra?.Invoke();
                break;
            case "Piedra3":
                ActivarPiedra?.Invoke();
                break;
            case "Piedra4":
                ActivarPiedra?.Invoke();
                break;*/
            case "Door":
                ActivarDoor?.Invoke();
                break;
        }
    }

    void SelectedObject(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.yellow;
        ultimoReconocido = transform.gameObject;
    }
    void Deselect()
    {
        if (ultimoReconocido)
        {
            if (!ultimoReconocido.gameObject.CompareTag("Door"))
                ultimoReconocido.GetComponent<MeshRenderer>().material.color = Color.white;
            else
                ultimoReconocido.GetComponent<MeshRenderer>().material.color = Color.black;
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
