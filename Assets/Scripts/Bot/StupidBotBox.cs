using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidBotBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnFaceWall(collision);
        gameObject.GetComponentInParent<StupidBot>().OnAttacked(collision);
    }

    private void OnFaceWall(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackGroundBox")
        {
            gameObject.GetComponentInParent<StupidBot>().movingTime = 0f;
        }
    }
}
