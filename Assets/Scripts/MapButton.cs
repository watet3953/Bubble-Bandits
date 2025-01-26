using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    /// <summary> The enemies present in this encounter. </summary>
    [SerializeField] public int[] enemies;
    /// <summary> Other buttons that are unlocked upon beating this button. </summary>
    [SerializeField] public MapButton[] unlocks;
    //Has to be in the order of these enemies:
    //Short Cactus
    //Tall Cactus
    //Fish In Boot
    //Knife in Boots


    /// <summary> Ones that get locked out when you pick this one. </summary>
    [SerializeField] public MapButton[] locks;

    public void StartRound()
    {
        MapManager.Instance.StartRound(this);
    }

}
