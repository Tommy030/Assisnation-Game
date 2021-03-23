using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitmark : MonoBehaviour
{
    [SerializeField] private GameObject HitmarkImage;
    public bool m_Hit = false;
    private void Start()
    {
        HitmarkImage = GameObject.Find("Hit");
        Invoke("HitReset", 0.1f);
    }
    void Update()
    {

        if (m_Hit == true)
        {
            HitmarkImage.SetActive(true);
            Invoke("HitReset", 0.1f);
            m_Hit = false;

        }
        
    }

    void HitReset()
    {
        HitmarkImage.SetActive(false);
    }
    
}
