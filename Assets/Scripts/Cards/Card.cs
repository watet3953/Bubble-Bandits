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

    [SerializeField] int layerMask = 3;
    private bool mouseOn = false;
    public Vector2 castPosition = Vector2.zero;

    [SerializeField] private CardAbility[] abilities;   // list of card's abilities

    private void Update()
    {
        mouseOn = PointInside(Input.mousePosition);

        print(currentState);

        if (mouseOn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentState = CardStates.Dragged;
            }

            else if (Input.GetMouseButtonUp(0))
            {
                currentState = CardStates.InEffect;
                mouseOn = false;
                UseAbility();
            }
        }

        if (currentState == CardStates.Dragged && Input.GetMouseButton(0))
        {
            transform.position = Input.mousePosition;
            
            Ray newRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(newRay, out hit, Mathf.Infinity, ~layerMask))
            {
                castPosition = hit.point;
                foreach (CardAbility ability in abilities)
                    ability.transform.position = castPosition;
            }
        }
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
        foreach (CardAbility ability in abilities)
        {
            ability.Activate();
        }
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

    /// <summary>
    /// Reset the position of the effect radius.
    /// </summary>
    public void ResetRadius()
    {
        foreach (CardAbility ability in abilities)
            ability.transform.position = transform.position;
    }
}

public class CardAbility : MonoBehaviour
{
    [SerializeField] protected Card cardMain;
    protected Vector2 effectPosition;
    [SerializeField] protected SphereCollider effectRadius;

    protected virtual void Start()
    {
        //cardMain = GetComponent<Card>();
    }

    /// <summary>
    /// Activate ability.
    /// </summary>
    public virtual void Activate() 
    {
        effectPosition = cardMain.castPosition;
        effectRadius.enabled = true;
    }
}