using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotListenEnemy : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        OnFaceEnemy(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnLeaveEnemy(collision);
    }

    private void OnFaceEnemy(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponentInParent<Bot>().isFighting = true;
        }
    }

    private void OnLeaveEnemy(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.GetComponentInParent<Bot>().isFighting = false;
        }
    }
}
