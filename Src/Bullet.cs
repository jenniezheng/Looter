using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public AudioClip fireSound;
    
    void Start()
    {
        AudioSource.PlayClipAtPoint(fireSound,transform.position, PlayerPrefsManager.getSfxVolume());
    }

    public void goStraightUp(float shotSpeed)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, shotSpeed);
    }

}
