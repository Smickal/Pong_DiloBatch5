using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWall : MonoBehaviour
{
    [SerializeField]GameManager gameManager;
    [SerializeField]public PlayerControl player;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.name == "Ball")
        {
            player.IncrementScore();

            if(player.GetScore() < gameManager.maxScore)
            {
                other.gameObject.SendMessage("RestartGame", 2f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
