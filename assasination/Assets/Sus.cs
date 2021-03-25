using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sus : MonoBehaviour
{
    private AudioSource Clip;
    void Start()
    {
        Clip = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DunDun()
    {
        Clip.Play();
    }
}
