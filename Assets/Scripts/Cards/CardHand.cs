using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines; // this guy is real

public class CardHand : MonoBehaviour
{
    public GameObject cardPrefab;
    public List<GameObject> cards = new();
    private List<Vector3> desiredPos = new();
    private List<Quaternion> desiredRot = new();
    public SplineContainer sc; // also fake error

    public int maxHandSize;


    public void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            AddCard(Instantiate(cardPrefab, transform));
        }
    }

    public void Update()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.SetLocalPositionAndRotation(
                Vector3.Lerp(cards[i].transform.localPosition, desiredPos[i], 0.1f),
                Quaternion.RotateTowards(cards[i].transform.localRotation, desiredRot[i], 15f));
        }
    }

    public void AddCard(GameObject card)
    {
        if (cards.Count >= maxHandSize) return;
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
        float firstCardPos = 0.5f - (cards.Count - 1) * cardSpacing / 2;
        Spline spline = sc.Spline; // fake error, just pretend it's not real :)
        for (int i = 0; i < cards.Count; i++)
        {
            float cardPos = firstCardPos + i * cardSpacing - 0.0001f;
            Vector3 splinePos = spline.EvaluatePosition(cardPos);
            Vector3 forward = spline.EvaluateTangent(cardPos);
            Vector3 up = spline.EvaluateUpVector(cardPos);
            Quaternion rotation = Quaternion.LookRotation(up, Vector3.Cross(up, forward).normalized);
            desiredPos[i] = splinePos;
            desiredRot[i] = rotation;
        }
    }
}
