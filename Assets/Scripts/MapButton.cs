using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    /// <summary> The enemies present in this encounter. </summary>
    [SerializeField] public int[] enemies;
    /// <summary> Other buttons that are unlocked upon beating this button. </summary>
    [SerializeField] public List<MapButton> unlocks;
    //Has to be in the order of these enemies:
    //Short Cactus
    //Tall Cactus
    //Fish In Boot
    //Knife in Boots


    /// <summary> Ones that get locked out when you pick this one. </summary>
    [SerializeField] public List<MapButton> locks;

    public Color baseColor;
    public Color hoverColor;
    public Color pressedColor;  
    public Color disabledColor;

    private SpriteRenderer sr;

    private bool mouseHovering = false;

    public bool isEncounter;

    public void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void StartRound()
    {
        GameManager.Instance.encounter = enemies;
        MapManager.Instance.StartRound(this);
    }

    private void OnMouseDown()
    {
        if (!enabled) return;
        sr.color = pressedColor;
    }

    private void OnMouseUp()
    {
        if (!enabled) return;
        if (isEncounter)
            StartRound();
        else
        {
            // do some random event shit here.
            MapManager.Instance.FakeRound(this);
        }

        if (mouseHovering )
            sr.color = hoverColor;
        else
            sr.color = baseColor;

    }

    private void OnMouseEnter()
    {
        if (!enabled) return;
        sr.color = hoverColor;
        mouseHovering = true;
    }

    private void OnMouseExit()
    {
        if (!enabled) return;
        sr.color = baseColor;
        mouseHovering = false;
    }

    private void OnEnable() => sr.color = baseColor;

    private void OnDisable() => sr.color = disabledColor;
}
