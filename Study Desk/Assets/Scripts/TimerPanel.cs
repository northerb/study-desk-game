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
    private Text timerNameText;
    [SerializeField]
    private InputField timerNameInputField;
    [SerializeField]
    private InputField minutesInputField;
    [SerializeField]
    private InputField secondsInputField;
    [SerializeField]
    private GameObject timeRemainingHelperText;
    [SerializeField]
    private Text timeRemainingText;
    [SerializeField]
    private Text errorText;
    [SerializeField]
    private GameObject fiveMinuteModal;
    [SerializeField]
    private GameObject fifteenMinuteModal;
    [SerializeField]
    private GameObject thirtyMinuteModal;
    [SerializeField]
    private CanvasGroup confirmTimerPanel;
    [SerializeField]
    private Button stopTimerButton;

    [Header("Panel Settings")]
    [SerializeField]
    private Vector3 openAnchorPosition;
    [SerializeField]
    private Vector3 closedAnchorPosition;
    [SerializeField]
    private float openLerpSpeed;
    [SerializeField]
    private float closeLerpSpeed;
    [SerializeField]
    private float lerpConfirmPanelSpeed;

    [Header("Pay")]
    [Tooltip("How long it takes for the player to get paid once")]
    [SerializeField]
    private float payTime;
    [SerializeField]
    private int payAmount;


    
    float startingTime;
    float remainingTime;

    string timerName;

    bool fiveMinuteShown;
    bool fifteenMinuteShown;
    bool thirtyMinuteShown;

    float timeToNextPay;

    bool timerGoing;
    bool open;
    RectTransform thisRect;

    Coroutine lastRoutine;

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
            StopTimer();
        }
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
            string minutes = ((int)remainingTime).ToString();
            string seconds = ((int)((remainingTime - (int)remainingTime) * 60)).ToString();
            timeRemainingText.text = minutes + " Minutes, " + seconds + " Seconds";

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
            if(lastRoutine != null) StopCoroutine(lastRoutine);
            lastRoutine = StartCoroutine(LerpConfirmPanel(true));
        }else{
            StartTimer();
        }
    }

    public void CancelTimerOnClick(){
        StopTimer();
    }

    public void NotSureOnClick(){
        if(lastRoutine != null) StopCoroutine(lastRoutine);
        lastRoutine = StartCoroutine(LerpConfirmPanel(false));
    }

    public void StopTimer(){
        timerName = "None";

        timerNameText.text = timerName;

        timerGoing = false;

        stopTimerButton.interactable = false;

        timeRemainingHelperText.SetActive(false);
        timeRemainingText.gameObject.SetActive(false);

        //Beep Beep
    }

    public void StartTimer(){
        if(int.TryParse(minutesInputField.text, out int minutes) && float.TryParse(secondsInputField.text, out float seconds)){
            timerGoing = true;

            timerName = timerNameInputField.text;

            timerNameText.text = timerName;


            stopTimerButton.interactable = true;

            timeRemainingHelperText.SetActive(true);
            timeRemainingText.gameObject.SetActive(true);

            startingTime = minutes + (seconds / 60);
            remainingTime = startingTime;

            //Set Modal Bools
            fiveMinuteShown = false;
            fifteenMinuteShown = false;
            thirtyMinuteShown = false;

            errorText.text = "";
        }else{
            errorText.text = "Please enter valid inputs!";
        }
        
    }

    IEnumerator LerpConfirmPanel(bool open){
        if(open){
            confirmTimerPanel.interactable = true;
            confirmTimerPanel.blocksRaycasts = true;
            while(confirmTimerPanel.alpha < 0.98){
                confirmTimerPanel.alpha = Mathf.Lerp(confirmTimerPanel.alpha, 1, lerpConfirmPanelSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            confirmTimerPanel.alpha = 1;
        }else{
            confirmTimerPanel.interactable = false;
            confirmTimerPanel.blocksRaycasts = false;
            while(confirmTimerPanel.alpha > 0.02){
                confirmTimerPanel.alpha = Mathf.Lerp(confirmTimerPanel.alpha, 0, lerpConfirmPanelSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            confirmTimerPanel.alpha = 0;
        }

    }
}
