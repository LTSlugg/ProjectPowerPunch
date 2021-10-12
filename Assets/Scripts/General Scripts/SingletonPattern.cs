using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonPattern : MonoBehaviour
{
    //A standard Singleton pattern script

    #region Singleton Code & Awake Function
    private static SingletonPattern _instance; //TODO: Change this to your current Class Name

    void Awake()
    {
        #region Singleton Code
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Rest of your Awake code

        }
        else
        {
            Destroy(this);
        }
        #endregion

    }
    #endregion
}
