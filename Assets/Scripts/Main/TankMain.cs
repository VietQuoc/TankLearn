using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMain : MonoBehaviour
{
    public float speed = 2.1f; // movement speed
    public string charactorName = "Main"; // name
    public float damage = 1; // damage on one shoot
    public float hp = 10; // total hp
    public float currentHp = 10; // current hp
    public Animator anim;
    public GameObject bullet; // input bullet Object
    public bool isPlayer = true; // is player or bot
    public float fireRate = 1.5F; // fire rate in second

    private float nextFire = 0.0F; // next fire second remain
    private int transformX = 0; // direction x
    private int transformY = 0; // direction y
    private int direction = 0; // direcetion number of 
    private bool isDie = false; // is dying
    private int[,] direction2D = new int[,] { { 5, 6, 7 }, { 4, 0, 0 }, { 3, 2, 1 } };
    private int[] rotationList = { 0, 315, 270, 225, 180, 135, 90, 45 };
    private int[,] directionList = { { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 } };

    // Audio
    private AudioSource audioSource;
    public AudioClip[] audioClips; // 0: shoot, 1: attacked, 2: destroy
    private void Awake()
    {
        anim.GetComponent<Animator>();
        bullet.GetComponent<Bullet>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.SetInteger("tankState", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie) {
            if (isPlayer) OnKeyboardPress();
            ChangeRotation();
            ChangeAnimation();
        }
    }

    void OnKeyboardPress() {
        Vector3 totalMovement = Vector3.zero;
        


        if (Input.GetKey(KeyCode.UpArrow))
        {
            transformY = 1;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            transformY = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transformY = -1;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            transformY = 0;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transformX = 1;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            transformX = 0;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transformX = - 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transformX = 0;
        }

        transform.Translate(new Vector2(transformX * speed, transformY * speed) * Time.deltaTime, Space.World);

        if (Input.GetKeyUp(KeyCode.A) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            OnAttack();
        }
    }

    void ChangeRotation() {
        if (!((transformX == transformY) && transformX == 0)) {
            direction = direction2D[transformX + 1, transformY + 1];
            transform.rotation = Quaternion.Euler(0, 0, rotationList[direction]);
        }
    }

    void ChangeAnimation() {
        if ((transformX == transformY) && transformX == 0)
        {
            anim.SetInteger("tankState", 0);
        }
        else {
            anim.SetInteger("tankState", 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnAttacked(collision);
    }

    private void OnAttacked(Collider2D collision) {
        if (collision.gameObject.tag == "Bullet" && collision.gameObject.GetComponent<Bullet>().parent != gameObject)
        {
            currentHp = currentHp - collision.gameObject.GetComponent<Bullet>().damage;
            collision.gameObject.GetComponent<Bullet>().OnAttack();
            if (currentHp <= 0)
            {
                anim.SetInteger("tankState", 2);
                isDie = true;
                audioSource.clip = audioClips[2];
                audioSource.Play();
            }
            else {
                audioSource.clip = audioClips[1];
                audioSource.Play();
            }
        }
    }

    private void OnAttack()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        GameObject instanceBullet = Instantiate(bullet, new Vector3(0, 0, 0), Quaternion.identity);
        int directionX = directionList[direction, 0];
        int directionY = directionList[direction, 1];
        instanceBullet.GetComponent<Bullet>().transform.localPosition = new Vector3(
            gameObject.transform.position.x + directionX*0.5f,
            gameObject.transform.position.y + directionY*0.5f,
            gameObject.transform.position.z);
        instanceBullet.GetComponent<Bullet>().transformX = directionX;
        instanceBullet.GetComponent<Bullet>().transformY = directionY;
        instanceBullet.GetComponent<Bullet>().parent = gameObject;
        instanceBullet.GetComponent<Bullet>().speed = speed*2;
    }
    public void destroyObject()
    {
        Destroy(gameObject);
    }
}
