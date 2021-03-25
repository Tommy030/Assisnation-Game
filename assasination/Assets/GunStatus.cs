using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStatus : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableGun()
    {
        Gun.GetComponent<MeshRenderer>().enabled = false;
        Gun.GetComponent<Gun>().IsSelected = false;
    }
    public void EnableGun()
    {
        Gun.GetComponent<MeshRenderer>().enabled = true;
        Gun.GetComponent<Gun>().IsSelected = true;
    }
}
