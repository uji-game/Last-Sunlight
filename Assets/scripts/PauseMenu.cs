using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public  bool gamePaused = false;
    public GameObject PauseMenuUI;
    public GameObject ControlMenuUI;
    private BarraDeVida scBarraVida;

    public GameObject ResumeButton;
    public GameObject retPauseButton;
    public EventSystem EvS;

    Scene x;
    void Start()
    {
        scBarraVida = FindObjectOfType<BarraDeVida>();
    }

    // Update is called once per frame
    void Update()
    {
       /* Cursor.visible = false;
        Cursor.lockState= CursorLockMode.Locked;*/
        x = SceneManager.GetActiveScene();
        if (Input.GetKeyDown(KeyCode.Escape) && !scBarraVida.dead) {
            if (gamePaused) 
            {
                Resume(); 
            }

            else 
            {
                Pause();
            }
        }
    }

    public void ControlMenu() 
    {
        PauseMenuUI.SetActive(false);
        ControlMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(retPauseButton);
        retPauseButton = EventSystem.current.currentSelectedGameObject;
    }
    public void returnPauselMenu()
    {

        PauseMenuUI.SetActive(true);
        ControlMenuUI.SetActive(false);

        EventSystem.current.SetSelectedGameObject(ResumeButton);
        ResumeButton = EventSystem.current.currentSelectedGameObject;
    }

    public void Pause() {
        PauseMenuUI.SetActive(true);

        EventSystem.current.SetSelectedGameObject(ResumeButton);
        ResumeButton = EventSystem.current.currentSelectedGameObject;

        Time.timeScale = 0f;
        gamePaused = true;
        if (x.name == "level2") { print("Si no funciono soy bien pendejo"); }

    }
    public void Resume() {

        EventSystem.current.SetSelectedGameObject(null);

        PauseMenuUI.SetActive(false);
        ControlMenuUI.SetActive(false);


        Time.timeScale = 1f;
        gamePaused = false;

    }
    public void LoadMenu() 
    {
        Debug.Log("Loading menu...");
        gamePaused = false;
        Time.timeScale = 1f;


        SceneManager.LoadScene("MenuIncio");
    }
    public void ExitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}

