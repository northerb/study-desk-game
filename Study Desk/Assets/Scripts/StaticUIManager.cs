using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StudyDesk.Utilities;

public class StaticUIManager : MonoBehaviour
{
    /*
    This script manages the large panels movement, allowing for all static items to access
    other objects indirectly. This script also manages the big picture timer stuff.
    */

    #region  Singleton

    public static StaticUIManager current;
    void Awake(){
        current = this;
    }
    #endregion

    [System.Serializable]
    public enum Panel{ Tasks = 0, Store = 1, Timer = 2}

    [Header("References")]
    [SerializeField]
    TaskPanel taskPanel;
    [SerializeField]
    TimerPanel timerPanel;

    [Header("Credits")]
    [SerializeField]
    private Transform creditChangeItemContainer;
    [SerializeField]
    private GameObject creditChangePrefab;
    [SerializeField]
    private Text creditText;
    [SerializeField]
    private float creditTextUpdateLerpTime;


    //These buttons should be on the right-hand side of the game, linked to each panel on this script.
    [Header("Side Buttons")]
    [SerializeField]
    ButtonAnimation[] buttonAnimationScripts;


    Panel currentOpenPanel;
    private Coroutine lastRoutine;
    private int currentTextCredits;

    public void TogglePanel(int panel){
        Panel newPanel = (Panel)panel;

        if(currentOpenPanel != newPanel){
            ClosePanel(currentOpenPanel);
        }

        currentOpenPanel = newPanel;

        switch(newPanel){
            case Panel.Tasks:
                taskPanel.TogglePanel();
                break;
            case Panel.Timer:
                timerPanel.TogglePanel();
                break;
            case Panel.Store:
                timerPanel.TogglePanel();
                break;
            default:
                break;
        }

        buttonAnimationScripts[(int)currentOpenPanel].ToggleSelect();
    }

    void ClosePanel(Panel _panel){
        switch((Panel)_panel){
            case Panel.Tasks:
                taskPanel.ClosePanel();
                break;
             case Panel.Timer:
                timerPanel.ClosePanel();
                break;
            case Panel.Store:
                timerPanel.ClosePanel();
                break;
            default:
                break;
        }
        buttonAnimationScripts[(int)currentOpenPanel].Deselect();
    }


    public void SetCredits(int newCredits, int changeAmount){
        if(changeAmount == 0) return;

        CreditChangeItem creditChangeItem = Instantiate(creditChangePrefab, creditChangeItemContainer).GetComponent<CreditChangeItem>();
        creditChangeItem.Initialize(changeAmount);

        if(lastRoutine != null){
            StopCoroutine(lastRoutine);
            lastRoutine = StartCoroutine(UpdateCreditCount(newCredits));
        }else{
            lastRoutine = StartCoroutine(UpdateCreditCount(newCredits));
        }
    }

    IEnumerator UpdateCreditCount(int newCredits){
        float lerp = 0f, duration = creditTextUpdateLerpTime;
        int score = currentTextCredits;
        int scoreTo = newCredits;

        while(currentTextCredits != newCredits){
            //Calculate lerp int

            lerp += Time.deltaTime / duration;
            score = (int)Mathf.Lerp(score, scoreTo, lerp);
     
            currentTextCredits = score;

            creditText.text = currentTextCredits + " CREDITS";
            yield return new WaitForEndOfFrame();
        }
    }




    
}
