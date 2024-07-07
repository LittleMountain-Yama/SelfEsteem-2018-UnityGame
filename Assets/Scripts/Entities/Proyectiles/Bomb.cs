using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{   
    float timer;
    float bombTime = 8;
    bool selfDestruct;
    Player _p;
    SpriteRenderer _sr;
    BoxCollider2D _bc;
    Rigidbody2D _rb;
    Animator _anim;    
    public Explosion boom;

    void Start ()
    {        
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _bc = GetComponent<BoxCollider2D>();
    }
	
	void Update ()
    {
        timer += Time.deltaTime * 1;

        if (timer > bombTime)
        {           
            Destroy(this.gameObject);            
        }

        if (selfDestruct == true)
        {
            if (timer > 1.3f)
            {
                Explosion bulletTemp = Instantiate(boom);
                bulletTemp.transform.position = transform.position;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 8 || collision.gameObject.layer == 9 || collision.gameObject.layer == 13)
        {            
            Explosion bulletTemp = Instantiate(boom);
            bulletTemp.transform.position = transform.position;
            Destroy(this.gameObject);
        }        
    }    

    public void Knockback(int dir, float force)
    {
        float velY = _rb.velocity.y;
        _rb.velocity = new Vector2(0, 0);
        _rb.AddForce(new Vector2(1 * dir, 2f * force), ForceMode2D.Impulse);

        timer = 0;
        selfDestruct = true;        
    }
}
