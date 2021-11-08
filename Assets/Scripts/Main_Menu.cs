using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    
    
}
