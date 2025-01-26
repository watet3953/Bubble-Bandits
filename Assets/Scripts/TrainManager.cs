using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.PlayerSettings;

public class TrainManager : MonoBehaviour
{




    //Forgive me for a bad kind of coding here :)
    [SerializeField] private TrainCar[] trainCars;

    [SerializeField] public int[] encounter;
    public List<GameObject> encounterList = new List<GameObject>();
    public List<GameObject> dead = new List<GameObject>();

    [SerializeField] private Transform pos;
    private int total;



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
        total = encounterList.Count;
    }

    private void Update()
    {

        if(dead.Count >= total && !ending)
        {
            StartCoroutine(EndDelay(3f));
        }

    }

    bool ending = false;

    public IEnumerator EndDelay(float time)
    {
        if (ending) yield break;
        ending = true;
        yield return new WaitForSeconds(time);
        EndEncounter();
    }


    //Get the four rooms and keep them in an array for reference.

    //Get the enemy spots from the rooms and hold that in a seperate array?
    //The spots should have a mini script that lets them know if they are occupied or not.

    /// <summary>
    /// 
    /// </summary>
    /// <param name="c"></param> the car that the enemy is currently in.
    /// <returns></returns>
    public EnemyPoint findOpenSpot(int c)
    {
        EnemyPoint point;

        point = trainCars[c].checkSpots();
        if(point != null)
        {
            return point;
        }
        return null;
    }


    public bool checkForPlayer(int t)
    {
        bool b = trainCars[t].hasPlayer();
        return b;
    }

    //For the Encounter Builder:
    //Needs the time that the encounter will last.
    //Once that timer is done then one of two things happen, the encounter ends OR it no longer spawns enemies and the encounter is done once all enemies are dead.
    //Enemies will spawn in after 10 seconds? and move their way to their starting room before entering the room. If the room they want to enter has no spots.
    //It will either reroll the room or wait.

    //For the Card Restock NEED THE CARDS READY FOR THIS.
    //when the player is in the engine, they can press some button to start the restock
    //The restock will take 5 seconds to complete
    //When it is complete, the player will get rid of their current hand and draw a new hand.

    /// <summary> Starts a round with the provided kinds of enemies. </summary>
    /// <param name="enemyCount"> An array of enemies to add for this round, index corresponds to enemy type (make sure to define this somewhere). </param>
    public void StartRound(int[] enemyCount)
    {
        // make sure to reset the train-side stuff like player, also probably reset the hand.
        encounter = enemyCount;
        SetUpEncounter();
    }


    public void EndEncounter()
    {
        GameManager.Instance.SwapToSceneWithCall("Map Scene", () => MapManager.Instance.EndRound());

    }



    public void SetUpEncounter()
    {
        //Set up a List that will randomly add the different enemies.
        int total = 0;

        foreach (int i in encounter)
        {
            total += i;
        }

        //Now it has to add the enemies to a List by randomly choosing one of the ints in encounter.
        //depending on the int it will bring in a certain prefab to the List and decrease the num by 1.
        //if the int is 0, then it will keep grabbing a new random number until it gets one.
        //When the length of the List is the same as total, then it will stop.
        int rand;
        while (encounterList.Count < total)
        {
            rand = UnityEngine.Random.Range(0, encounter.Length);

            while (encounter[rand] == 0)
            {
                rand = UnityEngine.Random.Range(0, encounter.Length);
            }

            //If it is NOT empty, get one of the enemies.
            switch (rand)
            {
                case 0:
                    encounterList.Add(Instantiate(Resources.Load("Enemies/Spiky joe") as GameObject, pos));
                    encounter[rand]--;
                    break;
                case 1:
                    encounterList.Add(Instantiate(Resources.Load("Enemies/Prickle Pete") as GameObject, pos));
                    encounter[rand]--;
                    break;
                case 2:
                    encounterList.Add(Instantiate(Resources.Load("Enemies/Gil") as GameObject, pos));
                    encounter[rand]--;
                    break;
                case 3:
                    encounterList.Add(Instantiate(Resources.Load("Enemies/Sgt. Stab") as GameObject, pos));
                    encounter[rand]--;
                    break;
            }
        }
        foreach (GameObject g in encounterList)
        {
            g.SetActive(true);
        }
        total = encounterList.Count;
        StartCoroutine(StartSpawning());
    }


    IEnumerator StartSpawning(float wait = 0.1f, float less = 0.1f)
    {
        GameObject sporen;
        float timeLimit = UnityEngine.Random.Range(3, 9);

        while (timeLimit > 0)
        {
            timeLimit -= less;
            yield return new WaitForSeconds(wait);
        }

        if (encounterList.Count <= 0) yield break;
        sporen = encounterList[0];
        encounterList.RemoveAt(0);

        if (sporen.TryGetComponent<Enemy>(out Enemy e))
        {
            print("fuck");
            e.s = true;
        }

        StartCoroutine(StartSpawning());

    }

}
