using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButtons : MonoBehaviour
{
    public static MainMenuButtons m_instance;
    
    private void Awake()
    {
        if (m_instance == null) m_instance = this;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
    }
    public void ExitGame()
    {
        Application.Quit(0);
    }
}
