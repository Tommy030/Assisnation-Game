using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    private AudioSource ShatterSound;
    private MeshRenderer MeshRend;
    private MeshCollider Collider;
    void Start()
    {
        ShatterSound = gameObject.GetComponent<AudioSource>();
        MeshRend = gameObject.GetComponent<MeshRenderer>();
        Collider = gameObject.GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BreakWindow()
    {
        ShatterSound.Play();
        MeshRend.enabled = false;
        Collider.enabled = false;
    }
}
