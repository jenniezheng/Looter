using UnityEngine;
using System.Collections;

public class Icon : MonoBehaviour {
    public Sprite[] sprites;
    public AudioClip collect;
    public GameObject gb;//ballorWall to instantiate
    public static float fallSpeed=3f;
    GamingManager gm;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallSpeed);
        gm = FindObjectOfType<GamingManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerConditions(other))
        {
            GamingManager.upScore(10);
            AudioSource.PlayClipAtPoint(collect, gameObject.transform.position, PlayerPrefsManager.getSfxVolume());
            if (this.tag == "Burger") Burger();//bigger paddle, less life
            else if (this.tag == "Salad") Salad();//smaller paddle, more life
            else if (this.tag == "Weed") Weed(); //increase speed but superpower
            else if (this.tag == "Wall") Wall();//blocks ball from getting to other side
            else if (this.tag == "Gun") Gun();//shoot blocks and ball with laser
            else if (this.tag == "Quarter") Quarter(); //5 extra balls
            Destroy(gameObject);
        }
    }
    bool triggerConditions(Collider2D other) {
        return (other.gameObject.tag == "Paddle");
    }

    void Burger()
    {
        gm.changeLife(-.5f);
        FindObjectOfType<Paddle>().changeSize(true);
    }

    void Salad()
    {
        gm.changeLife(.5f);//smaller paddle, more life
        FindObjectOfType<Paddle>().changeSize(false);
    }

    void Gun()
    {
        FindObjectOfType<PlayerFire>().fireStart();
    }

    void Wall()
    {
        Instantiate(gb, gb.transform.position, gb.transform.rotation);
    }

    void Weed()
    {
       FindObjectOfType<Ball>().OnWeed();//just call it for one ball
    }

    

    void Quarter()
    {
        for (int i = 0; i < 5; i++)
            Instantiate(gb, FindObjectOfType<Paddle>().transform.position, FindObjectOfType<Paddle>().transform.rotation);
            
    }
};
