using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    Player _p;

    void Start()
    {
        _p = FindObjectOfType<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {                
        if (collision.gameObject.GetComponent<Player>())
        {
            if (_p.isDamaged == false)
            {
                collision.gameObject.GetComponent<Player>().Knockback(0, 3f);
                _p.stars -= 1;
            }            
        }
    }
}
