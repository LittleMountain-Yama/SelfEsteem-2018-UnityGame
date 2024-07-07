using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    Player _p;
    public Text gemsText;      
    public GameObject starImageA;
    public GameObject starImageB;
    public GameObject starImageC;
    public GameObject mirror;

    private void Awake()
    {
        _p = FindObjectOfType<Player>();
    }

    void Update()
    {        
        if (_p.gems > -1)
        {            
            gemsText.text = "" + _p.gems;
        }

        if(_p.stars == 3)
        {
            starImageA.SetActive(true);
            starImageB.SetActive(true);
            starImageC.SetActive(true);
        }
        else if (_p.stars == 2)
        {
            starImageA.SetActive(true);
            starImageB.SetActive(true);
            starImageC.SetActive(false);
        }
        else if (_p.stars == 1)
        {
            starImageA.SetActive(true);
            starImageB.SetActive(false);
            starImageC.SetActive(false);
        }
        else
        {
            starImageA.SetActive(false);
            starImageB.SetActive(false);
            starImageC.SetActive(false);
        }

        if (_p.mirrorActive == false && _p.mirrorUnlock == true)
        {
          mirror.SetActive(true);
        }
        else
        mirror.SetActive(false);
        
    }
}
