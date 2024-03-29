using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnFaceWall(collision);
    }

    private void OnFaceWall(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackGroundBox")
        {
            gameObject.GetComponentInParent<Animal>().movingTime = 0f;
        }
    }
}
