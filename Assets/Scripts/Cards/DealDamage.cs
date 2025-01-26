using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : CardAbility
{
    [SerializeField] private int damage = 5;    // damage dealt to enemies
    private List<Enemy> enemies = new();                // enemies affected

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
            foreach (Enemy enemy in enemies)
                enemy.TakeDamage(damage);
            cardMain.currentState = Card.CardStates.Discarded;
            cardMain.gameObject.SetActive(false);
        }
        else
        {
            cardMain.currentState = Card.CardStates.Werk;
        }

        cardMain.ResetRadius();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("touched");
        if (other.TryGetComponent<Enemy>(out Enemy newEnemy))
            enemies.Add(newEnemy);
    }
}
