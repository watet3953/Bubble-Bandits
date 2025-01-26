using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallWheel : MonoBehaviour
{
    private int rotSpeed = 300;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, -rotSpeed * Time.deltaTime);
    }
}
