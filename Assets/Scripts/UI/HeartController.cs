using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour
{
    private int playerhealth;
    private int NumOfHearts;

    [SerializeField] private Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public PlayerController player;



    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        

    }


    void Update()
    {
        playerhealth = player.PlayerHP;
        NumOfHearts = player.PlayerTotalHP;

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < playerhealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if(i < NumOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
