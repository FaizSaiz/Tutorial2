using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    // This is for the challenge branch
     private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    private int lives;

    private int scoreValue = 0;

    public GameObject winTextObject;
    public GameObject loseTextObject;
    public TextMeshProUGUI livesText;

    public AudioSource musicSource;
    public AudioClip musicClipOne;

    Animator anim;
    private bool facingRight = true;
    


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        lives = 3;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        
        rd2d.AddForce(new Vector2(hozMovement * speed, 0f));

        if (Input.GetKey("escape"))
            {
                Application.Quit();
            }

 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if (scoreValue == 4)
        {
            transform.position = new Vector2(75f, 1f);
            lives = 3;
            livesText.text = "Lives: " + lives.ToString();
        }

         if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
        }
    

        if (collision.collider.tag == "Enemy")
        {
            lives = lives -1;
            Destroy(collision.collider.gameObject);
            SetLivesText();
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

    float hozMovement = Input.GetAxis("Horizontal");
    float vertMovement = Input.GetAxis("Vertical");

        if (facingRight == false && hozMovement > 0)
         {
             Flip();
         }
        else if (facingRight == true && hozMovement < 0)
         {
             Flip();
         }

    }

void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives == 0)
        {
            loseTextObject.SetActive(true);
            Destroy(this);
        }
    }

     private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                anim.SetInteger("State", 2);
            }
        }

        
    }
}
