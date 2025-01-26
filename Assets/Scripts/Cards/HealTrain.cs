using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrain : CardAbility
{
 
    public override void Activate()
    {
        base.Activate();

        GameManager.Instance.tHealth += 5;
        cardMain.currentState = Card.CardStates.Discarded;
        cardMain.hand.RemoveCard(cardMain.gameObject);
        Destroy(cardMain.gameObject);


    }
}
