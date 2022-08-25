using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShockwaveUnityEvent : MonoBehaviour
{

    // use for cutscenes or boss rumblings etc.

    public UnityEvent Shock;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShockwaveEvent", 3f, 4f);
    }


    private void ShockwaveEvent()
    {
        Shock.Invoke();
    }
}
