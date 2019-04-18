
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharControllerScript : MonoBehaviour
{
    public float maxSpeed = 10f;
    bool facingRight = true;
    Animator anim;
    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 7f;
    bool doubleJump = false;
    public int count;
    public int countKey;
    private bool gameEnd = false;
    private bool looser = false;
    public Text countText;
    public Text endText;
    public GameObject door;
    public GameObject doorOpened;
    public GameObject doorClosed;
    private GameMaster gm;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        endText.text = "";

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);
        if (grounded)
        {
            doubleJump = false;
        }
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
        if (!gameEnd)
        {
            float move = Input.GetAxis("Horizontal");
            anim.SetFloat("speed", Mathf.Abs(move));
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (move > 0 && !facingRight)
                Flip();
            else if (move < 0 & facingRight)
                Flip();
        }

    }
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R) && gameEnd)
        if (Input.GetKeyDown(KeyCode.R))
        {
            gm.restarted = true;
            Application.LoadLevel (Application.loadedLevel);
        }
        if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space) && !gameEnd)
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            collision.transform.gameObject.SetActive(false);
            count +=1 ;
            gm.countCoin = count;
            gm.collectedCoins.Add(collision.gameObject.name);
            SetCoinText();
        }
        if (collision.gameObject.tag == "key")
        {
            collision.transform.gameObject.SetActive(false);
            countKey += 1;
            gm.countKey = countKey;
            gm.collectedKeys.Add(collision.gameObject.name);
            door.GetComponent<SpriteRenderer>().sprite = doorOpened.GetComponent<SpriteRenderer>().sprite;
        }
        if (collision.gameObject.tag == "door" && countKey > 0)
        {
            endText.text = "You won! Press \"R\" to restart the level";
            gameEnd = true;
            anim.SetFloat("speed", 0);
        }
        if(collision.gameObject.tag == "walkEnemy")
        {
            endText.text = "You lost! Press \"Q\" to start from last checkpoint or \"R\" to restart";
            gameEnd = true;
            anim.SetFloat("speed", 0);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 5.0f);
        }
    }
    
    public void SetCoinText()
    {
        countText.text = "Coins:" + count.ToString();
    }
}