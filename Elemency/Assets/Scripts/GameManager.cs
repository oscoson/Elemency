using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        Cursor.visible = false;
    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene(0);
    }

}
