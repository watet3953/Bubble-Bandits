using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTrain : CardAbility
{

    private int h = 5;

    public override void Activate()
    {
        base.Activate();


        GameManager.Instance.Heal(h);
        cardMain.currentState = Card.CardStates.Discarded;
        cardMain.hand.RemoveCard(cardMain.gameObject);
        Destroy(cardMain.gameObject);


    }
}
