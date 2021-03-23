using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallCheck : MonoBehaviour
{
    public static fallCheck Instance;

    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] private GameObject DefaultPoint;
    [SerializeField] private GameObject SecondPoint;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -20)
        {
            gameObject.transform.position = DefaultPoint.transform.position;
        }
    }
    public void Spotted()
    {
        gameObject.transform.position = SecondPoint.transform.position;
    }
}
