using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COOL : MonoBehaviour
{
    public GameObject OPTIONS;

    public void SETTINGOPEN()
    {
        OPTIONS.SetActive(true);
    }

    public void SETTINGCLOSE() { OPTIONS.SetActive(false); }


}
