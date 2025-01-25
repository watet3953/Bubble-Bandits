using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform[] camPoints;

    [SerializeField] private int target = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //When theres a new destination, move to it.
        //There will be buttons that will increase or decreas the target num by 1.
        //Then camera will slideeeeee over to the new point.
        //make a mini cool down so they can't spam it and potentiall break.



    }
}
