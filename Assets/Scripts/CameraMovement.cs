using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform[] camPoints;

    [SerializeField] private GameObject player;
    [SerializeField] private Transform[] pPoints; //Player points

    private int target = 3;

    int speed = 5;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = camPoints[target].position;
        player.transform.position = pPoints[target].position;
        //StartCoroutine(MoveCam(target));

    }

    // Update is called once per frame
    void Update()
    {
        //When theres a new destination, move to it.
        //There will be buttons that will increase or decreas the target num by 1.
        //Then camera will slideeeeee over to the new point.
        //make a mini cool down so they can't spam it and potentialy break.



    }

    public void MoveLeft()
    {
        target--;
        player.transform.position = pPoints[target].position;
        StartCoroutine(MoveCam(target));
    }

    public void MoveRight()
    {
        target++;
        player.transform.position = pPoints[target].position;
        StartCoroutine(MoveCam(target));
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
