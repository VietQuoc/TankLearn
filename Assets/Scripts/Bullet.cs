using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int transformX = 0;
    public int transformY = 1;
    public float speed = 2;
    public float damage = 5;
    public Animator anim;
    public GameObject parent;
    public bool isDestroying = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 10);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(transformX * speed * Time.deltaTime, transformY * speed * Time.deltaTime), Space.World);
    }

    public void destroyObject()
    {
        Destroy(gameObject);
    }

    public void OnAttack()
    {
        transformX = 0;
        transformY = 0;
        anim.SetBool("isCollided", true);
        isDestroying = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackGroundBox" || collision.gameObject.tag == "Bullet")
        {
            OnAttack();
        }
    }
}
