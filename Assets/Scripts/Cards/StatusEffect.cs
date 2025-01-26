using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private int slowMultiplier = 2;
    private List<Enemy> enemies = new();

    public override void Activate()
    {
        base.Activate();

        foreach (Collider collider in Physics.OverlapSphere(transform.position, effectRadius.radius))
        {
            print(collider);
            if (collider.TryGetComponent<Enemy>(out Enemy newEnemy))
                enemies.Add(collider.GetComponent<Enemy>());
        }

        if (enemies.Count > 0)
        {
            StartCoroutine(InitiateStatus());
        }
        else
        {
            cardMain.currentState = Card.CardStates.Werk;
            cardMain.ResetRadius();
        }
    }

    private IEnumerator InitiateStatus()
    {
        cardMain.gameObject.GetComponent<Image>().enabled = false;
        bool affected = false;

        if (!affected)
            switch (status)
            {
                case EffectType.Slow:
                    // slow enemy
                    foreach (Enemy enemy in enemies)
                        enemy.SlowDown(slowMultiplier);
                    affected = true;
                    break;
                case EffectType.Weak:
                    // reduce enemy damage
                    affected = true;
                    break;
                case EffectType.Frail:
                    // reduce enemy defence
                    affected = true;
                    break;
            }
        yield return new WaitForSeconds(statusDuration);
        effectRadius.enabled = false;
        cardMain.currentState = Card.CardStates.Discarded;
        cardMain.ResetRadius();
        cardMain.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy newEnemy))
            enemies.Add(newEnemy);
    }
}
