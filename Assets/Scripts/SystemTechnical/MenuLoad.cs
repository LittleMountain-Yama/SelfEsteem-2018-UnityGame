using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoad : MonoBehaviour
{
    float timer;
    float duration;

    private void Awake()
    {
        duration = 4f;
    }
    void Update ()
    {
        timer += Time.deltaTime * 1;

        if (timer >= duration)
        {
            SceneManager.LoadScene("Menu");
        }

	}
}
