using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetJump : MonoBehaviour
{
    //Resets player jump
    //Goes on collision gmOjb of player
    Player player;

    [SerializeField] private string Tag;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Resets jump
        //Tag will be the ground tag, or objects that reset jump
        if (collision.gameObject.CompareTag(Tag))
        {
            player.jumpCount = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //Ensures the jump resets
        if (collision.gameObject.CompareTag(Tag))
        {
            if(player.jumpCount > 0)
            {
                player.jumpCount = 0;
            }
        }
    }
}
