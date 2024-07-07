using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiny : MonoBehaviour
{
    Player _p;

    private void Start()
    {
        _p = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
                _p.mirrorUnlock = true;
                Destroy(this.gameObject);            
        }
    }
}
