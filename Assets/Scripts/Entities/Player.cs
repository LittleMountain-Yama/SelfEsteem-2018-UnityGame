using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    public List<AudioClip> sounds;
    public int gems;
    public int stars;
    public float timerStun;
    float speed;
    float jumpSpeed;
    float stunTime;
    public float deathTimer;
    float mirrorTime;
    float mirrorCd = 3f;
    float dir;
    float currentDir;
    string sceneName;
    public bool isJumping;
    public bool isDamaged;    
    public bool isDefault;
    public bool egoUnlock;
    public bool mirrorActive;
    public bool mirrorUnlock;
    bool deathTime;   
    public GameObject mirror;
    Rigidbody2D _rb;
    Animator _anim;
    SpriteRenderer _sr;
    BoxCollider2D _bc;
    Brain _brain;
    AudioSource _au;
    GameManager _mng;

    void Awake()
    {
        _au = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();    
        _anim = GetComponent<Animator>();
        _bc = GetComponent<BoxCollider2D>();
        _mng = FindObjectOfType<GameManager>();
        _brain = new Brain(this);     

        stars = 3;
        speed = 6.8f;
        jumpSpeed = 325;
        stunTime = 0.75f;

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "Tutorial")
            mirrorUnlock = false;
        else
            mirrorUnlock = true;       
    }

    void Update()
    {
        if (stars <= 0)
        {
            deathTimer += Time.deltaTime * 1;
            deathTime = true;
        }                

        if (deathTimer >= 1f && deathTime == true)
        {
            if (sceneName == "Tutorial")
            {
                SceneManager.LoadScene("DeathTutorial");
            }
            if (sceneName == "Level 1")
            {
                SceneManager.LoadScene("DeathLvl1");
            }

            if (sceneName == "Level 2")
            {
                SceneManager.LoadScene("DeathLvl2");
            }

            if (sceneName == "Level 3")
            {
                SceneManager.LoadScene("DeathLvl3");
            }

            if (sceneName == "Boss")
            {
                SceneManager.LoadScene("DeathBoss");
            }
            Destroy(this.gameObject);
        }

        _brain.ListenerKey();       

        float axisX = Input.GetAxis("Horizontal");
        _anim.SetFloat("speed", Mathf.Abs(axisX));      
        _anim.SetBool("isJumping", isJumping);
        _anim.SetBool("isDamaged", isDamaged);

        timerStun += 1 * Time.deltaTime;

        if (mirrorUnlock == true)
            mirrorTime += 1 * Time.deltaTime;

        if (mirrorTime > mirrorCd)
        {
            mirrorActive = false;
        }
        
        if (timerStun > stunTime)
        {
            isDamaged = false;
            _sr.color = Color.white;
        }               
    }

    public void Move(float dir)
    {       
        {
            if (isDamaged != true)
            {
                float velY = _rb.velocity.y;
                _rb.velocity = new Vector2(dir * speed, velY);

                if (dir > 0)
                {
                    _sr.flipX = false;
                    currentDir = 1;
                }
                else
                {
                    currentDir = -1;
                    _sr.flipX = true;
                }
            }
        }
    }

    public void Jump()
    {
        if (isJumping != true && deathTime != true)
        {
            _au.Play(0);
            float velY = _rb.velocity.y;
            _rb.velocity = new Vector2(0, velY);
           _rb.AddForce(Vector2.up * jumpSpeed);
            _au.clip = sounds[0];
            _au.Play();
        }
    }

    public void Mirror()
    {
        if (mirrorUnlock == true && deathTime != true)
        {
            _au.Play(2);
            GameObject mirrorTemp = Instantiate(mirror);

            if (_sr.flipX == false)
                mirrorTemp.transform.position = transform.position + Vector3.right * 1.25f;
            else
                mirrorTemp.transform.position = transform.position - Vector3.right * 1.25f;

            mirrorTime = 0;

            _au.clip = sounds[2];
            _au.Play();
        }
    }

    public void Knockback(int dir, float force)
    {
        _au.clip = sounds[3];
        _au.Play();
        float velY = _rb.velocity.y;
        _rb.velocity = new Vector2(0, 0);
        _rb.AddForce(new Vector2(1 * dir, 2f) * force, ForceMode2D.Impulse);
        _sr.color = new Color(0.95f, 0.5f, 0.5f, 0.8f);
        timerStun = 0;
        isDamaged = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 9 || collision.gameObject.layer == 14)
        {            
            isJumping = false;           
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 17)
        {
            _au.clip = sounds[1];
            _au.Play();
        }
        if (collision.gameObject.layer == 16)
        {
            if(stars < 3)
            {
                _au.clip = sounds[1];
                _au.Play();
            }
        }
    }  
}
