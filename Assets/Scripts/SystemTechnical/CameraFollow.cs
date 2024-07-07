using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool follow = true;
    public Player target;

    void Update()
    {
        if (target != null)
        {
            if (follow == true)
                transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        }
    }
}
