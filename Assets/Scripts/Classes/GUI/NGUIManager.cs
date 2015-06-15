using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NGUIManager : MonoBehaviour {
    UIRoot uiRoot = null;
    Camera guiCamera = null;
    
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
                    if(_instance == null) {
                        GameObject NGUIManagerGameObject = new GameObject("NGUIManager");
                        _instance = (NGUIManagerGameObject.AddComponent<NGUIManager>()).GetComponent<NGUIManager>();
                    }
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
    
    public UIRoot UIRoot() {
        if(uiRoot == null) {
            uiRoot = GameObject.FindObjectOfType<UIRoot>();
        }
        
        return uiRoot;
    }
    
    public Camera Camera() {
        if(guiCamera == null) {
            guiCamera = UIRoot().transform.Find("Camera").GetComponent<Camera>();
        }
        // return UIRoot().transform.Find("Camera").GetComponent<Camera>();
        return guiCamera;
    }
}

