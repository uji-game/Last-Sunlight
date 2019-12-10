using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public  bool gamePaused = false;
    public GameObject PauseMenuUI;
    private BarraDeVida scBarraVida;

    Scene x;

    void Start()
    {
        scBarraVida = FindObjectOfType<BarraDeVida>();

    }

    // Update is called once per frame
    void Update()
    {
        x = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Escape) && !scBarraVida.dead) {
            if (gamePaused) { Resume(); }
            else { Pause(); }        }


    }
    public void Pause() {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        if (x.name == "level2") { print("Si no funciono soy gay"); }

    }
    public void Resume() {

        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;

    }
    public void LoadMenu() 
    {
        Debug.Log("Loading menu...");
        gamePaused = false;
        Time.timeScale = 1f;


        SceneManager.LoadScene("MenuIncio");
        //SceneManager.LoadScene(7);
    }
    public void ExitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

