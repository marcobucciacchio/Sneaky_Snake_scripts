using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public int totalLevel = 4;
    private int totalPage = 4;
    private LevelButton[] levelButtons;
    //private int pageItem = 1;
   //private int page = 0;
    public GameObject nextButton;
    public GameObject previousButton;
    public int unlockedLevel = 0;

    private void OnEnable()
    {
        levelButtons = GetComponentsInChildren<LevelButton>();
    }
    void Start()
    {
        // Refresh();
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + level + 1);
        //SceneManager.LoadScene((level + 1));
        //Refresh();
    }
}
   