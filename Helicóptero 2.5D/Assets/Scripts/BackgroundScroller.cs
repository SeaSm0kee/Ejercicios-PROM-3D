using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float speed;
    private Renderer mrend;

    private void Awake()
    {
        mrend = GetComponent<Renderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * speed;
        mrend.material.SetTextureOffset("_BaseMap", new Vector2(offset,0));
    }
}
