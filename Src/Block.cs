using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    public GameObject icon;
    public ParticleSystem sparkle;
    public AudioClip hit;
    public Sprite[] sprites;
    public int maxHits = 3;
    public static int numBlocks = 0;
    int timesHit;
    float sfxVolume;

    // Use this for initialization
    void Start()
    {
        //if (gameObject.tag != "Unbreakable") numBlocks++;//count only breakable?
        numBlocks++;
        timesHit = 0;
        sfxVolume = PlayerPrefsManager.getSfxVolume();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Ball")
            collisionHandler();
         
          
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Destroy(col.gameObject);
            bulletCollisionHandler();
        }

    }

    void bulletCollisionHandler()//ignoreSecondsStrong, only balls are strong
    {
        playSound();
        if (gameObject.tag == "Unbreakable") return;
        timesHit++;
        if (timesHit < maxHits) changeSprite();
        else if (timesHit >= maxHits)
        {
            upScore();
            createParticles();
            createIcons();
            numBlocks--;
            if (numBlocks <= 0)
            {
                GamingManager.LoadLevel("Pass");//if this is last one
            }
            else Destroy(gameObject);
        }
    }

    void collisionHandler()
    {
        playSound();
        if (Ball.secondsStrong <= 0) { 
            if (gameObject.tag == "Unbreakable") return;
            timesHit++;
            if (timesHit < maxHits) changeSprite();
        }
        if(Ball.secondsStrong >0||timesHit>=maxHits)
        {
            //if (gameObject.tag != "Unbreakable") numBlocks--;
            numBlocks--;
            upScore();
            createParticles();
            createIcons();
            if (numBlocks <= 0)
                FindObjectOfType<GamingManager>().allDone();//slower win, will be called even after gameobject is destroyed
            Destroy(gameObject);//destroy gameObject either way
        }
    }


    void createParticles()
    {
        GameObject gbSpark = (GameObject)Instantiate(sparkle, this.transform.position, this.transform.rotation);
        gbSpark.GetComponent<ParticleSystem>().startColor = this.GetComponent<SpriteRenderer>().color;
    }

    void createIcons()
    {
        if (icon!=null) Instantiate(icon, this.transform.position, icon.transform.rotation);
    }


    void changeSprite()
    {
        if (sprites[timesHit] != null) GetComponent<SpriteRenderer>().sprite = sprites[timesHit];
        else Debug.LogError("Sprite is missing!");
    }

    void playSound()
    {
        if (gameObject.tag == "Unbreakable")
            AudioSource.PlayClipAtPoint(hit, gameObject.transform.position, sfxVolume);//dull,softer sound
        else
            AudioSource.PlayClipAtPoint(hit, gameObject.transform.position, sfxVolume);
    }

    void upScore()
    {
        string name = gameObject.tag;
        int score;
        if (name == "1D") score = 1;
        else if (name == "5D") score = 5;
        else if (name == "20D") score = 20;
        else score = 100;
        GamingManager.upScore(score);
    }

};
