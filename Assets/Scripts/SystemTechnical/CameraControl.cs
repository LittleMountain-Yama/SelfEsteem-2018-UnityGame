using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{        
    public bool sad;
    public bool ego;
    public GameObject cameraSad;
    public GameObject cameraEgo;
    Player _p;

    private void Start()
    {        
        sad = true;
        ego = false;
        _p = FindObjectOfType<Player>();
    }

    void Update ()
    {
        if (sad)
        {
            cameraSad.SetActive(true);
            cameraEgo.SetActive(false);
        }
        else
        {
            cameraSad.SetActive(false);
            cameraEgo.SetActive(true);
        }

        if (_p.mirrorUnlock == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.Log("The letter C has been pressed");

                if (sad == true)
                {
                    ego = true;
                    sad = false;
                }
                else
                {
                    ego = false;
                    sad = true;
                }

            }
        }
    }
}
