using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    public int tHealth; //Train Health
    public int bSupply; //Bubble Wrap Supply

    private int maxTHealth = 100; //The maxBSupply and maxTHealth are the same number

    //Forgive me for a bad kind of coding here :)
    [SerializeField] private TrainCar[] trainCars;


    #region Singleton
    public static TrainManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Two copies of TrainManager");
            Destroy(this);
        }
        Instance = this;
    }
    #endregion Singleton

    private void Start()
    {
        tHealth = maxTHealth;
        bSupply = maxTHealth;
    }


    //Get the four rooms and keep them in an array for reference.

    //Get the enemy spots from the rooms and hold that in a seperate array?
    //The spots should have a mini script that lets them know if they are occupied or not.

    //For the Encounter Builder:
    //Needs the time that the encounter will last.
    //Once that timer is done then one of two things happen, the encounter ends OR it no longer spawns enemies and the encounter is done once all enemies are dead.
    //Enemies will spawn in after 10 seconds? and move their way to their starting room before entering the room. If the room they want to enter has no spots.
    //It will either reroll the room or wait.

    //For the Card Restock NEED THE CARDS READY FOR THIS.
    //when the player is in the engine, they can press some button to start the restock
    //The restock will take 5 seconds to complete
    //When it is complete, the player will get rid of their current hand and draw a new hand.


}
