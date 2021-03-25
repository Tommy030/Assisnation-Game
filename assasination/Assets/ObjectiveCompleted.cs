using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveCompleted : MonoBehaviour
{
    public static ObjectiveCompleted Instance;
    private void Awake()
    {
        Instance = this;
    }
    public bool ObjComp;
    [SerializeField] private GameObject Player;
    [SerializeField] private int SceneToJumpTo = 1;
    void Start()
    {
        //Debug.Log("ping");

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(gameObject.transform.position, Player.transform.position));

        if (ObjComp == true && Vector3.Distance(gameObject.transform.position,Player.transform.position) < 5)
        {
            Score.Instance.CheckHighScore();
            Debug.Log("ping");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SceneToJumpTo);
        }
    }
}
