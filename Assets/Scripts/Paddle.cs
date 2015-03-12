using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
    public Transform Ball;
    public float speed;

    private Rigidbody body;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	    //if the ball is to the left then move left at speed
        //if the ball is to the right then move right
        //else if the ball is anywhere above the paddle then dont move (maybe some idle moving)
	}

    //on collision something just change the velocity to a fixed z and an x that 
    //is based on how far the ball is from the center and then the ball will normailize its speed and go on its merry way
}
