using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager current;
    void Awake(){
        current = this;
    }

    #endregion

    public static int credits;

    // Start is called before the first frame update
    void Start()
    {
        LoadStats();
    }

    void LoadStats(){
        //Load Credits, tasks, current timer,
        //Set world items based on load location id and item id.
        StaticUIManager.current.SetCredits(credits, 0);
    }
    

    void Update(){
        if(Input.anyKeyDown){
            AddCredits(Random.Range(-100,100));
        }
    }

    public void AddCredits(int amount){
        credits += amount;
        Debug.Log("Current Credits = " + credits);
        StaticUIManager.current.SetCredits(credits, amount);
    }
}
