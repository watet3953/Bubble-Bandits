using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKRIPT2 : MonoBehaviour
{
    public GameObject deckScreen;
    //public GameObject closeButton;
    //public GameObject background;

    public void DeckOpen()
    {
        deckScreen.SetActive(true);
        //background.SetActive(true);
        //closeButton.SetActive(false);
    }

    public void DeckClose()
    {
        deckScreen.SetActive(false);
        //background.SetActive(false);
        //closeButton.SetActive(true);
    }
}
