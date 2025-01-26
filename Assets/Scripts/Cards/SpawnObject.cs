using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : CardAbility
{
    [SerializeField] GameObject[] spawnObjects;    // the object that will be spawned

    public override void Activate()
    {
        foreach (GameObject obj in spawnObjects)
            Instantiate(obj, cardMain.transform.position, Quaternion.identity);

        cardMain.currentState = Card.CardStates.Discarded;
        cardMain.hand.RemoveCard(cardMain.gameObject);
        Destroy(cardMain.gameObject);
    }
}
