using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene1 : MonoBehaviour
{
    public void MoveToScene(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }

    public void EXIT()
    {
        Application.Quit();
    }

}
