using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Two copies of GameManager");
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
    #endregion Singleton

    int curSceneIndex = 0;

    Vector3 pos = new Vector3(20,20,0);

    [SerializeField] public int[] encounter;
    public Queue<GameObject> encounterQueue = new Queue<GameObject>();

    public void Start()
    {
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void SwapToSceneWithCall(string sn, Action call) => StartCoroutine(SwapToSceneInternal(sn, call));

    public void SwapToScene(string sn) => StartCoroutine(SwapToSceneInternal(sn, null));


    private IEnumerator SwapToSceneInternal(string sn, Action? call)
    {
        yield return SceneManager.LoadSceneAsync(sn);
        if (SceneManager.GetSceneByBuildIndex(curSceneIndex).isLoaded)
            yield return SceneManager.UnloadSceneAsync(curSceneIndex);
        curSceneIndex = SceneManager.GetSceneByName(sn).buildIndex;
        if (call != null) call.Invoke();
        SetUpEncounter();
    }

    public void SetUpEncounter()
    {
        //Set up a queue that will randomly add the different enemies.
        int total = 0;

        foreach (int i in encounter)
        {
            total += i;
        }

        //Now it has to add the enemies to a queue by randomly choosing one of the ints in encounter.
        //depending on the int it will bring in a certain prefab to the queue and decrease the num by 1.
        //if the int is 0, then it will keep grabbing a new random number until it gets one.
        //When the length of the queue is the same as total, then it will stop.
        int rand;
        while (encounterQueue.Count < total)
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
                    encounterQueue.Enqueue(Resources.Load("Enemies/Spiky joe") as GameObject);
                    encounter[rand]--;
                    break;
                case 1:
                    encounterQueue.Enqueue(Resources.Load("Enemies/Prickle Pete") as GameObject);
                    encounter[rand]--;
                    break;
                case 2:
                    encounterQueue.Enqueue(Resources.Load("Enemies/Gil") as GameObject);
                    encounter[rand]--;
                    break;
                case 3:
                    encounterQueue.Enqueue(Resources.Load("Enemies/Sgt. Stab") as GameObject);
                    encounter[rand]--;
                    break;
            }
        }
        foreach(GameObject g in encounterQueue)
        {
            Instantiate(g, pos, Quaternion.identity);
            //g.transform.position = pos;
            pos.z += 1;
            g.SetActive(true);
        }
        StartCoroutine(StartSpawning());
    }


    IEnumerator StartSpawning(float wait = 0.1f, float less = 0.1f)
    {
        GameObject sporen;
        float timeLimit = UnityEngine.Random.Range(3,9);

        while (timeLimit > 0)
        {
            timeLimit -= less;
            yield return new WaitForSeconds(wait);
        }

        sporen = encounterQueue.Dequeue();

        if (sporen.TryGetComponent<Enemy>(out Enemy e))
        {
            e.s = true;
        }

        StartCoroutine(StartSpawning());

    }


}
