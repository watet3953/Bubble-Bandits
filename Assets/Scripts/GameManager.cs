using System.Collections;
using System.Collections.Generic;
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
        DontDestroyOnLoad(gameObject);

        SwapToScene("Title Scene");
    }
    #endregion Singleton

    int curSceneIndex = 0;

    public void SwapToScene(string sn) => StartCoroutine(SwapToSceneInternal(sn));

    private IEnumerator SwapToSceneInternal(string sn)
    {
        yield return SceneManager.UnloadSceneAsync(curSceneIndex);
        yield return SceneManager.LoadSceneAsync(sn);
        curSceneIndex = SceneManager.GetSceneByName(sn).buildIndex;
    }

}
