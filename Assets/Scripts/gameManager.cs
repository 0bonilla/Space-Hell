using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gameManager : MonoBehaviour
{

    [SerializeField] private UnityEvent Win;
    [SerializeField] private UnityEvent defeat;
    private WinPlatform winPlatform;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        winPlatform = FindObjectOfType<WinPlatform>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (winPlatform.win)
        {
            Win.Invoke();
        }
        if (player.Defeat)
        {
            defeat.Invoke();
        }
    }
}
