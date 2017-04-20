using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
    public static float sizeDifference=.4f;
    static bool autoPlay=false;
    public Vector2 originalSize;
    const float paddleHeight = .5f;
    Ball ball;

	// Use this for initialization
	void Start () {
        originalSize = gameObject.transform.localScale;
	    ball = GameObject.FindObjectOfType<Ball>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (autoPlay) moveWithBall();
        else moveWithMouse();
    }

    void moveWithMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mousePos.x = Mathf.Clamp(mousePos.x, 0, 1);
        float xPos = Camera.main.ViewportToWorldPoint(mousePos).x;
        Vector2 paddlePos = new Vector2(xPos, paddleHeight);
        gameObject.transform.position = paddlePos;
    }

    void moveWithBall()
    {
        gameObject.transform.position = new Vector2(ball.transform.position.x, paddleHeight);
    }

    public void changeSize(bool increaseSize)
    {
       float sizeMulti;
       if (increaseSize) sizeMulti = 1 + sizeDifference;
       else sizeMulti = 1 /(1+sizeDifference);
        Vector2 tf = this.transform.localScale;
       this.transform.localScale= new Vector2(tf.x*sizeMulti, tf.y);//change only x
    }

    public void resetSize()
    {
        this.transform.localScale = originalSize;
    }

   
};

    
