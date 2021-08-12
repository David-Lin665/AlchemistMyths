using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck_stand_ver : MonoBehaviour
{
    private Enemy_behaviour_stand_ver enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_behaviour_stand_ver>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}
