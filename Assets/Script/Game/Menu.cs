using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
  public void PlayGame()
  {
        SceneManager.LoadScene("MainScene");
        Debug.Log("play");
  }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MenuGo()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("play");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Mainscene");
        
    }
}
