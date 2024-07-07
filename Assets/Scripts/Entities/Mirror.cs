using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    float mirrorTime;
    float mirrorCd = 2.2f;
    public ProyectileLeft proyectileL;
    public ProyectileRight proyectileR;
    public Bomb bomb;
    GameManager _mng;

    void Update ()
    {
        mirrorTime += 1 * Time.deltaTime;

        if (mirrorTime > mirrorCd)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bomb>())
        {            
          collision.gameObject.GetComponent<Bomb>().Knockback(0, 5f);               
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ProyectileLeft>())
        {
            ProyectileRight bulletTemp = Instantiate(proyectileR);
            bulletTemp.transform.position = transform.position + Vector3.left * 1.1f;

            if (_mng.difficulty == 1 || _mng.difficulty == 2)
            {
                Destroy(this.gameObject);
            }
        }
        if (collision.gameObject.GetComponent<ProyectileRight>())
        {
            ProyectileLeft bulletTemp = Instantiate(proyectileL);
            bulletTemp.transform.position = transform.position + Vector3.right * 1.1f;

            if (_mng.difficulty == 1 || _mng.difficulty == 2)
            {
                Destroy(this.gameObject);
            }
        }
    }

    
}
