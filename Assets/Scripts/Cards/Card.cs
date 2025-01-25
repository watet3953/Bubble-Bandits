using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardStates
    {
        OnCooldown,
        Werk,
        InEffect,
        Dicarded
    }
    
    public float abilityCooldown = 2.0f;  // cooldown placed on other cards after ability
    public float exhaustCooldown = 1.0f;  // cooldown placed on other cards after exhaust
    private int trainHealthInc = 5;

    [SerializeField] private CardAbility[] abilities;   // list of card's abilities

    public void UseAbility()
    {
        foreach (CardAbility ability in abilities)
        {
            ability.Activate();
        }
    }

    public void ExhaustCard()
    {
        TrainManager.Instance.tHealth += trainHealthInc;
    }
}

public class CardAbility : MonoBehaviour
{
    [SerializeField] protected Collider effectRadius;
    
    public virtual void Activate() 
    {
        effectRadius.enabled = true;
    }
}