using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : CardAbility
{
    //private Card hand;  // the player's card hand
    [SerializeField] int drawAmount = 1;    // the amount of cards that will be drawn

    public override void Activate()
    {
        // draw card

        cardMain.currentState = Card.CardStates.Discarded;
    }
}
