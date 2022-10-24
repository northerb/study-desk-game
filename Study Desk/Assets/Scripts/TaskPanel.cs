using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : MonoBehaviour
{
    /*
    This script to act as the master for any operations done in the task panel
    Creating tasks, editing tasks, removing tasks, etc.
    */

    [Header("References")]
    [SerializeField]
    private Transform taskItemContainer;
    [SerializeField]
    private GameObject taskItemPrefab;

    [Header("Panel Settings")]
    [SerializeField]
    private Vector3 openAnchorPosition;
    [SerializeField]
    private Vector3 closedAnchorPosition;
    [SerializeField]
    private float openLerpSpeed;
    [SerializeField]
    private float closeLerpSpeed;

    bool open;

    RectTransform thisRect;
    void Start()
    {
        thisRect = GetComponent<RectTransform>();
        open = false;
    }

    void Update(){
        UpdateGFX();
    }

    void UpdateGFX(){
        if(open){
            thisRect.anchoredPosition = Vector3.Lerp(thisRect.anchoredPosition, openAnchorPosition, openLerpSpeed * Time.deltaTime);
        }
        else{
            thisRect.anchoredPosition = Vector3.Lerp(thisRect.anchoredPosition, closedAnchorPosition, closeLerpSpeed * Time.deltaTime);
        }
    }

    public void CreateTaskOnClick(){
        CreateTask();
    }

    public void CreateTask(){
        Instantiate(taskItemPrefab, taskItemContainer);
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
}
