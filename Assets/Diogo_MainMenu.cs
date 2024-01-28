using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Diogo_MainMenu : MonoBehaviour
{
   public void StartGame()
    {
        Debug.Log("sdaujihhf");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
