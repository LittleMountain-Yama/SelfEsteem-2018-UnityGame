using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
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
            if (_p.stars < 3)
            {
                Destroy(this.gameObject);
                _p.stars += 1;
            }
        }
    }
}
