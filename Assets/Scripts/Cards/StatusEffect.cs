using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : CardAbility
{
    enum EffectType // types of status effects
    {
        Slow,
        Weak,
        Frail
    }

    [SerializeField] private EffectType status; // effect type of the card
    [SerializeField] private float statusDuration = 2.0f;    // how long the effect lasts
    private List<Enemy> enemies;

    public override void Activate()
    {
        base.Activate();

        StartCoroutine(InitiateStatus());
    }

    private IEnumerator InitiateStatus()
    {
        switch (status)
        {
            case EffectType.Slow:
                // slow enemy
                break;
            case EffectType.Weak:
                // reduce enemy damage
                break;
            case EffectType.Frail:
                // reduce enemy defence
                break;
        }
        yield return new WaitForSeconds(statusDuration);
        effectRadius.enabled = false;
        cardMain.currentState = Card.CardStates.Discarded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy newEnemy))
            enemies.Add(newEnemy);
    }
}
