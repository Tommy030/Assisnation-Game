using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertEnemy : MonoBehaviour
{
    private enemyState es;
    void Start()
    {
        es = gameObject.GetComponent<enemyState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (es.hunting == true)
        {
            fallCheck.Instance.Spotted();
            es.hunting = false;
        }
    }
}
