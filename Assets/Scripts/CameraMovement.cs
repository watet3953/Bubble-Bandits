using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform[] camPoints;

    [SerializeField] private int target;

    int speed = 5;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveCam(target));

    }

    // Update is called once per frame
    void Update()
    {
        //When theres a new destination, move to it.
        //There will be buttons that will increase or decreas the target num by 1.
        //Then camera will slideeeeee over to the new point.
        //make a mini cool down so they can't spam it and potentialy break.

        if (Input.GetMouseButtonDown(0) && target < camPoints.Length)
        {
            target++;
            StartCoroutine(MoveCam(target));
        }else if (Input.GetMouseButtonDown(1) && target > 0)
        {
            target--;
            StartCoroutine(MoveCam(target));
        }


    }


    IEnumerator MoveCam(int t)
    {

        while (transform.position != camPoints[t].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, camPoints[t].position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

    }

}
