using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float speed = 1f; // movement speed
    public string charactorName = "Animal"; // name
    public float damage = 1; // damage on one shoot
    public float hp = 10; // total hp
    public float currentHp = 10; // current hp
    public Animator anim;

    private bool isDie = false; // is dying
    public float movingTime = 0f;
    private int transformX = 0; // direction x
    private int transformY = 0; // direction y

    // Audio
    private AudioSource audioSource;
    private void Awake()
    {
        anim.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetInteger("MoveState", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            RandomAction();
        }
    }

    private void RandomAction() {
        
            
        if (movingTime < Time.time) {
            float randomTime = Random.Range(1, 10);
            int randomAnimation = Random.Range(1, 9);
            movingTime = randomTime + Time.time;

            switch (randomAnimation) {
                case 1:
                    transformY = 1;
                    transformX = 0;
                    anim.SetInteger("MoveState", randomAnimation);
                    break;
                case 2:
                    transformY = 0;
                    transformX = 1;
                    anim.SetInteger("MoveState", randomAnimation);
                    break;
                case 3:
                    transformY = -1;
                    transformX = 0;
                    anim.SetInteger("MoveState", randomAnimation);
                    break;
                case 4:
                    transformY = 0;
                    transformX = -1;
                    anim.SetInteger("MoveState", randomAnimation);
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                    transformY = 0;
                    transformX = 0;
                    anim.SetInteger("MoveState", randomAnimation);
                    audioSource.Play();
                    break;
                default:
                    transformY = 0;
                    transformX = 0;
                    anim.SetInteger("MoveState", 0);
                    break;
            }
        }
        transform.Translate(new Vector2(transformX * speed, transformY * speed) * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnAttacked(collision);
    }

    private void OnAttacked(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && !collision.gameObject.GetComponent<Bullet>().isDestroying)
        {
            currentHp = currentHp - collision.gameObject.GetComponent<Bullet>().damage;
            collision.gameObject.GetComponent<Bullet>().OnAttack();
            if (currentHp <= 0)
            {
                anim.SetInteger("MoveState", 10);
                isDie = true;
                GetComponent<BoxCollider2D>().enabled = false;
                //audioSource.clip = audioClips[2];
                //audioSource.Play();
            }
            else
            {
                //audioSource.clip = audioClips[1];
                //audioSource.Play();
            }
        }
    }
    public void Die() {
        anim.SetInteger("MoveState", 10);
    }
}
