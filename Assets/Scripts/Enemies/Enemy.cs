using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int maxHealth;
    [SerializeField] public int health;

    [SerializeField] private int startRoom;

    private float maxTime = 5f;
    [SerializeField] private float timeLimit = 5f;

    [SerializeField] private int bubbleDamageDealt = 0;

    private int tDamage = 2; //Damage dealt to train
    private int bDamage = 2; //Damage dealt to Bubble Supply

    private int bRoom = 2; // the location of the bubble room in the trainManager array.

    EnemyPoint dest;

    public bool s = false;
    bool sw = false;

    // Start is called before the first frame update
    void Start()
    {
        startRoom = Random.Range(0,2);
        //startRoom = 1;
        //StartCoroutine(Timer());
    }

    void Update()
    {
        if (s && !sw)
        {
            Spawn();
            StartCoroutine(Timer());
            sw = true;
        }


        if(health <= 0)
        {
            TrainManager.Instance.dead.Add(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    public void Spawn()
    {
        bool hasP = TrainManager.Instance.checkForPlayer(startRoom);
        if (dest == null && hasP)//first point.
        {
            if (startRoom == 0)
            {
                startRoom++;
                hasP = TrainManager.Instance.checkForPlayer(startRoom);
            }
            else if (startRoom == 1)
            {
                startRoom--;
                hasP = TrainManager.Instance.checkForPlayer(startRoom);
            }
            if (!hasP)
            {
                dest = TrainManager.Instance.findOpenSpot(startRoom);
                gameObject.transform.position = dest.transform.position;
                dest.e = gameObject;
                dest.occupied = true;
            }
        }
        else if (dest == null && !hasP)
        {
            dest = TrainManager.Instance.findOpenSpot(startRoom);
            gameObject.transform.position = dest.transform.position;
            dest.e = gameObject;
            dest.occupied = true;
        }

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
            bool hasP = TrainManager.Instance.checkForPlayer(startRoom);
            bool hasP2 = TrainManager.Instance.checkForPlayer(startRoom + 1);
            if (dest != null && !hasP && !hasP2)//has a previous point.
            {
                dest.e = null;
                dest.occupied = false;
                startRoom++;
                dest = TrainManager.Instance.findOpenSpot(startRoom);
                gameObject.transform.position = dest.transform.position;

                //Deal Damage
                Debug.Log("Damage Dealt");
                GameManager.Instance.tHealth -= tDamage;
            }
        }

        if(startRoom == bRoom)
        {
            Debug.Log("Deal Bubble Damage");
            GameManager.Instance.tHealth -= tDamage;
            GameManager.Instance.bSupply -= bDamage;
            bubbleDamageDealt += bDamage;
        }

        timeLimit = maxTime;

        if (bubbleDamageDealt < 6)
        {
            StartCoroutine(Timer());
        }
        else
        {
            Debug.Log("Bubble Bandit had Left");
            TrainManager.Instance.dead.Add(this.gameObject);
            gameObject.SetActive(false);
        }
        
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public void SlowDown(int slowMultiplier)
    {
        maxTime *= slowMultiplier;
        timeLimit = maxTime;
    }
}
