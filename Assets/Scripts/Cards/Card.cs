using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public CardHand hand;
    public CardStates currentState = CardStates.Werk;
    public float abilityCooldown = 2.0f;  // cooldown placed on other cards after ability
    public float exhaustCooldown = 1.0f;  // cooldown placed on other cards after exhaust
    private int trainHealthInc = 5;

    [SerializeField] int layerMask = 3;
    private bool mouseOn = false;
    public Vector2 castPosition = Vector2.zero;

    [SerializeField] private CardAbility[] abilities;   // list of card's abilities

    private void Start()
    {
        es = FindFirstObjectByType<EventSystem>();
        transform.parent.TryGetComponent<CardHand>(out hand);
    }

    private void Update()
    {
        mouseOn = PointInside(Input.mousePosition);

        if (mouseOn)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -5);
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
        } else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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

    private EventSystem es;

    private bool PointInside(Vector2 point)
    {
        PointerEventData ped = new PointerEventData(es);
        ped.position = point;
        List<RaycastResult> results = new List<RaycastResult>();
        GetComponentInParent<GraphicRaycaster>().Raycast(ped, results);
        return (results.Count > 0 && results[0].gameObject == gameObject);
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
        GameManager.Instance.tHealth += trainHealthInc;
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