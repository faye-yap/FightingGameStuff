using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //Temp, should be passed from character select screen
    public string p1Character = "Knight";
    public string p2Character = "Knight";
    public int p1HP;
    public int p2HP;
    [HideInInspector]
    public int frameNumber;
    private int timeRemaining = 99;
    public GameObject timer;
    private TextMeshProUGUI timerText;
    void Start()
    {
        Application.targetFrameRate = 60;
        frameNumber = 0;
        timerText = timer.GetComponent<TextMeshProUGUI>();
        DontDestroyOnLoad(gameObject);

    }

    void Update(){
        frameNumber += 1;
        if (frameNumber % 60 == 0){
            timeRemaining -= 1;
            //Debug.Log(timerText);
            timerText.text = timeRemaining.ToString();

            //todo: if timer == 0, end the round
        }
    }

    // Update is called once per frame
    
}
