using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStatus : MonoBehaviour
{
    [SerializeField] private GameObject[] Gun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableGun()
    {
        for (int i = 0; i < Gun.Length; i++)
        {
            Gun[i].GetComponent<MeshRenderer>().enabled = false;
            
        }
        Gun[0].GetComponent<Gun>().IsSelected = false;
    }
    public void EnableGun()
    {
        for (int i = 0; i < Gun.Length; i++)
        {
            Gun[i].GetComponent<MeshRenderer>().enabled = true;
            
        }
        Gun[0].GetComponent<Gun>().IsSelected = true;
    }
}
