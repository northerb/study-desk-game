using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StudyDesk.Utilities;

public class TaskItem : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private InputField taskInputField;
    [SerializeField]
    private Button taskFinishButton;
    [SerializeField]
    private Text taskText;

    [Header("Button Animation References")]
    [SerializeField]
    private ButtonAnimation inputFieldAnimator;
    [SerializeField]
    private ButtonAnimation finishButtonAnimator;
    

    [Header("Settings")]
    [SerializeField]
    private Vector3 lerpPos1;
    [SerializeField]
    private Vector3 lerpPos2;

    [SerializeField]
    private float lerpSpeed1;
    [SerializeField]
    private float lerpSpeed2;


    [SerializeField]
    private RectTransform itemRect;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LerpPositions());
    }

    public void FinishTaskOnClick(){
        taskText.text = taskInputField.text;

        taskText.gameObject.SetActive(true);

        // taskInputField.gameObject.SetActive(false);
        // taskFinishButton.gameObject.SetActive(false);

        inputFieldAnimator.OnHoverOver();
        finishButtonAnimator.OnHoverOver();
       
    }



    IEnumerator LerpPositions(){
        while(Vector3.Distance(itemRect.anchoredPosition, lerpPos1) > 1){
            itemRect.anchoredPosition = Vector3.Lerp(itemRect.anchoredPosition, lerpPos1, lerpSpeed1 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        while(Vector3.Distance(itemRect.anchoredPosition, lerpPos2) > 1){
            itemRect.anchoredPosition = Vector3.Lerp(itemRect.anchoredPosition, lerpPos2, lerpSpeed2 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    
}
