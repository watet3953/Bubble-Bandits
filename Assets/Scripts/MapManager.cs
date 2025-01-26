using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MapManager : MonoBehaviour
{
    #region Singleton
    public static MapManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Two copies of MapManager");
            Destroy(this);
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
    #endregion Singleton

    private MapButton curRound = null;
    public MapButton[] startingEncounters;

    public void Start()
    {
        foreach(Transform t in transform)
        {
            if (t.TryGetComponent<Button>(out Button button))
            {
                button.interactable = false;
            }
        }
        foreach(MapButton b in startingEncounters)
        {
            b.GetComponent<Button>().interactable = true;
        }
            
    }

    public void StartRound(MapButton data)
    {
        curRound = data;
        // do shit that hides the UI.
        // reset the player.
        GameManager.Instance.SwapToSceneWithCall("Train Scene", () => TrainManager.Instance.StartRound(data.enemies));
    }

    public void EndRound()
    {
        // open the UI again
        foreach (MapButton toLoad in curRound.unlocks)
        {
            toLoad.GetComponent<Button>().interactable = true;
        }
        foreach (MapButton toLoad in curRound.locks)
        {
            toLoad.GetComponent<Button>().interactable = false;
        }
        curRound.GetComponent<Button>().enabled = false;
        curRound.enabled = false;
        curRound = null;
    }


}
