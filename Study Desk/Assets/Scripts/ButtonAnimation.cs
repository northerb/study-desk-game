using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StudyDesk.Utilities{
    
    public class ButtonAnimation : MonoBehaviour
    {
        /*
        This script allows for a button to be animated through pointer events. 
        These settings are manipulated in the inspector.
        */

        [Header("References")]
        [SerializeField]
        private RectTransform buttonRect;
        [SerializeField]
        private Image image;
        [SerializeField]
        private Text buttonText;

        [Header("Settings")]
        [SerializeField]
        private Color lerpToButtonColor;
        [SerializeField]
        private Color selectedTextColor;
        [SerializeField]
        private Vector3 lerpToPos;
        [SerializeField]
        private Vector2 lerpToSize;
        [SerializeField]
        private float lerpSpeed;


        private Vector3 initialRectPos;
        private Vector2 initialSize;
        private Color initialTextColor;
        private Color initialButtonColor;        
        private Coroutine lastRoutine;

        private bool selected = false;

        void Start(){
            initialRectPos = buttonRect.anchoredPosition;
            initialSize = buttonRect.sizeDelta;
            initialButtonColor = image.color;
            initialTextColor = buttonText.color;
        }

        public void OnHoverOver(){
            if(lastRoutine != null) StopCoroutine(lastRoutine);
            lastRoutine = StartCoroutine(HoverOver());
        }

        public void OnHoverOff(){
            if(lastRoutine != null) StopCoroutine(lastRoutine);
            lastRoutine = StartCoroutine(HoverOff());
        }

        public void ToggleSelect(){
            selected = !selected;

            if(selected){
                buttonText.color = selectedTextColor;
                buttonText.fontStyle = FontStyle.Bold;
            }
            else{
                buttonText.color = initialTextColor;
                buttonText.fontStyle = FontStyle.Normal;
            }

            //Play Click Sound
        }

        public void Select(){
            selected = true;

            buttonText.color = selectedTextColor;
            buttonText.fontStyle = FontStyle.Bold;

            //Play Click Sound
        }

        public void Deselect(){
            selected = false;

            buttonText.color = initialTextColor;
            buttonText.fontStyle = FontStyle.Normal;

            //Play Click Sound
        }

        #region Coroutines

        IEnumerator HoverOver(){
            while(Vector3.Distance(buttonRect.anchoredPosition, lerpToPos) > .1f){
                buttonRect.anchoredPosition = Vector3.Lerp(buttonRect.anchoredPosition, lerpToPos, Time.deltaTime * lerpSpeed);
                buttonRect.sizeDelta = Vector2.Lerp(buttonRect.sizeDelta, lerpToSize, Time.deltaTime * lerpSpeed);
                image.color = Color.Lerp(image.color, lerpToButtonColor, Time.deltaTime * lerpSpeed);
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }

        IEnumerator HoverOff(){
            while(Vector3.Distance(buttonRect.anchoredPosition, initialRectPos) > .1f){
                buttonRect.anchoredPosition = Vector3.Lerp(buttonRect.anchoredPosition, initialRectPos, Time.deltaTime * lerpSpeed);
                buttonRect.sizeDelta = Vector2.Lerp(buttonRect.sizeDelta, initialSize, Time.deltaTime * lerpSpeed);
                image.color = Color.Lerp(image.color, initialButtonColor, Time.deltaTime * lerpSpeed);
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }

        #endregion
    }
}
