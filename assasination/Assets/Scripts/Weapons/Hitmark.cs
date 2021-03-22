using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmark : MonoBehaviour
{
    public GameObject HitmarkImage;
    public bool abc = false;
    void Update()
    {
        if (abc == true)
        {
            HitmarkImage.SetActive(true);
            Invoke("HitReset", 0.1f);
            abc = false;

        }
        
    }

    void HitReset()
    {
        HitmarkImage.SetActive(false);
    }
    
}
