using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform[] camPoints;

    [SerializeField] private GameObject player;
    [SerializeField] private EnemyPoint[] pPoints; //Player points

    [SerializeField] private Button Left;
    [SerializeField] private Button Right;

    private int target = 3;

    int speed = 5;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = camPoints[target].position;
        player.transform.position = pPoints[target].transform.position;
        pPoints[target].occupied = true;
        //StartCoroutine(MoveCam(target));

    }

    public void MoveLeft()
    {
        pPoints[target].occupied = false;
        target--;
        player.transform.position = pPoints[target].transform.position;
        pPoints[target].occupied = true;
        Left.interactable = false;
        Right.interactable = false;
        StartCoroutine(MoveCam(target));
    }

    public void MoveRight()
    {
        pPoints[target].occupied = false;
        target++;
        player.transform.position = pPoints[target].transform.position;
        pPoints[target].occupied = true;
        Left.interactable = false;
        Right.interactable = false;
        StartCoroutine(MoveCam(target));
    }



    IEnumerator MoveCam(int t)
    {

        while (transform.position != camPoints[t].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, camPoints[t].position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Left.interactable = true;
        Right.interactable = true;

    }

}
