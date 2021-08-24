using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public float minX, maxX, minY, maxY;
    private Transform player;
    private float halfHeight;
    private float halfWidth;
    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;
        halfHeight = camera.orthographicSize;
        halfWidth = camera.aspect * halfHeight;
        minX = minX + halfWidth;
        maxX = maxX - halfWidth;
        minY = minY + halfHeight;
        maxY = maxY - halfHeight;
        player = GameObject.Find("MainPlayer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) {
            Vector3 temp = transform.position;
            temp.x = player.transform.position.x;
            temp.y = player.transform.position.y;
            if (temp.x < minX) {
                temp.x = minX;
            }
            if (temp.y < minY)
            {
                temp.y = minY;
            }
            if (temp.x > maxX)
            {
                temp.x = maxX;
            }
            if (temp.y > maxY)
            {
                temp.y = maxY;
            }
            transform.position = temp;
        }
    }
}
