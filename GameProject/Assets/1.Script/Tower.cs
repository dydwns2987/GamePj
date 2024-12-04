using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.instance.towerHealth -= Time.deltaTime * 1;

            if (GameManager.instance.towerHealth < 0)
            {
                for (int index = 2; index < transform.childCount; index++)
                {
                    transform.GetChild(index).gameObject.SetActive(false);
                }

                GameManager.instance.GameOver();
            }
        }
    }
}
