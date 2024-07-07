using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public List<AudioClip> sounds;
    public int life;
    float range;
    public float bulletTime;
    float bulletCd;
    float bombTime;
    float bombCd;
    float jumpTime;
    float jumpCd;
    float jumpSpeed;
    float timerDamage;
    float invulnerability;
    float deadTime;
    public float deathTimer;   
    public bool isJumping;
    bool isInvulnerable;
    bool isDead;
    public GameObject target;
    public Bomb bomb;
    public ProyectileRight proyectileR;
    public ProyectileLeft proyectileL;
    SpriteRenderer _sr;
    Rigidbody2D _rb;
    Animator _anim;
    Player _p;
    AudioSource _au;
    GameManager _mng;

    private void Awake()
    {
        _au = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
        _p = FindObjectOfType<Player>();
        _mng = FindObjectOfType<GameManager>();
        target = GameObject.Find("Player");

        timerDamage = 0;

        if (_mng.difficulty == 0)
        {
            life = 2;
            jumpSpeed = 8;
            jumpCd = 3f;
            bulletCd = 3f;
            invulnerability = 0.5f;
            isInvulnerable = false;
        }
        if (_mng.difficulty == 1)
        {
            life = 3;
            jumpSpeed = 7;
            jumpCd = 2.5f;
            bulletCd = 3f;
            invulnerability = 0.9f;
            isInvulnerable = false;
        }
        if (_mng.difficulty == 2)
        {
            life = 3;
            jumpSpeed = 10;
            jumpCd = 1.85f;
            bulletCd = 2f;
            invulnerability = 1.5f;
            isInvulnerable = true;
        }             
    }

    private void Update()
    {
        _anim.SetBool("isJumping", isJumping);
        _anim.SetBool("isDead", isDead);

        timerDamage += 1 * Time.deltaTime;

        if (timerDamage >= invulnerability)
        {
            isInvulnerable = false;
        }

        if (life == 2)
        {
            _sr.color = new Color(0.1f, 0.1f, 0.1f, 0.9f);
        }
        if (life == 1)
        {
            _sr.color = new Color(0.75f, 0.75f, 0.75f, 0.5f);
        }
        if (life == 0)
        {
            isDead = true;            
        }
        
        if (isDead != true)
        {
            jumpTime += Time.deltaTime * 1;
            bombTime += Time.deltaTime * 1;
            bulletTime += Time.deltaTime * 1;
        }
        else
        {
            _au.clip = sounds[2];
            _au.Play();
            deathTimer += Time.deltaTime * 1;
            if (deathTimer > deadTime)
                SceneManager.LoadScene("WinBoss");
        }

        if (transform.position.x < target.transform.position.x)
        {
            _sr.flipX = false;
        }
        else
        {
            _sr.flipX = true;
        }

        if (jumpTime > jumpCd)
        {
            if (isJumping != true)
            {
                _rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);

                if (_sr.flipX == false)
                    _rb.AddForce(Vector2.right * jumpSpeed, ForceMode2D.Impulse);
                else
                    _rb.AddForce(-Vector2.right * jumpSpeed, ForceMode2D.Impulse);

                isJumping = true;
                jumpTime = 0;
                _au.clip = sounds[1];
                _au.Play();
            }                     
        }

        if (bombTime > jumpCd + 0.3f)
        {            
           Bomb bulletTemp = Instantiate(bomb);
           bulletTemp.transform.position = transform.position - Vector3.up * 4f;                   
           bombTime = 0;
        }

        if (bulletTime > bulletCd)
        {
            bulletTime = 0;

            if (_sr.flipX == true)
            {
                ProyectileRight bulletTemp = Instantiate(proyectileR);
                bulletTemp.transform.position = transform.position - Vector3.right * 2f - Vector3.up * 1f;
                _au.clip = sounds[4];
                _au.Play();
            }
            else
            {
                ProyectileLeft bulletTemp = Instantiate(proyectileL);
                bulletTemp.transform.position = transform.position + Vector3.right * 2f - Vector3.up * 1f;
                _au.clip = sounds[4];
                _au.Play();
            }            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isInvulnerable != true)
        {
            if (collision.gameObject.layer == 14)
            {
                life -= 1;
                isInvulnerable = true;
                timerDamage = 0;
                _au.clip = sounds[0];
                _au.Play();
            }
        }
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9 || collision.gameObject.layer == 11 || collision.gameObject.layer == 14)
        {
            isJumping = false;
        }
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvulnerable != true)
        {
            if (_mng.difficulty == 0)
            {
                if (collision.gameObject.layer == 19)
                {
                    life -= 1;
                    isInvulnerable = true;
                    timerDamage = 0;
                    _au.clip = sounds[0];
                    _au.Play();
                }
            }
            if (_mng.difficulty == 1)
            {
                if (collision.gameObject.layer == 10)
                {
                    life -= 1;
                    isInvulnerable = true;
                    timerDamage = 0;
                    _au.clip = sounds[0];
                    _au.Play();
                }
            }
        }
    }
}
