using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain
{
    Player _p;
    CameraControl _c;
    public int difficulty = 1;

    public Brain(Player player)
    {
        _p = player;        
    }
   
    public void ListenerKey()
    {
        if ( _p.isDamaged != true && _p.isDefault != true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _p.Jump();
                _p.isJumping = true;
            }
        }

        if (_p.mirrorActive == false)
        {
            if(Input.GetKeyDown(KeyCode.V))
            {
                _p.Mirror();
                _p.mirrorActive = true;
            }
        }

        if (Input.GetAxis("Horizontal") != 0)
            if (_p.isDamaged != true)
               _p.Move(Input.GetAxis("Horizontal"));
    }   
}
