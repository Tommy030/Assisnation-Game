using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetChar : MonoBehaviour
{
    private enemyState es;
    private EnemyHealth eh;
    void Start()
    {
        es = gameObject.GetComponent<enemyState>();
        eh = gameObject.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (es.dead == true)
        {
            Invoke("StartChar", 2f);
        }
    }

    private void StartChar()
    {
        eh.Restart();
    }
}
