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

    public int tHealth; //Train Health
    public int bSupply; //Bubble Wrap Supply
    public int maxTHealth = 100; //The maxBSupply and maxTHealth are the same number

    public List<GameObject> deck;
    public GameObject[] cardPrefabs;
    public int startingDeckSize;


    public void Start()
    {
        curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        tHealth = maxTHealth;
        bSupply = maxTHealth;

        BuildNewDeck();
    }

    public void BuildNewDeck()
    {
        deck = new List<GameObject>();
        for (int i = 0; i < startingDeckSize; i++)
        {
            deck.Add(cardPrefabs[UnityEngine.Random.Range(0, cardPrefabs.Length)]);
        }

    }

    public void AddCardToDeck(GameObject g)
    {
        deck.Add(g);
    }

    public void RemoveCardFromDeck(GameObject g)
    {
        if (deck.Contains(g))
        {
            deck.Remove(g);
        }
    }

    public GameObject GetCardFromDeck()
    {
        int rand = UnityEngine.Random.Range(0, deck.Count - 1);
        print(rand + " " + deck.Count);
        return deck[rand];
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
    }




}
