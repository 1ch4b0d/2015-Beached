using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGUIManager : MonoBehaviour {
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile NGUIManager _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static NGUIManager() {
    }
    
    public static NGUIManager Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<NGUIManager>();
                    // if(_instance == null) {
                    //     GameObject NGUIManagerGameObject = new GameObject("NGUIManager");
                    //     _instance = (NGUIManagerGameObject.AddComponent<NGUIManager>()).GetComponent<NGUIManager>();
                    // }
                }
            }
            return _instance;
        }
    }
    
    private NGUIManager() {
    }
    
    public void Awake() {
        _instance = this;
    }
    //END OF SINGLETON CODE CONFIGURATION
}

