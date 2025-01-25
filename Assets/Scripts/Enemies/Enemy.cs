using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int maxHealth;
    [SerializeField] private int health;

    private int startRoom;

    // Start is called before the first frame update
    void Start()
    {
        startRoom = Random.Range(1,3);
    }

    // Update is called once per frame
    void Update()
    {
        //There is one timer that will control how the enemy works.
        //It will be the time that the enemy will be in the cart before dealing damage and moving on

        //The enemy will be in the cart for 5 seconds and then deal 3 damage to the train.

        //Once the enemy deals the damage, it will move to the next room if it has the room (max 3 per car, 2 for storage)

        //Once the enemy is in the storage room and deals damage, it will deal 2 bubble supply damage.

        //Once the enemy has delt 6 bubble damage, it will leave the train.

    }


    IEnumerator Timer()
    {

        return null;
    }



}
