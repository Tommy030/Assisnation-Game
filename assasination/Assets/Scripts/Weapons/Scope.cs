using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scope : MonoBehaviour
{
    public Camera Cam;
    public GameObject sniperScope;

    public float ScopeValue;
    private float StartScopeValue;

    public float m_ScopeSensitivity;

    private MouseLook scopesens;

    private void Start()
    {
        Cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        sniperScope = GameObject.Find("SS");
        StartScopeValue = Cam.GetComponent<Camera>().fieldOfView;
        scopesens = Cam.GetComponent<MouseLook>();
       
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Cam.GetComponent<Camera>().fieldOfView = ScopeValue;
            sniperScope.GetComponent<RawImage>().enabled = true;
            scopesens.m_isScoping = true;
            scopesens.m_MouseSensitivity = m_ScopeSensitivity;

        }
        else
        {
            Cam.GetComponent<Camera>().fieldOfView = StartScopeValue;
            sniperScope.GetComponent<RawImage>().enabled = false;
            scopesens.m_isScoping = false;

        }
    }
}
