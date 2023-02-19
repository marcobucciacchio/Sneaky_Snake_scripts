using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;
	[SerializeField] public GameObject menuPanel;
	[SerializeField] public GameObject optionsPanel;
	[SerializeField] public GameObject levelSelectPanel;

	private bool menu = true;
	private bool opt = false;
	private bool level = false;

	void Start()
    {
		menu = true;
		opt = false;
		level = false;
	}

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool("selected", true);
			if(Input.GetAxis("Submit") == 1){
				animator.SetBool("pressed", true);
                if (gameObject.tag == "exit" )
                {
					Application.Quit();
                }else if(gameObject.tag == "options" && Input.GetKeyDown(KeyCode.Return))
                {
					menuPanel.gameObject.SetActive(opt);
					optionsPanel.gameObject.SetActive(menu);
					opt = !opt;
					menu = !menu;
                }else if(gameObject.tag == "levelselect" && Input.GetKeyDown(KeyCode.Return))
                {
					menuPanel.gameObject.SetActive(level);
					levelSelectPanel.gameObject.SetActive(menu);
					menu = !menu;
					level = !level;
				}
			}else if (animator.GetBool ("pressed")){
				animator.SetBool("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}else{
			animator.SetBool("selected", false);
		}
    }

}


