using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public float speed = 10.0f;
    public float yBoundary = 9f;

    private Rigidbody2D rigidBody2D;
    private int score;

    public ContactPoint2D lastContactPoint;

    public ContactPoint2D GetContactPoint2D(){
        return lastContactPoint;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = other.GetContact(0);
        }
    }

    private void Start() 
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        Movement();
        SetBoundaries();
        
    }

    void Movement()
    {
        Vector2 velocity = rigidBody2D.velocity;

        if(Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }else if(Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        else
        {
            velocity.y = 0f;
        }

        rigidBody2D.velocity = velocity;
    }

    void SetBoundaries()
    {
        Vector2 position = transform.position;

        if(position.y > yBoundary)
        {
            position.y = yBoundary;
        }else if(position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        transform.position = position;
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }

}
