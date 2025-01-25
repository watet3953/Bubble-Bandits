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
        Discarded
    }
    
    public CardStates currentState = CardStates.Werk;
    public float abilityCooldown = 2.0f;  // cooldown placed on other cards after ability
    public float exhaustCooldown = 1.0f;  // cooldown placed on other cards after exhaust
    private int trainHealthInc = 5;

    [SerializeField] private CardAbility[] abilities;   // list of card's abilities

    /// <summary>
    /// Use the card abilities.
    /// </summary>
    public void UseAbility()
    {
        currentState = CardStates.InEffect;
        foreach (CardAbility ability in abilities)
        {
            ability.Activate();
        }
        currentState = CardStates.Discarded;
    }

    /// <summary>
    /// Exhaust card to replenish train health.
    /// </summary>
    public void ExhaustCard()
    {
        currentState = CardStates.InEffect;
        TrainManager.Instance.tHealth += trainHealthInc;
        currentState = CardStates.Discarded;
    }
}

public class CardAbility : MonoBehaviour
{
    [SerializeField] protected Card cardMain;
    protected Vector2 effectPosition;
    protected Collider effectRadius;

    protected virtual void Start()
    {
        if (cardMain)
        {
            effectPosition = cardMain.transform.position;
            effectRadius = cardMain.GetComponent<Collider>();
        }
    }

    /// <summary>
    /// Activate ability.
    /// </summary>
    public virtual void Activate() 
    {
        effectPosition = cardMain.transform.position;
        effectRadius.enabled = true;
    }
}