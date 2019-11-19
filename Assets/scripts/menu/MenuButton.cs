using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;
    [SerializeField] string Scena;

    //[SerializeField] GameManager manager;

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{

            animator.SetBool ("selected", true);

			if(Input.GetAxis ("Submit") == 1){
				animator.SetBool ("pressed", true);
			}
            else if (animator.GetBool ("pressed"))
            {
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
                //Scene pantalla = SceneManager.GetActiveScene();

                /*string cargar = "controles";
                if (pantalla.name == "MenuIncio")
                {
                    cargar = "level_1";
                }
                else if (pantalla.name == "controles")
                {
                    cargar = "MenuIncio";
                }
                else if(pantalla.name == "level_1")
                {
                    cargar = "MenuIncio";
                }*/

                switch (menuButtonController.index)
                {
                    case 0: //New game
                        SceneManager.LoadScene("level_1");
                        break;

                    case 1: //Options
                        SceneManager.LoadScene("controles");
                        break;

                    case 2://Quit
                        Application.Quit();
                        break;
                }
            }
		}else{
			animator.SetBool ("selected", false);
		}
    }
}
