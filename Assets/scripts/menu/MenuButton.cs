using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;

    [SerializeField] GameManager manager;

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1){
				animator.SetBool ("pressed", true);
			}else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
                switch (menuButtonController.index)
                {
                    case 0: //New game
                        manager.CambiarEscena();
                        break;

                    case 1: //Options
                        manager.CambiarEscena();
                        break;

                    case 2://Quit
                        manager.Salir();
                        break;
                }
            }
		}else{
			animator.SetBool ("selected", false);
		}
    }
}
