using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : CardAbility
{
    [SerializeField] private int damage = 5;    // damage dealt to enemies
    private List<Enemy> enemies;                // enemies affected

    public override void Activate()
    {
        base.Activate();

        foreach (Enemy enemy in enemies)
        {
            // deal damage
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy newEnemy))
            enemies.Add(newEnemy);
    }
}
