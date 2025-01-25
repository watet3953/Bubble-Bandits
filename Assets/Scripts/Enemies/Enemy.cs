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

    private int tDamage = 3; //Damage dealt to train
    private int bDamage = 2; //Damage dealt to Bubble Supply

    // Start is called before the first frame update
    void Start()
    {
        startRoom = Random.Range(1,3);
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

        if (startRoom > 0)
        {
            startRoom--;
        }
        else
        {
            Debug.Log("Deal Bubble Damage");
            bubbleDamageDealt += bDamage;
        }
        //Deal Damage
        Debug.Log("Damage Dealt");
        timeLimit = maxTime;

        if (bubbleDamageDealt < 6)
        {
            StartCoroutine(Timer());
        }
        else
        {
            Debug.Log("Bubble Bandit had Left");
        }
        
    }



}
