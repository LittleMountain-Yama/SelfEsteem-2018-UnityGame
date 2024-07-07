using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrike : MonoBehaviour
{
    public int life;
    public int distance;
    int dir;
    float speed;
    float lifeTimer;
    float lifeExpectancy;
    float bombRate;
    float bombTimer;
    public Bomb bomb;    
    Animator _anim;
    SpriteRenderer _sr;
    Rigidbody2D _rb;
    Player _p;
    GameManager _mng;
    public ExplosionPassive death1;
    public ExplosionNoDmg death2;
    public Explosion death3;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _p = FindObjectOfType<Player>();
        _mng = FindObjectOfType<GameManager>();

        life = 1;

        if (_mng.difficulty == 0)
        {
            speed = 6f;
            bombRate = 4;           
            lifeExpectancy = 5;
        }
        if (_mng.difficulty == 1)
        {
            speed = 4f;            
            bombRate = 2.3f;           
            lifeExpectancy = 10;
        }
        if (_mng.difficulty == 2)
        {
            speed = 3.5f;
            bombRate = 1.8f;            
            lifeExpectancy = 15;
        }        
    }

    void Update()
    {
        if (life <= 0)
        {
            Death();
            Destroy(this.gameObject);
        }       

        bombTimer += Time.deltaTime * 1;
        lifeTimer += Time.deltaTime * 1;

        if(_sr.flipX == false)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }

        float velY = _rb.velocity.y;
        _rb.velocity = new Vector2(dir * speed, velY);

        if (bombTimer > bombRate)
        {                                      
            Deploy();               
        }

        if (lifeTimer > lifeExpectancy)
        {
            life -= 1;
        }

    }

    private void Death()
    {
        if (_mng.difficulty == 0)
        {
            ExplosionPassive bulletTemp = Instantiate(death1);
            bulletTemp.transform.position = transform.position;
        }

        if (_mng.difficulty == 1)
        {
            ExplosionNoDmg bulletTemp = Instantiate(death2);
            bulletTemp.transform.position = transform.position;
        }

        if (_mng.difficulty == 2)
        {
            Explosion bulletTemp = Instantiate(death3);
            bulletTemp.transform.position = transform.position;
        }
    }

    void Deploy()
    {      
        Bomb bulletTemp = Instantiate(bomb);
        bulletTemp.transform.position = transform.position - Vector3.up * 1.05f;       
        bombTimer = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (_p.isDamaged == false)
            {
                collision.gameObject.GetComponent<Player>().Knockback(-1, 1f);
                _p.stars -= 2;
            }
            life -= 1;
        }
        if (collision.gameObject.layer == 8)
            life -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
            life -= 1;
    }
}
