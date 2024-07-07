using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour{  

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GoToSelect()
    {
        SceneManager.LoadScene("Select");
    }
    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void GoToLvl1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void GoToLvl2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void GoToLvl3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void GoToBoss()
    {
        SceneManager.LoadScene("Boss");
    }        
    public void Exit()
    {
        Application.Quit();
    }   
}
