using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public int life;
    public int distance;
    float speed;    
    float timer;
    float jumpCd;
    float jumpSpeed;
    public bool isJumping;
    public GameObject target;
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
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _p = FindObjectOfType<Player>();
        _rb = GetComponent<Rigidbody2D>();
        _mng = FindObjectOfType<GameManager>();
        target = GameObject.Find("Player");

        life = 1;

        if (_mng.difficulty == 0)
        {
            jumpCd = 1.8f;
            jumpSpeed = 5.5f;
        }
        if (_mng.difficulty == 1)
        {
            jumpCd = 1.8f;
            jumpSpeed = 4;
        }
        if (_mng.difficulty == 2)
        {
            jumpCd = 1.4f;
            jumpSpeed = 3.6f;
        }               
    }

    void Update()
    {
        if (life == 0)
        {
            Death();
            Destroy(this.gameObject);
        }

        _anim.SetBool("isJumping", isJumping);
        timer += Time.deltaTime * 1;

        if (transform.position.x < target.transform.position.x)
        {
            _sr.flipX = false;
        }
        else
        {
            _sr.flipX = true;
        }

        if (timer > jumpCd)
        {                     
            _rb.AddForce(Vector2.up * jumpSpeed * 2, ForceMode2D.Impulse);

            if (_sr.flipX == false)
                _rb.AddForce(Vector2.right * jumpSpeed/2, ForceMode2D.Impulse);
            else
                _rb.AddForce(-Vector2.right * jumpSpeed/2, ForceMode2D.Impulse);

            timer = 0;
            isJumping = true;
            Debug.Log("Jumper is jumping");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (_p.isDamaged == false)
            {
                if (_sr.flipX == false)
                {
                    collision.gameObject.GetComponent<Player>().Knockback(-1, 1f);
                }
                else
                    collision.gameObject.GetComponent<Player>().Knockback(1, 1f);

                _p.stars -= 1;
            }
            life -= 1;
        }

        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 14)
            life -= 1;

        if (collision.gameObject.layer == 8)
            isJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 || collision.gameObject.layer == 14)
            life -= 1;
    }
}
