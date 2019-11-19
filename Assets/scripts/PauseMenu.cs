using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public  bool gamePaused = false;
    public GameObject PauseMenuUI;
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePaused) { Resume(); }
            else { Pause(); }
        }


    }
    public void Pause() {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }
    public void Resume() {

        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;

    }
    public void LoadMenu() 
    {
        Debug.Log("Loading menu...");
        SceneManager.LoadScene("MenuIncio"); 
    }
    public void ExitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

