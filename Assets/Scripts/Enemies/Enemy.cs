using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int maxHealth;
    [SerializeField] private int health;

    [SerializeField] private int startRoom;

    private float maxTime = 5f;
    [SerializeField] private float timeLimit = 5f;

    [SerializeField] private int bubbleDamageDealt = 0;

    private int tDamage = 2; //Damage dealt to train
    private int bDamage = 2; //Damage dealt to Bubble Supply

    private int bRoom = 2; // the location of the bubble room in the trainManager array.

    EnemyPoint dest;

    // Start is called before the first frame update
    void Start()
    {
        //startRoom = Random.Range(1,3);
        startRoom = 0;
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {


    }


    IEnumerator Timer(float wait = 0.1f, float less = 0.1f)
    {

        while (timeLimit > 0)
        {
            timeLimit -= less;
            yield return new WaitForSeconds(wait);
        }

        //will move the enemy to the next room if there is an open spot.
        //if there isn't then it will stay in the same room.
        if (startRoom < bRoom)
        {
            if(dest != null)//has a previous point.
            {
                dest.e = null;
                dest.occupied = false;
                dest = TrainManager.Instance.findOpenSpot(startRoom);
            }
            else//first point.
            {
                dest = TrainManager.Instance.findOpenSpot(startRoom);
                dest.e = gameObject;
                dest.occupied = true;
            }

            //Are we having them jump or move?
            
            if (dest != null)
            {
                transform.position = dest.transform.position;
                dest.e = gameObject;
            }
            startRoom++;

            //Deal Damage
            TrainManager.Instance.tHealth -= tDamage;
        }

        if(startRoom == bRoom)
        {
            Debug.Log("Deal Bubble Damage");
            TrainManager.Instance.tHealth -= tDamage;
            TrainManager.Instance.bSupply -= bDamage;
            bubbleDamageDealt += bDamage;
        }




        Debug.Log("Damage Dealt");
        timeLimit = maxTime;

        if (bubbleDamageDealt < 6)
        {
            StartCoroutine(Timer());
        }
        else
        {
            Debug.Log("Bubble Bandit had Left");
            gameObject.SetActive(false);
        }
        
    }
}
