using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController p1Controller;
    public PlayerController p2Controller;
    public OptionConstants optionConstants;
    public PlayerSelectConstants PlayerSelectConstants;
    public PlayerSelectConstants.CharacterSelection p1Character;
    public PlayerSelectConstants.CharacterSelection p2Character;
    public PauseMenuController pauseMenuController;
    [HideInInspector]
    public int p1MaxHP;
    [HideInInspector] 
    public int p1CurrentHP;
    [HideInInspector]
    public Dictionary<string,int> meter = new Dictionary<string, int>(){{"Player1", 0},{ "Player2", 0}};
    public Transform p1;
    private Vector3 p1StartPos;
    public Transform p1HPUI;
    public Transform p1MeterUI;
    public Transform p1MeterNumberUI;
    public TextMeshProUGUI p1ComboCounter;
    private TextMeshProUGUI p1MeterNumber;
    [HideInInspector]
    public int p2MaxHP;
    [HideInInspector]
    public int p2CurrentHP;
    [HideInInspector]
    
    public Transform p2;
    private Vector3 p2StartPos;
    public Transform p2HPUI;
    public Transform p2MeterUI;
    public Transform p2MeterNumberUI;
    public TextMeshProUGUI p2ComboCounter;
    private TextMeshProUGUI p2MeterNumber;
    [HideInInspector]
    public int frameNumber;
    private int timeRemaining = 99;
    public GameObject timer;
    private int preTimeRemaining = 3;
    public GameObject preTimerObj;
    private GameObject preTimer;
    private TextMeshProUGUI timerText;
    private TextMeshProUGUI preTimerText;
    public Dictionary<string,int> comboCounter = new Dictionary<string, int> {{"Player1", 0},{"Player2",0}};
    
    public int firstTo = 2;
    private int p1NumWins;
    private int p2NumWins;
    public WinManager p1Wins;
    public WinManager p2Wins;
    public GameObject gameFinishedPrefab;

    

    
    
    void Start()
    {
        Application.targetFrameRate = 60;
        frameNumber = 0;
        timerText = timer.GetComponent<TextMeshProUGUI>();
        timeRemaining = optionConstants.TimeLimit;
        timerText.text = optionConstants.TimeLimit.ToString();
        preTimer = preTimerObj.transform.Find("Timer").gameObject.transform.Find("TimerText").gameObject;
        preTimerText = preTimer.GetComponent<TextMeshProUGUI>();
        p1StartPos = p1.transform.position;
        p2StartPos = p2.transform.position;
        p1Character = PlayerSelectConstants.p1Character;
        p2Character = PlayerSelectConstants.p2Character;
        p1MeterNumber = p1MeterNumberUI.GetComponent<TextMeshProUGUI>();
        p2MeterNumber = p2MeterNumberUI.GetComponent<TextMeshProUGUI>();
        p1Controller = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>();
        p2Controller = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>();
        StartCoroutine(PreRoundTimer());
    }

    void Update(){
        // use timescale to determine if game is paused
        if(pauseMenuController.GameIsPaused){
            return;
        }
        frameNumber += 1;
        if (frameNumber % 60 == 0){
            if(timeRemaining == 0){
                EndRound();
            }
            timeRemaining -= 1;
            //Debug.Log(timerText);
            timerText.text = timeRemaining.ToString();

            
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
            float newScale = (float) p2CurrentHP/ (float) p2MaxHP;
            if(newScale < 0) newScale = 0;
            p2HPUI.localScale = new Vector3(newScale,1,1);
        }

        if(p1CurrentHP <= 0 || p2CurrentHP <= 0) EndRound();
        
    }

    public void GainMeter(string player, int meterGain){
        if (player == "Player1"){
            meter["Player1"] += meterGain;
            if(meter["Player1"] > 100) meter["Player1"] = 100;

            int newMeterNumber = meter["Player1"]/25;
            float newScale = (float) (meter["Player1"]%25)/ (float) 25.0f;
            if(meter["Player1"] == 100) newScale = 1;
            p1MeterNumber.text = newMeterNumber.ToString();            
            p1MeterUI.localScale = new Vector3(newScale,1,1);
        } else {
            meter["Player2"] += meterGain;
            if(meter["Player2"] > 100) meter["Player2"] = 100;

            int newMeterNumber = meter["Player2"]/25;
            float newScale = (float) (meter["Player2"] % 25)/ (float) 25.0f;
            if(meter["Player2"] == 100) newScale = 1;
            p2MeterNumber.text = newMeterNumber.ToString();            
            p2MeterUI.localScale = new Vector3(newScale,1,1);
        }
    }

    public void UseMeter(string player, int meterLoss){
        if (player == "Player1"){
            meter["Player1"] -= meterLoss;
            if(meter["Player1"] < 0) meter["Player1"] = 0;

            int newMeterNumber = meter["Player1"]/25;
            float newScale = (float) (meter["Player1"]%25)/ (float) 25.0f;
            
            p1MeterNumber.text = newMeterNumber.ToString();            
            p1MeterUI.localScale = new Vector3(newScale,1,1);
        } else {
            meter["Player2"] -= meterLoss;
            if(meter["Player2"] < 0) meter["Player2"] = 0;

            int newMeterNumber = meter["Player2"]/25;
            float newScale = (float) (meter["Player2"] % 25)/ (float) 25.0f;
            
            p2MeterNumber.text = newMeterNumber.ToString();            
            p2MeterUI.localScale = new Vector3(newScale,1,1);
        }
    }

    public void EndRound(){
        //round win animation



        if(p1CurrentHP <= 0) {
            p2Wins.UpdateScore(p2NumWins);
            p2NumWins += 1;
            
        }else if (p2CurrentHP <= 0) {
            p1Wins.UpdateScore(p1NumWins);
            p1NumWins += 1;
        }

        //reset everything
        p1CurrentHP = p1MaxHP;
        p2CurrentHP = p2MaxHP;
        p1.transform.position = p1StartPos;
        p2.transform.position = p2StartPos;
        p1HPUI.localScale = new Vector3(1,1,1);
        p2HPUI.localScale = new Vector3(1,1,1);
        meter["Player1"] = 0;
        meter["Player2"] = 0;
        p1MeterUI.localScale = new Vector3(0,1,1);
        p2MeterUI.localScale = new Vector3(0,1,1);
        p1MeterNumber.text = "0";
        p2MeterNumber.text = "0"; 
        frameNumber = 0;
        timeRemaining = optionConstants.TimeLimit;
        timerText.text = optionConstants.TimeLimit.ToString();
        if(p1NumWins == firstTo || p2NumWins == firstTo){
            GameObject gameFinishedUI = Instantiate(gameFinishedPrefab,gameFinishedPrefab.transform.position,gameFinishedPrefab.transform.rotation);
            gameFinishedUI.transform.SetParent(timer.transform,false);
            StartCoroutine(EndMatch());
            return;
        }
        StartCoroutine(PreRoundTimer());
        //super meter
       
    }

    private IEnumerator PreRoundTimer(){
        preTimerObj.SetActive(true);
        Time.timeScale = 0f;
        pauseMenuController.GameIsPaused = true;
        for (int i = 0; i < preTimeRemaining; i++){
            preTimerText.text = (preTimeRemaining-i).ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        preTimerObj.SetActive(false);
        Time.timeScale = 1f;
        pauseMenuController.GameIsPaused = false;
    }

    private IEnumerator EndMatch(){
        Time.timeScale = 0f;
        pauseMenuController.GameIsPaused = true;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        pauseMenuController.GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void UpdateComboCounter(string player){
        comboCounter[player] += 1;
        Debug.Log(comboCounter[player]);
        Debug.Log(p1Controller.hasBeenHit);
        //update text
        if(player == "Player1"){
            p1ComboCounter.text = comboCounter[player].ToString() + " Hit";
            if(comboCounter[player] > 1){
                p1ComboCounter.alpha = 1;
            }
        } else if (player == "Player2"){
            p2ComboCounter.text = comboCounter[player].ToString() + " Hit";
            if(comboCounter[player] > 1){
                p2ComboCounter.alpha = 1;
            }
        }

        
        
    }
    public void ResetComboCounter(string player){
        comboCounter[player] = 0;
        
        //hide text
        if(player == "Player1"){
                    

            StartCoroutine(FadeP1ComboCounter());
            
            
            
        } else {
            //Debug.Log("b");
            StartCoroutine(FadeP2ComboCounter());
            

        }
    }

    IEnumerator FadeP1ComboCounter(){
        
    
        for (float a = p1ComboCounter.alpha; a >= -0.5f; a-= 0.05f){
            
            p1ComboCounter.alpha = a; 
            yield return null;
        }
        p1ComboCounter.text = "0 Hit";
       
        
    }
    IEnumerator FadeP2ComboCounter(){
        
        for (float a = p2ComboCounter.alpha; a >= -0.5f; a-= 0.05f){
            
            p2ComboCounter.alpha = a; 
            yield return null;
        }
        p2ComboCounter.text = "0 Hit";
       
        
    }
}
