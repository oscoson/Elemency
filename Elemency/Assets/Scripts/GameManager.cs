using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float deathTime = 5f;

    public void PlayerDeath()
    {
        SceneManager.LoadScene(0);
    }

}
