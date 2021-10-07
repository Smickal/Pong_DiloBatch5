using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    Rigidbody2D ball;

    public float XInitialForce,yInitialForce;

    private Vector2 trajectoryOrigin;


    void ResetBall()
    {
        transform.position = Vector2.zero;
        ball.velocity = Vector2.zero;
    }

    void PushBall()
    {
        float randomDirection = Random.Range(0,2);

        if(randomDirection < 1f){
            ball.AddForce(new Vector2(-XInitialForce,yInitialForce));
        }
        else
        {
            ball.AddForce(new Vector2(XInitialForce,yInitialForce));
        }
    }


    void RestartGame()
    {
        ResetBall();
        Invoke("PushBall",2);
    }

    private void Start() 
    {
        ball = GetComponent<Rigidbody2D>();
        RestartGame();
        trajectoryOrigin = transform.position;
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        trajectoryOrigin = transform.position;
    }

    public Vector2 GetTrajectoryOrigin(){
        return trajectoryOrigin;
    }
}
