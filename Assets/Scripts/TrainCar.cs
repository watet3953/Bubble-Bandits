using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCar : MonoBehaviour
{
    [SerializeField] public EnemyPoint[] enemyPoints;
    [SerializeField] private EnemyPoint playerPoint;

    public EnemyPoint checkSpots()
    {
        foreach (EnemyPoint p in enemyPoints)
        {
            if(!p.occupied)
            {
                p.occupied = true; //I DON'T KNOW HOW TO TURN THIS OFF AAAAHHHHHHHHHHH!1!!!!!!!!!!
                return p;
            }
        }
        return null;
    } 


    public bool hasPlayer()
    {
        if (playerPoint.occupied)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
