using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpEvents : MonoBehaviour
{
    [SerializeField] private GameObject a; //option 1result
    [SerializeField] private GameObject b;
    [SerializeField] private GameObject c;  

    [SerializeField] private GameObject close;

    public void healTrain(int h)
    {
        GameManager.Instance.tHealth += h;
    }
    public void DamageTrain(int d)
    {
        GameManager.Instance.tHealth -= d;
    }

    public void ReSupply(int s) 
    {
        GameManager.Instance.bSupply += s;
    }

    public void LoseSupply(int l)
    {
        GameManager.Instance.bSupply -= l;
    }


    public void NewCard()
    {
        GameManager.Instance.AddCardToDeck(GameManager.Instance.cardPrefabs[UnityEngine.Random.Range(0, GameManager.Instance.cardPrefabs.Length)]);
    }

    public void LoseCard()
    {
        GameManager.Instance.RemoveCardFromDeck(GameManager.Instance.GetCardFromDeck());
    }

    public void Nothing()
    {

    }

    public void Option1()
    {
        a.SetActive(true);
        b.SetActive(false);
        c.SetActive(false);
    }

    public void Option2()
    {
        a.SetActive(false);
        b.SetActive(true);
        c.SetActive(false);
    }

    public void Option3()
    {
        a.SetActive(false);
        b.SetActive(false);
        c.SetActive(true);
    }

    public void Close()
    {
        a.SetActive(false);
        b.SetActive(false);
        c.SetActive(false);
        gameObject.SetActive(false);
    }

}
