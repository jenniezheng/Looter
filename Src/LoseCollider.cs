using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball"&& Ball.numBalls == 1)
            FindObjectOfType<GamingManager>().ballDied();
        else Destroy(other.gameObject);
    }
};