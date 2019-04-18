using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerScript : MonoBehaviour
{
    public float bulSpeed = 3f;
    Transform left;
    Vector3 localScale;
    bool movingLeft = true;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveLeft();
        //Destroy(GameObject.Find("bullet"), 2);
    }

    void moveLeft()
    {
        localScale.x = -1;
        transform.localScale = localScale;
        rb.velocity = new Vector2(localScale.x * bulSpeed, rb.velocity.y);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
