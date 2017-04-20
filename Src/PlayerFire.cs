using UnityEngine;
using System.Collections;

public class PlayerFire : MonoBehaviour {
    public GameObject bullet;
    public int fireSpeed=4;
    static float fireRate = .4f;
    float lastFire = -5;
    bool left = true;
    public static int secondsFire = 0;
    Paddle pad;

    void Start()
    {
        pad = FindObjectOfType<Paddle>();
    }
    void Update()
    {
        if (secondsFire > 0)
        {
            if (Input.GetKeyDown("space") && Time.time - lastFire > fireRate)
            {
                InvokeRepeating("fire", .00001f, fireRate);
                lastFire = Time.time;
            }
            if (Input.GetKeyUp("space")) CancelInvoke();
        }
        else CancelInvoke();
    }

    void fire()
    {
        float adjustment = pad.GetComponent<Transform>().localScale.x/3.5f;
        if (left) adjustment = -adjustment;
     GameObject go = Instantiate(bullet, transform.position + new Vector3(adjustment,0,0), bullet.transform.rotation) as GameObject;
      go.GetComponent<Bullet>().goStraightUp(fireSpeed);
        left = !left;
    }

    public void fireStart()
    {
        secondsFire += 8;
        StartCoroutine(fireHelper());
    }

    IEnumerator fireHelper()
    {
        GetComponent<SpriteRenderer>().color = new Color32(244, 162, 88, 255);
        while (secondsFire > 0)
        {
            yield return new WaitForSeconds(1);//wait for time
        }
        GetComponent<SpriteRenderer>().color = Color.white;
    }


}
