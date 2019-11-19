using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {

    private bool gameIsPaused;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Start()
    {
        gameIsPaused =false;
    }
    void Update() {
        if (Input.GetKey(KeyCode.X)) {
            Debug.Log("Yepa");
            if (gameIsPaused) { Resume(); }
            else { Pause(); }
        }
    }
    public void Resume() {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1;
        gameIsPaused = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0;
        gameIsPaused = true;
    }

    public void QuitGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //Time.timeScale = 1;
        gameIsPaused = false;
    }
}
