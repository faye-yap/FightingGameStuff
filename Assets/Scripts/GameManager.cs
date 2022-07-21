using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static GameManager _instance;
    public static GameManager Instance {
        get {return _instance;}
    }

   


    public string p1Character;
    public string p2Character;
    public int p1MaxHP; 
    public int p1CurrentHP;
    public Transform p1;
    private Vector3 p1StartPos;
    public Transform p1HPUI;
    public int p2MaxHP;
    public int p2CurrentHP;
    public Transform p2;
    private Vector3 p2StartPos;
    public Transform p2HPUI;
    [HideInInspector]
    public int frameNumber;
    private int timeRemaining = 99;
    public GameObject timer;
    private TextMeshProUGUI timerText;

    public int p1Wins = 0;
    public int p2Wins = 0;
    
    
    void Start()
    {
        Application.targetFrameRate = 60;
        frameNumber = 0;
        timerText = timer.GetComponent<TextMeshProUGUI>();
        p1StartPos = p1.transform.position;
        p2StartPos = p2.transform.position;

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
    public void TakeDamage(string player, int damage){
        if (player == "Player1"){
            p1CurrentHP -= damage;
          
            
            float newScale = (float) p1CurrentHP/ (float) p1MaxHP;
            if(newScale < 0) newScale = 0;
            p1HPUI.localScale = new Vector3(newScale,1,1);
        } else {
            p2CurrentHP -= damage;
            float newScale = p2CurrentHP/p2MaxHP;
            if(newScale < 0) newScale = 0;
            p2HPUI.localScale = new Vector3(newScale,1,1);
        }

        if(p1CurrentHP <= 0 || p2CurrentHP <= 0) EndRound();
        
    }

    public void EndRound(){
        //round win animation



        if(p1CurrentHP <= 0) p2Wins += 1;
        else if (p2CurrentHP <= 0) p1Wins += 1;


        //reset everything
        p1CurrentHP = p1MaxHP;
        p2CurrentHP = p2MaxHP;
        p1.transform.position = p1StartPos;
        p2.transform.position = p2StartPos;
        p1HPUI.localScale = new Vector3(1,1,1);
        p2HPUI.localScale = new Vector3(1,1,1);
        frameNumber = 0;
        timeRemaining = 99;
        timerText.text = timeRemaining.ToString();
        //super meter
       
    }
    
}
