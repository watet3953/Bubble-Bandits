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
    }




}
