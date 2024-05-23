using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : Singleton<UserDataManager>
{
    public bool arrowInventario { get; set; }
    private void Start()
    {
        arrowInventario = false;
    }
}
