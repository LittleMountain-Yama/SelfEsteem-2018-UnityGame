using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionNoDmg : MonoBehaviour
{
    public AudioClip sound;
    AudioSource _au;
    Player _p;
    float timer;
    float dissTime;

    private void Start()
    {
        _au = GetComponent<AudioSource>();
        _au.clip = sound;
        _au.Play();
        _p = FindObjectOfType<Player>();
        dissTime = 0.7f;
    }

    private void Update()
    {
        timer += 1 * Time.deltaTime;

        if (timer > dissTime)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            if (_p.isDamaged == false)
            {
                collision.gameObject.GetComponent<Player>().Knockback(-1, 1.7f);                
            }
        }
    }
}
