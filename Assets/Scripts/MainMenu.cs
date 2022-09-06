 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("DificultySelector");
    }
    
    public void StartGameNoCheckPoints()
    {
        Parameters.GameMode = Parameters.Dificulty.NoCheckPoints;
        SceneManager.LoadScene("Game");
    }
    
    public void StartGameEasy()
    {
        Parameters.GameMode = Parameters.Dificulty.Easy;
        SceneManager.LoadScene("Game");
    }
    
    public void StartGameNormal()
    {
        Parameters.GameMode = Parameters.Dificulty.Normal;
        SceneManager.LoadScene("Game");
    }
    
    public void StartGameHard()
    {
        Parameters.GameMode = Parameters.Dificulty.Hard;
        SceneManager.LoadScene("Game");
    }
    
    public void Exit()
    {
        Application.Quit();
    }
    
    public void About()
    {        
        SceneManager.LoadScene("About");
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
