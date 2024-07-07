using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string sceneName;
    Player _p;

    void awake ()
    {
        _p = FindObjectOfType<Player>();       
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (sceneName == "Tutorial")
            {
                SceneManager.LoadScene("WinTutorial");
            }
            if (sceneName == "Level 1")
            {
                SceneManager.LoadScene("WinLvl1");
            }

            if (sceneName == "Level 2")
            {
                SceneManager.LoadScene("WinLvl2");
            }

            if (sceneName == "Level 3")
            {
                SceneManager.LoadScene("WinLvl3");
            }

            if (sceneName == "Boss")
            {
                SceneManager.LoadScene("WinBoss");
            }

            Debug.Log("Exit used");
        }
    }
}
