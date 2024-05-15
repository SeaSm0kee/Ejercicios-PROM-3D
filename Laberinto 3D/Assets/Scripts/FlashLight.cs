using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashLight : MonoBehaviour
{
    [SerializeField] private Light spotLight;
    [SerializeField] private Texture cookie;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (spotLight.enabled)
                spotLight.enabled = false;
            else
                spotLight.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (spotLight.enabled)
            {
                if (spotLight.cookie == null)
                    spotLight.cookie = cookie;
                else if (spotLight.cookie != null)
                    spotLight.cookie = null;
            }
        }
    }
}
