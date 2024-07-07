using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unfollow : MonoBehaviour
{
    public CameraFollow _c;
    Player _p;

	void Start ()
    {
        _c = FindObjectOfType<CameraFollow>();
        _p = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {            
            _c.follow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
            if (_p.stars != 0)
            {                
                _c.follow = true;
            }
    }
}
