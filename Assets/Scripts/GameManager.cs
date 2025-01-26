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
            Destroy(this);
        }
        Instance = this;
        DontDestroyOnLoad(this);

        SwapToScene("Train Scene");
    }
    #endregion Singleton


    public void SwapToScene(string sn)
    {
        SceneManager.LoadScene(sceneName: sn);
    }

}
