using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {
    public Transform ball;
    public float speed;
    public float paddleWidth = 10f;
    public float moveDistance = 10f;

    //private CapsuleCollider col;
    private Rigidbody body;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        //col = GetComponent<CapsuleCollider>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //only do this if the ball is moving towards the paddle and if it is a certain distance away
        if(ball.GetComponent<Rigidbody>().velocity.z < 0 && Mathf.Abs(ball.position.z - transform.position.z ) < moveDistance)
        {
            if(ball.transform.position.x  <  transform.position.x - (0.4 * paddleWidth))
            {
                body.velocity = new Vector3(-speed, 0, 0);
            }
            else if (ball.transform.position.x > transform.position.x + (0.4 * paddleWidth))
            {
                body.velocity = new Vector3(speed, 0, 0);
            }
        }   
        else
        {
            body.velocity = new Vector3(0, 0, 0);
            //else if the ball is anywhere above the paddle then dont move (maybe some idle moving)
        }        
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform == ball.transform)
        {
            Vector3 vel = new Vector3(ball.position.x - transform.position.x, 0, 2.0f);
            vel.Normalize();
            ball.GetComponent<Rigidbody>().velocity = vel;
        }
    }

    //on collision something just change the velocity to a fixed z and an x that 
    //is based on how far the ball is from the center and then the ball will normailize its speed and go on its merry way
}
