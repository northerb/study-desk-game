using UnityEngine;
using UnityEngine.UI;
using System;

/*
This script links the current Time text to the current computer system's time of day.
*/

public class TimeOfDay : MonoBehaviour
{
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Text amPMText;

    void Update(){
        timeText.text = DateTime.Now.ToString("hh:mm");
        amPMText.text = DateTime.Now.ToString("tt");
    }
}
