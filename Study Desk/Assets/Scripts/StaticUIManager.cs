using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StudyDesk.Utilities;

public class StaticUIManager : MonoBehaviour
{
    /*
    This script manages the large panels movement, allowing for all static items to access
    other objects indirectly.
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
    TaskPanel storePanel;

    //These buttons should be on the right-hand side of the game, linked to each panel on this script.
    [Header("Side Buttons")]
    [SerializeField]
    ButtonAnimation[] buttonAnimationScripts;

    public Panel currentOpenPanel;


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
            case Panel.Store:
                storePanel.TogglePanel();
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
            case Panel.Store:
                storePanel.ClosePanel();
                break;
            default:
                break;
        }
        buttonAnimationScripts[(int)currentOpenPanel].Deselect();
    }
}
