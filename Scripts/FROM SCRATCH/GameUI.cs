using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject gameLoseUI;
    public GameObject gameWinUI;
    //public GameObject resultScreenUI;
    bool gameIsOver;
    bool gameLose;
    bool gameWin;
    bool levelFinished;

    public Text timerText;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        Guard.OnGuardHasSpottedPlayer += ShowGameLoseUI;
        SpotlightScript.OnGuardHasSpottedPlayer += ShowGameLoseUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameLose)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().speed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        } 
    }
    void ShowGameLoseUI()
    {
        GameObject.Find("Player").GetComponent<Animator>().SetBool("isDead", true);
        gameLoseUI.SetActive(true);
        gameLose = true;
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
        //OnGameOver(gameLoseUI);
    }

    void OnGameOver(GameObject gameOverUI)
    {
        gameOverUI.SetActive(true);
        gameIsOver = true;
        Guard.OnGuardHasSpottedPlayer -= ShowGameLoseUI;
    }
}
