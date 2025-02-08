using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    
        void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    void OnTriggerEnter2D (Collider2D collision){
         if (collision.gameObject.tag == "Enemy") 
        {
            gameObject.SetActive(false);
        }

    }
    
}
