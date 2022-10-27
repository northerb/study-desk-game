using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreditChangeItem : MonoBehaviour
{

    [Header("References")]
    [SerializeField]
    private Text itemText;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private RectTransform rect;


    
    [Header("Settings")]
    [SerializeField]
    private Color negativeAmountColor;
    [SerializeField]
    private Color positiveAmountColor;
    [SerializeField]
    private float lerpSpeed;
    [SerializeField]
    private Vector3 lerpToPos;


    public void Initialize(int changeAmount){
        
        if(changeAmount < 0){
            //If deducted amount
            itemText.text = changeAmount + " CREDITS";
            itemText.color = negativeAmountColor;
        }
        else{
            //If added amount
            itemText.text = "+" + changeAmount + " CREDITS";
            itemText.color = positiveAmountColor;
        }

        StartCoroutine(LerpAlpha());
    }

    IEnumerator LerpAlpha(){

        while(canvasGroup.alpha > 0.02f){
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, lerpSpeed * Time.deltaTime);
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition, lerpToPos, lerpSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject);
    }
}
