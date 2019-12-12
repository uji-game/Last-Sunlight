using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gamePaused = false;
    public GameObject deathMenuUI;

    private BarraDeVida script;
    private movController scMov;

    public float tAnim=3f;
    private void Start()
    {
        script = FindObjectOfType<BarraDeVida>();
        scMov = FindObjectOfType<movController>();

    }
    // Update is called once per frame
    void Update()
    {
        if (script.dead)
        {
            timeForAnim();
            
            
        }
    }
    public void Restart()
    {
        
        Scene lvl = SceneManager.GetActiveScene();
       
        SceneManager.LoadScene(lvl.name);
        deathMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;

        script.dead = false;


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

    void timeForAnim()
    {

        tAnim -= Time.deltaTime;
        if (tAnim <= 0)
        {
            deathMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gamePaused = true;

        }

    }
}
