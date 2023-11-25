using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;

    [SerializeField] private UnityEvent Win;
    [SerializeField] private UnityEvent defeat;
    private WinPlatform winPlatform;
    private PlayerController player;

    public GameObject pauseMenu; // Referencia al menu de pausa

    public bool onMenu = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        HideCursor();
        winPlatform = FindObjectOfType<WinPlatform>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si se presiona Escape, y el menú de pausa está activo, llamo a la funcion Continue()
            if (pauseMenu.activeInHierarchy)
            {
                Continue();
            }
            // Si no está activo, lo activo y paro el juego
            else
            {
                ShowCursor();
                onMenu = true;
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
            }
        }
        
        if (winPlatform.win)
        {
            Win.Invoke();
        }
        if (player.isDeath)
        {
            defeat.Invoke();
        }
    }
    public void Continue()
    {
        HideCursor();
        onMenu = false;
        Time.timeScale = 1f; // Vuelvo el tiempo de juego a la normalidad
        pauseMenu.SetActive(false); // Desactivo el menu de pausa
    }

    public void HideCursor()
    {
        Cursor.visible = false;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
    }

}
