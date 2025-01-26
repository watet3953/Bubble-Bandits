using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
    public enum CardStates
    {
        OnCooldown,
        Werk,
        Dragged,
        InEffect,
        Discarded
    }
    
    public CardStates currentState = CardStates.Werk;
    public float abilityCooldown = 2.0f;  // cooldown placed on other cards after ability
    public float exhaustCooldown = 1.0f;  // cooldown placed on other cards after exhaust
    private int trainHealthInc = 5;
    private bool mouseOn = false;

    [SerializeField] private CardAbility[] abilities;   // list of card's abilities

    private void Update()
    {
        mouseOn = PointInside(Input.mousePosition);

        //print(Input.GetMouseButtonUp(0));

        if (mouseOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentState = CardStates.Dragged;
            }

            else if (Input.GetMouseButtonUp(0))
            {
                currentState = CardStates.Werk;
                mouseOn = false;
            }
        }

        if (currentState == CardStates.Dragged && Input.GetMouseButton(0))
            transform.position = Input.mousePosition;
    }

    private bool PointInside(Vector2 point)
    {
        return point.x >= (transform.position.x - 50) && point.x <= (transform.position.x + 50)
            && point.y >= (transform.position.y - 75) && point.y <= (transform.position.y + 75);
    }

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
        cardMain = GetComponent<Card>();
        
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