using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerPanel : MonoBehaviour
{
     /*
    This script to act as the master for any operations done in the timer panel
    Setting timers, stopping them, viewing remaining time, etc.
    */

    [Header("References")]
    [SerializeField]
    private Slider timerBar;
    [SerializeField]
    private InputField minutesInputField;
    [SerializeField]
    private InputField secondsInputField;
    [SerializeField]
    private GameObject fiveMinuteModal;
    [SerializeField]
    private GameObject fifteenMinuteModal;
    [SerializeField]
    private GameObject thirtyMinuteModal;

    //TEMP
    [SerializeField]
    private GameObject confirmTimerPanel;

    [Header("Panel Settings")]
    [SerializeField]
    private Vector3 openAnchorPosition;
    [SerializeField]
    private Vector3 closedAnchorPosition;
    [SerializeField]
    private float openLerpSpeed;
    [SerializeField]
    private float closeLerpSpeed;

    [Header("Pay")]
    [Tooltip("How long it takes for the player to get paid once")]
    [SerializeField]
    private float payTime;
    [SerializeField]
    private int payAmount;


    
    public float startingTime;
    public float remainingTime;


    bool fiveMinuteShown;
    bool fifteenMinuteShown;
    bool thirtyMinuteShown;

    float timeToNextPay;

    bool timerGoing;
    bool open;
    RectTransform thisRect;

    void Start()
    {
        thisRect = GetComponent<RectTransform>();
        open = false;
        timeToNextPay = payTime;
    }

    void Update(){
        UpdateCredits();
        UpdateTimer();
        UpdateGFX();
    }

    void UpdateCredits(){
        if(timerGoing){
            timeToNextPay -= Time.deltaTime;


            //Get Paid
            if(timeToNextPay <= 0){
                timeToNextPay = payTime;
            
                GameManager.current.AddCredits(payAmount);
                //Play Pay Sound
            }
        }        
    }

    void UpdateTimer(){
        if(!timerGoing) return;

        remainingTime -= Time.deltaTime / 60; //Turn time factor into minutes

        if(remainingTime <= 0){
            SetTimerOff();
        }
    }

    //When the timer goes off (BEEP BEEP)
    void SetTimerOff(){
        startingTime = 0;
        timerGoing = false;
    }

    void UpdateGFX(){
        if(open){
            thisRect.anchoredPosition = Vector3.Lerp(thisRect.anchoredPosition, openAnchorPosition, openLerpSpeed * Time.deltaTime);
        }
        else{
            thisRect.anchoredPosition = Vector3.Lerp(thisRect.anchoredPosition, closedAnchorPosition, closeLerpSpeed * Time.deltaTime);
        }

        if(timerGoing){
            //Set Timer Bar
            timerBar.value = remainingTime / startingTime;

            // 30 Minute Modal
            if(startingTime >= 30){
                if(remainingTime < 30){
                    if(!thirtyMinuteShown){
                        thirtyMinuteModal.SetActive(true);
                        thirtyMinuteShown = true;
                    }
                }
            }
            
            // 15 Minute Modal
            if(startingTime >= 15){
                if(remainingTime < 15){
                    if(!fifteenMinuteShown){
                        fifteenMinuteModal.SetActive(true);
                        fifteenMinuteShown = true;
                    }
                }
            }
            
            //5 Minute Modal
            if(startingTime >= 5){
                if(remainingTime < 5){
                    if(!fiveMinuteShown){
                        fiveMinuteModal.SetActive(true);
                        fiveMinuteShown = true;
                    }
                }
            }

            //Update remaining time text in panel
        }else{
            timerBar.value = 1;
        }
    }

    public void OpenPanel(){
        open = true;
    }

    public void ClosePanel(){
        open = false;
    }

    public void TogglePanel(){
        open = !open;
    }

    public void StartTimerOnClick(){
        if(timerGoing){
            //Prompt are you sure overwrite other timer?????? BOZO????
            //TEMP
            confirmTimerPanel.SetActive(true);
        }else{
            StartTimer();
        }
    }

    public void StartTimer(){
        timerGoing = true;
        
        int minutes = int.Parse(minutesInputField.text);
        float seconds = (float)int.Parse(secondsInputField.text);
        startingTime = minutes + (seconds / 60);
        remainingTime = startingTime;

        //Set Modal Bools
        fiveMinuteShown = false;
        fifteenMinuteShown = false;
        thirtyMinuteShown = false;
    }
}
