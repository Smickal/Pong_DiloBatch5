using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerControl player1,player2;
    public BallControl ball;

    private Rigidbody2D player1Rigid;

    private Rigidbody2D player2Rigid;

    private Rigidbody2D ballRigid;
    private CircleCollider2D ballCollider;

    public int maxScore;
    private bool isDebugWindowShown = false;

    public Trajectory trajectory;

    private void Start() {
        trajectory.enabled = false;
        player1Rigid = player1.GetComponent<Rigidbody2D>();
        player2Rigid = player2.GetComponent<Rigidbody2D>();
        ballRigid = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width/ 2 - 150 - 12, 20, 100, 100),"" + player1.GetScore());
        GUI.Label(new Rect(Screen.width/ 2 + 150 + 12, 20, 100, 100), ""+ player2.GetScore());

        if(GUI.Button(new Rect(Screen.width / 2 - 60, 35, 120, 53), "RESTART"))
        {
            player1.ResetScore();
            player2.ResetScore();

            ball.SendMessage("ResetBall", 0.5f, SendMessageOptions.RequireReceiver);
        }

        if(player1.GetScore() == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 -150, Screen.height / 2 -10
            ,2000,1000), "PLAYER ONE WIN");
            ball.SendMessage("RestartGame", null, SendMessageOptions.RequireReceiver);

        }else if(player2.GetScore() == maxScore)
        {
            GUI.Label(new Rect(Screen.width / 2 -150, Screen.height / 2 -10
            ,2000,1000), "PLAYER TWO WIN");
            ball.SendMessage("ResetBall", null, SendMessageOptions.RequireReceiver);
        }


        if(isDebugWindowShown)
        {
            Color oldColor = GUI.backgroundColor;
            GUI.backgroundColor = Color.red;

            float ballMass = ballRigid.mass;
            Vector2 ballVelocity = ballRigid.velocity;
            float ballSpeed = ballRigid.velocity.magnitude;
            Vector2 ballMomentum = ballMass * ballVelocity; 
            float ballFriction = ballCollider.friction;
 
            float impulsePlayer1X = player1.lastContactPoint.normalImpulse;
            float impulsePlayer1Y = player1.lastContactPoint.tangentImpulse;
            float impulsePlayer2X = player2.lastContactPoint.normalImpulse;
            float impulsePlayer2Y = player2.lastContactPoint.tangentImpulse;
 
            string debugText = 
                "Ball mass = " + ballMass+ "\n" +
                "Ball velocity = " + ballVelocity + "\n" +
                "Ball speed = " + ballSpeed + "\n" +
                "Ball momentum = " + ballMomentum + "\n" +
                "Ball friction = " + ballFriction + "\n" +
                "Last impulse from player 1 = (" + impulsePlayer1X + ", " + impulsePlayer1Y + ")\n" +
                "Last impulse from player 2 = (" + impulsePlayer2X + ", " + impulsePlayer2Y + ")\n";

            GUIStyle guiStyle = new GUIStyle(GUI.skin.textArea);
            guiStyle.alignment = TextAnchor.UpperCenter;
            GUI.TextArea(new Rect(Screen.width/2 - 200, Screen.height - 200, 400, 110), debugText, guiStyle);

            GUI.backgroundColor = oldColor;
            
        }


        if (GUI.Button(new Rect(Screen.width/2 - 60, Screen.height - 73, 120, 53), "TOGGLE\nDEBUG INFO"))
        {
            isDebugWindowShown = !isDebugWindowShown;
            trajectory.enabled = !trajectory.enabled;
        }
    }


}
