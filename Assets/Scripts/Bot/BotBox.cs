using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnFaceWall(collision);
    }

    private void OnFaceWall(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackGroundBox")
        {
            gameObject.GetComponentInParent<Bot>().movingTime = 0f;
        }
    }
}
