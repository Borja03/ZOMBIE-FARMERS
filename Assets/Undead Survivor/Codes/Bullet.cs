using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int BulletNum;
    public int dmg;

    public float speed = 20f;

    public Rigidbody2D rb;

    void Start()
    {
        rb.linearVelocity = transform.right * speed;


    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
