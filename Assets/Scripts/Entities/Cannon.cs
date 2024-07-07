using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Cannon : MonoBehaviour
{
    public List<AudioClip> sounds;
    int life = 2;
    int dir;
    public int range;
    float attackTime;
    float atkCd;
    float jumpTime;
    float jumpCd;
    public float jumpForce;
    bool canAttack;
    public GameObject target;   
    public ProyectileRight proyectileR;
    public ProyectileLeft proyectileL;
    Animator _anim;
    SpriteRenderer _sr;
    Rigidbody2D _rb;
    Player _p;
    AudioSource _au;
    GameManager _mng;
    public ExplosionPassive death1;
    public ExplosionNoDmg death2;
    public Explosion death3;

    void Start ()
    {
        _au = GetComponent<AudioSource>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _p = FindObjectOfType<Player>();
        _rb = GetComponent<Rigidbody2D>();
        _mng = FindObjectOfType<GameManager>();

        target = GameObject.Find("Player");

        if (_mng.difficulty == 0)
        {
            life = 1;
            atkCd = 5f;
            range = 5;
        }
        if (_mng.difficulty == 1)
        {
            life = 2;
            atkCd = 3.2f;
            range = 7;
        }
        if (_mng.difficulty == 2)
        {
            life = 2;
            atkCd = 2.85f;
            jumpCd = 5.2f;
            jumpForce = 5;
            range = 12;
        }
        
    }
	
	void Update ()
    {
        if (life == 0)
        {
            Death();
            Destroy(this.gameObject);
        }

        attackTime += Time.deltaTime * 1;        

        if (target != null)
        {
            if (attackTime > atkCd)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);

                if (range < distance)
                    {
                    Shoot();                    
                }
            }

            if (_mng.difficulty == 2)
            {
                jumpTime += Time.deltaTime * 1;
                if (jumpTime > jumpCd / 1.5f)
                {
                    Jump();
                }
            }
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

    void Shoot()
    {
        if (_sr.flipX == false)
        {
            ProyectileRight bulletTemp = Instantiate(proyectileR);
            bulletTemp.transform.position = transform.position - Vector3.right*1.25f - Vector3.up*0.2f;
        }
        else
        {
            ProyectileLeft bulletTemp = Instantiate(proyectileL);
            bulletTemp.transform.position = transform.position + Vector3.right * 1.25f - Vector3.up * 0.2f;
        }

        attackTime = 0;
        _au.clip = sounds[0];
        _au.Play();       
    }

    void Jump()
    {
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Cannon is jumping");
        jumpTime = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (_p.isDamaged == false)
            {
                collision.gameObject.GetComponent<Player>().Knockback(-5, 1f);                              
            }                        
        }

        if(collision.gameObject.layer == 8)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            if (_mng.difficulty == 0)
            {
                life -= 1;
            }
            if (_mng.difficulty == 1)
            {
                life -= 1;
                _au.clip = sounds[1];
                _au.Play();
            }
            if (_mng.difficulty == 2)
            {
                life -= 1;
                _sr.color = new Color(0.95f, 0.5f, 0.5f, 0.8f);
                atkCd = atkCd * 0.55f;
                _au.clip = sounds[1];
                _au.Play();
            }
        }
    }
}
