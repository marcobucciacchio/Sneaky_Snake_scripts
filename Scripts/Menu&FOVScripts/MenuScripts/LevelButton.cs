using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    public LevelSelect menu;
    public int level = 0;
    private Button button;
    private RawImage image;
    public Animator transition;
    public float transitionTime = 6f;
    private void OnEnable()
    {
        button = GetComponent<Button>();
        image = GetComponent<RawImage>();
    }

    public void Setup(int level)
    {
        this.level = level;
        button.enabled = true;
    }

    public void OnClick()
    {        
        
        StartCoroutine(LoadLevel(level));


    }

    IEnumerator LoadLevel(int levelIndex)
    {

        //play Animation
        transition.SetTrigger("Start");
        //wait
        yield return new WaitForSeconds(transitionTime);
        //loadScene        
        menu.StartLevel(level);

    }
}
