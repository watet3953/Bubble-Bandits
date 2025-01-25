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

    public void StartRound()
    {
        MapManager.Instance.StartRound(this);
    }

}
