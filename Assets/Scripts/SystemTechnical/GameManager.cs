using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager: MonoBehaviour
{    
    public Text selectText;
    public int difficulty;
    public float timer;
    float duration;
    string sceneName;

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        DontDestroyOnLoad(gameObject);     
        difficulty = 0;
        duration = 2.2f;
    }

    private void Update()
    {
        if (sceneName == "Select")
        {
            timer += Time.deltaTime * 1;
            if (timer > duration)
            {
                selectText.text = " ";
            }
        }
    }

    public void ChangeDiffTo1()
    {
        difficulty = 0;
        timer = 0;
        selectText.text = "Difficulty changed to 1!";
    }
    public void ChangeDiffTo2()
    {
        difficulty = 1;
        timer = 0;
        selectText.text = "Difficulty changed to 2!";
    }
    public void ChangeDiffTo3()
    {
        difficulty = 2;
        timer = 0;
        selectText.text = "Difficulty changed to 3!";
    }
}
