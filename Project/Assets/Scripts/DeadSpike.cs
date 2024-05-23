using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSpike : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerAction player;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dead")
        {
            gameManager.health=0;
            gameManager.UIhealth[0].color = new Color(1, 0, 0, 0.4f);
            gameManager.UIhealth[1].color = new Color(1, 0, 0, 0.4f);
            gameManager.UIhealth[2].color = new Color(1, 0, 0, 0.4f);

            player.OnDie();
            Invoke("ResultUI", 0.5f);
        }
    }

    void ResultUI()
    {
        gameManager.gameover.SetActive(true);
        gameManager.game.text = "게임 오버";
    }
}
