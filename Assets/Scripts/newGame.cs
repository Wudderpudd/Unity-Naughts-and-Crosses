using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class newGame : MonoBehaviour
{
    public void startNewGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
