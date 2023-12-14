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
    public GameObject configurationMenu;

    public bool onMenu = false;

    //Command
    private Queue<ICommand> _events = new Queue<ICommand>();
    private List<ICommand> _doneEvents = new List<ICommand>();
    private const int MAX_UNDOS = 5000;

    public void AddEvents(ICommand command) => _events.Enqueue(command);

    public void UndoEvents()
    {
        if (_doneEvents.Count == 0) return;

        int lastIndex = _doneEvents.Count - 1;
        _doneEvents[lastIndex].Undo();
        _doneEvents.RemoveAt(lastIndex);
    }

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
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Si se presiona Escape, y el menú de pausa está activo, llamo a la funcion Continue()
            if (pauseMenu.activeInHierarchy || configurationMenu.activeInHierarchy)
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

        while (_events.Count > 0)
        {
            // aplicar logica necesaria
            var command = _events.Dequeue();

            _doneEvents.Add(command);
            if (_doneEvents.Count > MAX_UNDOS) _doneEvents.RemoveAt(0);

            command.Do();
        }

        if (Input.GetKey(KeyCode.Z)) UndoEvents();

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
        configurationMenu.SetActive(false);
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
