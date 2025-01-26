using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
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

    int speed = 8;

    [SerializeField] private TrainCar[] rooms;
    [SerializeField] private GameObject leftWarn;
    [SerializeField] private GameObject rightWarn;

    private bool isMoving = false;

    [SerializeField] private CardHand had;
    private bool d = true; //can draw or no?
    [SerializeField] private GameObject t;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = camPoints[target].position;
        player.transform.position = pPoints[target].transform.position;
        pPoints[target].occupied = true;
        //StartCoroutine(MoveCam(target));
        //leftWarn.SetActive(false);
        //rightWarn.SetActive(false);

    }

    private void Update()
    {

        if(target == 3)
        {
            Right.interactable = false;
        }
        else
        {
            Right.interactable= true;
        }

        if(target == 0)
        {
            Left.interactable = false;
        }
        else
        {
            Left.interactable= true;
        }

        //Need to check the rooms next to the current one.
        //If there is an enemy in the room, then it will show the exclamation icon.

        if (target > 0)
        {
            CheckLeft();
        }

        if (target < rooms.Length - 1) {
            CheckRight();
        }


        if(target == rooms.Length - 1 && d)
        {
            t.gameObject.SetActive(true);

            StartCoroutine(Timer());
            
        }

        if (target < rooms.Length-1 && !d)
        {
            d = true;

        }


    }

    private void CheckLeft()
    {

        if (target == 0)
        {
            leftWarn.SetActive(false);
        }

        //look at car too the left
        foreach(EnemyPoint e in rooms[target - 1].enemyPoints)
        {
            if (e.occupied)
            {
                leftWarn.SetActive(true);
                break;
            }
            else
            {
                leftWarn.SetActive(false);
            }
        }
    }

    private void CheckRight()
    {

        if (target == rooms.Length -1)
        {
            rightWarn.SetActive(false);
        }
        foreach (EnemyPoint e in rooms[target + 1].enemyPoints)
        {
            if (e.occupied)
            {
                rightWarn.SetActive(true);
                break;
            }
            else
            {
                rightWarn.SetActive(false);
            }
        }
    }



    public void MoveLeft()
    {


        if (!isMoving)
        {
            isMoving = true;
            pPoints[target].occupied = false;
            target--;
            player.transform.position = pPoints[target].transform.position;
            pPoints[target].occupied = true;
            Left.interactable = false;
            Right.interactable = false;
            StartCoroutine(MoveCam(target));
        }
    }

    public void MoveRight()
    {

        if (!isMoving)
        {
            isMoving = true;
            pPoints[target].occupied = false;
            target++;
            player.transform.position = pPoints[target].transform.position;
            pPoints[target].occupied = true;
            Left.interactable = false;
            Right.interactable = false;
            StartCoroutine(MoveCam(target));
        }
    }

    IEnumerator Timer(float wait = 0.1f, float time = 3f)
    {
        while(time > 0)
        {
            time -= wait;
            yield return new WaitForSeconds(wait);
        }
        d = false;
        had.RefillCards();
        t.gameObject.SetActive(false);
        StopCoroutine(Timer());

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
        isMoving = false;
    }

}
