using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainManager : MonoBehaviour
{
    #region Singleton
    public static TrainManager Instance;

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


}
