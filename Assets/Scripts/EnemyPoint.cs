using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoint : MonoBehaviour
{
    public bool occupied = false;
    public GameObject e;

    public void Update()
    {
        if (e != null && e.TryGetComponent<Enemy>(out Enemy en) && en.GetComponent<Enemy>().health <= 0)
        {
            occupied = false;
            e = null;
        }
    }
}
