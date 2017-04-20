using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
   
    public static int secondsStrong;
    public static float increase = 1.01f;
    public int startVelocity = 4;
    public bool started;
    public static int numBalls;
    float yPos;
    Paddle paddle;
    Rigidbody2D rb;
    int maxVelocity;//as in natural velocity

    // Use this for initialization
    void Start()
    {
        startVelocity = PlayerPrefsManager.getDifficulty(); //set to pref
        GetComponent<Collider2D>().enabled = false;
        numBalls++;
        maxVelocity = startVelocity * 4;
        paddle = GameObject.FindObjectOfType<Paddle>();
        yPos = paddle.transform.position.y +paddle.GetComponent<Renderer>().bounds.size.y/2 + gameObject.GetComponent<Renderer>().bounds.size.y / 2;
        rb = GetComponent<Rigidbody2D>();
        started = false;
    }

    // Use this for changes
    void Update()
    {
        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Collider2D>().enabled = true;
                rb.velocity = new Vector2(Random.Range(-1f,1f), startVelocity);
                started = true;
            }
            else
            {
                Vector2 moveWithPaddle = new Vector2(paddle.transform.position.x, yPos);
                gameObject.transform.position = moveWithPaddle;
                GetComponent<Transform>().position = moveWithPaddle;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        collisionMovement();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            BulletResponse();
            
        }
    }

    void collisionMovement()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 currentV = rb.velocity;
        //Stuck?
        float add = 2f;
        if (Mathf.Abs(rb.velocity.x) < .2 || Mathf.Abs(rb.velocity.y) < .2)
        {
            if (Random.Range(0, 1) == 1) add *= -1;
            if (Mathf.Abs(rb.velocity.x) < .2) rb.velocity = new Vector2(add, currentV.y);
            else rb.velocity = new Vector2(currentV.x, add);
        }
        //faster
        if (rb.velocity.SqrMagnitude() < maxVelocity) rb.velocity = new Vector2(rb.velocity.x * increase, rb.velocity.y * increase);
    }

    public void OnWeed()
    {
        secondsStrong += 10; // up strength
        StartCoroutine(WeedHelper());
    }

    IEnumerator WeedHelper()
    {
        float originalIncrease = increase;
        int originalMaxVelocity = maxVelocity;
        increase = 1.1f; //up acceleration 
        maxVelocity = startVelocity*10;//bigger max
        Ball[] balls = FindObjectsOfType<Ball>();
        for (int i = 0; i < balls.Length; i++)
            balls[i].GetComponent<Animator>().SetBool("Weed", true);
        while (secondsStrong > 0)
        {
            yield return new WaitForSeconds(1);//wait for time
        }
        for (int i = 0; i < balls.Length; i++)
            balls[i].GetComponent<Animator>().SetBool("Weed", false);
        increase = originalIncrease; //reset acceleration
        maxVelocity = originalMaxVelocity;
    }

    void OnDestroy()
    {
        numBalls--;
    }

    void BulletResponse()
    {
        StartCoroutine(bulletResponseHelper());
    }

    IEnumerator bulletResponseHelper()
    {
        GetComponent<Collider2D>().enabled = false;
        rb.velocity = new Vector2(0, 0);
        GetComponent<Animator>().SetBool("Dead", true);
        AudioSource audio = GetComponent<AudioSource>();
        audio.volume=PlayerPrefsManager.getSfxVolume();
        audio.Play();
        yield return new WaitForSeconds(1);
        //bullet destruction
        if (Ball.numBalls == 1)
        {
            GetComponent<Animator>().SetBool("Dead", false);
            FindObjectOfType<GamingManager>().ballShot();
        }
        else Destroy(gameObject);
    }
};
