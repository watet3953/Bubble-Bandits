using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region Singleton
    public static MapManager Instance;

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

    public void StartRound(MapButton data)
    {
        // do shit that hides the UI.
        // reset the player.
        TrainManager.Instance.StartRound(data.enemies);
    }

}
