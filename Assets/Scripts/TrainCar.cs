using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCar : MonoBehaviour
{
    [SerializeField] private EnemyPoint[] enemyPoints;


    public Transform checkSpots()
    {
        foreach (EnemyPoint p in enemyPoints)
        {
            if(!p.occupied)
            {
                return p.transform;
            }
        }
        return null;
    } 

}
