using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam2;

    void Start()
    {
        cam1.SetActive(true);
        cam2.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Sight"))
        {
            cam1.SetActive(true);
            cam2.SetActive(false);
        }
        
        if (Input.GetButtonDown("Vision"))
        {
            cam1.SetActive(false);
            cam2.SetActive(true);
        }
    }
}
