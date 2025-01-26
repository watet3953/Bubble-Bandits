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
            Debug.LogWarning("Two copies of MapManager");
            Destroy(gameObject);
            return;
        } 
        Instance = this;
        DontDestroyOnLoad(this);
    }
    #endregion Singleton

    private MapButton curRound = null;
    public MapButton[] startingEncounters;

    private Vector3 moveTo;
    public Transform initialMoveToTarget;
    public Camera camera;

    public Transform MapButtons;

    [SerializeField] private GameObject[] events;

    public void Start()
    {
        foreach(Transform t in MapButtons)
        {
            if (t.TryGetComponent<MapButton>(out MapButton button))
            {
                button.enabled = false;
            }
        }
        foreach(MapButton b in startingEncounters)
        {
            b.enabled = true;
        }
        SetCameraTarget(initialMoveToTarget.position);
            
    }

    private void SetCameraTarget(Vector3 target)
    {
        moveTo = target += new Vector3(0, 0, -10);
    }

    public void Update()
    {
        if (camera != null && (camera.transform.position - moveTo).magnitude > 1f)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, moveTo, 0.1f);

        }
    }

    public void StartRound(MapButton data)
    {
        curRound = data;
        // do shit that hides the UI.
        // reset the player.
        if (GameManager.Instance == null)
            Debug.LogError("YOU NEED A GAME MANAGER IN THE SCENE DUMBASS, GO TO THE MAIN MENU.");
        foreach (Transform b in MapButtons) b.gameObject.SetActive(false);
        GameManager.Instance.SwapToSceneWithCall("Train Scene", () => TrainManager.Instance.StartRound(data.enemies));
    }

    public void FakeRound(MapButton data)
    {
        curRound = data;

        int i = UnityEngine.Random.Range(0, events.Length);

        events[i].gameObject.SetActive(true);


        EndRound();
    }

    public void EndRound()
    {
        print("Round Ended");
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        // open the UI again
        Vector3 working_pos = Vector3.zero;
        int numNodes = 0;
        foreach (Transform b in MapButtons.GetComponentsInChildren(typeof(Transform), true)) 
            b.gameObject.SetActive(true);
        foreach (MapButton toLoad in curRound.unlocks)
        {
            if (curRound.locks.Contains(toLoad)) continue;
            toLoad.enabled = true;
            working_pos += toLoad.transform.position;
            numNodes++;
        }
        foreach (MapButton toLoad in curRound.locks) 
            toLoad.enabled = false; 
        working_pos /= numNodes;
        SetCameraTarget(working_pos);
        curRound.enabled = false;
        curRound.GetComponent<SpriteRenderer>().color = Color.white;
        curRound = null;
    }


}
