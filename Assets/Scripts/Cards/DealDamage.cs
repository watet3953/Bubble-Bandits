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

        //print(Physics.OverlapSphere(transform.position, 5));
        
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 5))
        {
            print(collider);
            if (collider.TryGetComponent<Enemy>(out Enemy newEnemy))
                enemies.Add(collider.GetComponent<Enemy>());
        }
        //print(enemies);

        foreach (Enemy enemy in enemies)
        {
            enemy.takeDamage(damage);
        }

        cardMain.currentState = Card.CardStates.Discarded;
    }

    private void OnTriggerEnter(Collider other)
    {
        print("touched");
        if (other.TryGetComponent<Enemy>(out Enemy newEnemy))
            enemies.Add(newEnemy);
    }
}
