using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidDeath : MonoBehaviour
{
    Player _p;
    Enemy _m;
    

    private void Start()
    {
        _p = FindObjectOfType<Player>();
        _m = FindObjectOfType<Enemy>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            _p.stars = 0;

        if (collision.gameObject.GetComponent<Enemy>())
            _m.life = 0;
    }

}
