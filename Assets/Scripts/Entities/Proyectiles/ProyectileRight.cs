using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectileRight : MonoBehaviour
{
    float speed = 9;
    float timer;
    float bulletTime = 6;    
    Player _p; 
    SpriteRenderer _sr;
    BoxCollider2D _bc;

    void Start ()
    {
        _sr = GetComponent<SpriteRenderer>();
        _bc = GetComponent<BoxCollider2D>();
        _p = FindObjectOfType<Player>();
    }

    void Update()
    {
        timer += Time.deltaTime * 1;
               
        transform.position += -transform.right * speed * Time.deltaTime;
       
        if (timer > bulletTime)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 8 || collision.gameObject.layer == 14 || collision.gameObject.layer == 9)
            Destroy(this.gameObject);

        if (collision.gameObject.GetComponent<Player>())
        {
            if (_p.isDamaged == false)
            {
                collision.gameObject.GetComponent<Player>().Knockback(-1, 1.5f);
                _p.stars -= 1;
            }           
        }        
    }    
}
