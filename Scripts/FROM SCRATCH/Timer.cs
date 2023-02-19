using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float startTime = 0;
    public Text timerText;
    private bool finished = false;
    public Text resultScreenTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {  
        
        float t = Time.time - startTime;
        if (finished)
        {
            return;
        }
        
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = "Timer: " + minutes + ":" + seconds;
      
    }

    public void Finish()
    {
        float t = Time.time - startTime;
        finished = true;
        timerText.color = Color.green;

        if (t / 60 < 2)
            Score.scoreValue += ((int)t / 5) * 1000;
        if (t / 60 < 1)
            Score.scoreValue += ((int)t / 5) * 2000;

        resultScreenTime.text += " " + ((int)t/60).ToString() + ":" + (t%60).ToString("f2");
    }
}
