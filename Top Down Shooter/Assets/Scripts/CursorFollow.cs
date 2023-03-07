using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorFollow : MonoBehaviour
{

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Game")
            Cursor.visible = false;
        else
            Cursor.visible = true;
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
