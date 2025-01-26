using System.Collections.Generic;
using UnityEngine;
//using Unity.Mathematics;
using UnityEngine.Splines; // this guy is real

public class CardHand : MonoBehaviour
{
    public List<GameObject> cards;
    [SerializeField] public GameObject[] badCards;
    private List<Vector2> desiredPos = new();
    private List<Quaternion> desiredRot = new();
    public SplineContainer sc; // also fake error

    public bool isDeck = false;

    public int maxHandSize;


    public void Start()
    {
        cards = new List<GameObject>();
        
        if (isDeck )
        {
            maxHandSize = GameManager.Instance.deck.Count;
            foreach (GameObject card in GameManager.Instance.deck)
            {
                AddCard(card);
            }
            return;
        }

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
                Vector2 value = new Vector2(cards[i].transform.localPosition.x, cards[i].transform.localPosition.y);
                if (((value - desiredPos[i]).magnitude > 1f) &&
                    cards[i].GetComponent<Card>().currentState != Card.CardStates.Dragged)
                {
                    Vector2 shit = Vector2.Lerp(value, desiredPos[i], 0.1f);
                    cards[i].transform.SetLocalPositionAndRotation(
                        new Vector3(shit.x, shit.y, cards[i].transform.localPosition.z),
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
        desiredPos.Add((Vector2)card.transform.position);
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
            desiredPos[i] = (Vector2)splinePos;
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
            AddCard(GameManager.Instance.GetCardFromDeck());
        }
    }
}
