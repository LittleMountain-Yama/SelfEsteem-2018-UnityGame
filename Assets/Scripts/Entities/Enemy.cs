using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<Transform> waypoints;
    public int life;
    public int distance;
    float speed;
    float range;
    public float guardTimer;
    float guard;
    public float dis;
    public int currentWaypoint;
    public GameObject target;
    Animator _anim;
    SpriteRenderer _sr;
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
        _mng = FindObjectOfType<GameManager>();
             
        currentWaypoint = 0;
        life = 1;

        if (_mng.difficulty == 0)
        {            
            speed = 2.8f;
            guard = 2;
        }
        if (_mng.difficulty == 1)
        {
            speed = 3.1f;
            guard = 1.5f;
        }
        if (_mng.difficulty == 2)
        {            
            range = 0.5f;
            speed = 3.5f;
            guard = 1f;
            target = GameObject.Find("Player");
        }
    }

    void Update()
    {
        if (life <= 0)
        {
            Death();
            Destroy(this.gameObject);
        }

        if ((_mng.difficulty == 2 && range < distance) || (_mng.difficulty <= 1))
        {
            if (_sr.flipX == true)
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
            else
            {
                transform.position += -transform.right * speed * Time.deltaTime;
            }
        }

        guardTimer += Time.deltaTime * 1;

        if(guardTimer >= guard)
        {
            Guard();
        }

        if (target != null)
        {
            Chase();
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

    private void Guard()
    {
        if (_sr.flipX == true)
        {
            _sr.flipX = false;
        }
        else
        {
            _sr.flipX = true;
        }
        guardTimer = 0;
    }

    private void Chase()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        dis = Vector3.Distance(transform.position, target.transform.position);

        if (range < distance)
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.position += direction.normalized * speed * Time.deltaTime;
            guardTimer = 0;
        }

        if (transform.position.x < target.transform.position.x)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
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
                    collision.gameObject.GetComponent<Player>().Knockback(-2, 1f);
                }
                else
                    collision.gameObject.GetComponent<Player>().Knockback(2, 1f);

                _p.stars -= 1;
            }
            life -= 1;
        }

        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 14)
            life -= 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
            life -= 1;
    }
}
