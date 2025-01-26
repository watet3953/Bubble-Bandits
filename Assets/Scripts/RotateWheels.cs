using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour
{
    private int rotSpeed = 150;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate( 0, 0, -rotSpeed * Time.deltaTime);
    }
}
