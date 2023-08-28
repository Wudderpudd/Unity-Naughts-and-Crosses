using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ToMenu : MonoBehaviour
{
    public void toMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
