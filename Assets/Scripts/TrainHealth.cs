using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainHealth : MonoBehaviour
{
    // Update is called once per frame

    public Image thing;

    void Update()
    {
        thing.fillAmount = (float)GameManager.Instance.tHealth / (float)GameManager.Instance.maxTHealth;
    }
}
