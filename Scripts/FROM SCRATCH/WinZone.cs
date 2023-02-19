using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    public GameObject player;
    public Transform playerpos;
    public GameObject exitLevelUI;
    public GameObject ui;
    public GameObject minimap;
    public GameObject resultUI;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerpos.position);
        if (distance < 4f)
        {
            exitLevelUI.SetActive(true);
            if (KeyScore.numKeys > 0)
            {
                if (KeyScore.numKeys == 1 && Input.GetKeyDown(KeyCode.Space))
                {
                    exit();
                    Score.scoreValue += 1000;
                }
                else if (KeyScore.numKeys == 2 && Input.GetKeyDown(KeyCode.Space))
                {
                    exit();
                    Score.scoreValue += 2000;
                }
                else if (KeyScore.numKeys == 3 && Input.GetKeyDown(KeyCode.Space))
                {
                    exit();
                    Score.scoreValue += 5000;
                }
                else if (KeyScore.numKeys == 4 && Input.GetKeyDown(KeyCode.Space))
                {
                    exit();
                    Score.scoreValue += 10000;
                }
                else if (KeyScore.numKeys == 5 && Input.GetKeyDown(KeyCode.Space))
                {
                    exit();
                    Score.scoreValue += 50000;
                }
            }

        }else
        {
            exitLevelUI.SetActive(false);
        }
        if (resultUI.activeSelf)
        {
            exitLevelUI.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(0);

            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(1);
                Score.scoreValue = 0;

            }
        }
    }
    void exit()
    {
        player.SendMessage("Finish");
        exitLevelUI.SetActive(false);
        ui.SetActive(false);
        minimap.SetActive(false);
        resultUI.SetActive(true);
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 4);
        player.GetComponent<PlayerMovement>().speed = 0;
    }

}
