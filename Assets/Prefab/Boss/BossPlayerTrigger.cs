using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerTrigger : MonoBehaviour {

    public BossEnemyController enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.inAttackArea = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            enemy.inAttackArea = false;
        }
    }
}
