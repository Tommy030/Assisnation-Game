using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    // een singleton
    public static Score Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [SerializeField] private TMP_Text HighScore, CurrentScore;
    public int MainMenu;
    public float ScoreNumber;

    private void Start()
    {
        Time.timeScale = 0;
        CurrentScore.text = "Current Time: " + ScoreNumber.ToString("F0");                                 // dit zet de player zijn naam en score(die zal altijd 0 hier zijn) in het currentscore object zodat je het in game kan zien
        HighScore.text = " Fastest Time: " + PlayerPrefs.GetFloat("HighScore").ToString("F1");                       // hier wordt de oude highscore en zijn houder in het highscore object gezet


    }

    // dit is een functie die overal aangeroepen kan worden om score toe te voegen in de huidige game
    public void Addscore(int _score, int _gold)
    {
        ScoreNumber += _score;    // de doorgegeven hoeveelheid wordt toegevoegd

        CurrentScore.text = " Current Time: " + ScoreNumber.ToString();  // currentscore object wordt geupdate ik vond dit beter dan het gewoon in de update zetten
 // currentscore object wordt geupdate ik vond dit beter dan het gewoon in de update zetten
        
    }

    private void Update()
    {
        ScoreNumber += Time.deltaTime;
        CurrentScore.text = " Current Time: " + ScoreNumber.ToString("F1");  // currentscore object wordt geupdate ik vond dit beter dan het gewoon in de update zetten
    }
    //hier wordt gecheckt of een nieuwe highscore is gehaald
    public void CheckHighScore()
    {
        if (ScoreNumber < PlayerPrefs.GetFloat("HighScore"))    // hij kijkt of de huidige score hoger is dan de high score
        {
            HighScore.text = " Fastest Time: " + ScoreNumber.ToString("F1"); ;  // omdat de playerprefs niet direct updaten wordt dan de huidige score en player toegevoegd aan het highscore gobject

            // de nieuwe highscore wordt gesaved
            PlayerPrefs.SetFloat("HighScore", ScoreNumber);
        }
        else
        {
            HighScore.text = "Fastest Time: " + PlayerPrefs.GetFloat("HighScore").ToString("F1"); // er verandert niks aan de highscore
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }
}
