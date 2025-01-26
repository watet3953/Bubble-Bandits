using System.Collections.Generic;
using UnityEngine;
//using Unity.Mathematics;
using UnityEngine.Splines; // this guy is real

public class CardHand : MonoBehaviour
{
    public GameObject[] cardPrefabs;
    [HideInInspector] public List<GameObject> cards;
    [SerializeField] public GameObject[] badCards;
    private List<Vector3> desiredPos = new();
    private List<Quaternion> desiredRot = new();
    public SplineContainer sc; // also fake error

    public int maxHandSize;


    public void Start()
    {
        cards = new List<GameObject>();
        
        if (badCards.Length == 0)
        {
            RefillCards();
        }
        else
        {
            foreach (GameObject card in badCards)
            {
                AddCard(card);
            }
        }
        
        //badCards = null;
    }

    public void Update()
    {
        if (cards.Count > 0)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (((cards[i].transform.localPosition - desiredPos[i]).magnitude > 1f) &&
                    cards[i].GetComponent<Card>().currentState != Card.CardStates.Dragged)
                {
                    cards[i].transform.SetLocalPositionAndRotation(
                        Vector3.Lerp(cards[i].transform.localPosition, desiredPos[i], 0.1f),
                        Quaternion.RotateTowards(cards[i].transform.localRotation, desiredRot[i], 15f));

                }
            }
        }

        if (Input.GetMouseButtonDown(1))
            RefillCards();
    }

    public void AddCard(GameObject badcard)
    {
        if (cards.Count >= maxHandSize) return;
        GameObject card = Instantiate(badcard, transform);
        card.transform.SetParent(transform, true);
        cards.Add(card);
        desiredPos.Add(card.transform.position);
        desiredRot.Add(card.transform.rotation);
        UpdateCardPos();
    }

    public void RemoveCard(GameObject card)
    {
        cards.Remove(card);
        Destroy(card);
        UpdateCardPos();
    }

    public void UpdateCardPos()
    {
        if (cards.Count == 0) return; // give up pal, seriously.

        float cardSpacing = 1.0f / maxHandSize;
        float firstCardPos = 0.5f - (cards.Count - 2) * cardSpacing / 2;
        //print("First Card Pos:" + firstCardPos);
        Spline spline = sc.Spline; // fake error, just pretend it's not real :)
        for (int i = 0; i < cards.Count; i++)
        {
            float cardPos = firstCardPos + i * cardSpacing;
            //print(cardPos);
            Vector3 splinePos = spline.EvaluatePosition(cardPos);
            Vector3 forward = spline.EvaluateTangent(cardPos);
            Vector3 up = spline.EvaluateUpVector(cardPos);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            desiredPos[i] = splinePos;
            desiredRot[i] = rotation;
        }
    }

    public void DrawCard()
    {

    }

    public void RefillCards()
    {
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            RemoveCard(cards[i]);
        }

        cards.Clear();

        //foreach (GameObject card in badCards)
        //{
        //    AddCard(card);
        //}

        for (int i = 0; i < maxHandSize; i++)
        {
            GameObject newCard = Instantiate(cardPrefabs[Random.Range(0, cardPrefabs.Length)], transform);
            cards.Add(newCard);
        }
    }
}
