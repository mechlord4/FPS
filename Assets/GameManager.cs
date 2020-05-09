using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public void toGame()
    {
        SceneManager.LoadScene("Game1");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void toMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void toCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void toWin()
    {
        SceneManager.LoadScene("Win");
    }
    public void toDeath()
    {
        SceneManager.LoadScene("Death");
    }
    public void ToGame2() 
    {
        SceneManager.LoadScene("Game2");
    }
}
