using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCanvas : MonoBehaviour
{
    #region Singleton
    public static MapCanvas Instance;

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
}
