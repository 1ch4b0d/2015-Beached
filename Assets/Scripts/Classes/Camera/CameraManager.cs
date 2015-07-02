using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour {

    public Camera mainCamera = null;
    
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile CameraManager _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static CameraManager() {
    }
    
    public static CameraManager Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<CameraManager>();
                    if(_instance == null) {
                        GameObject CameraManagerGameObject = new GameObject("CameraManager");
                        _instance = (CameraManagerGameObject.AddComponent<CameraManager>()).GetComponent<CameraManager>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private CameraManager() {
    }
    
    public void Awake() {
        _instance = this;
    }
    //END OF SINGLETON CODE CONFIGURATION
    
    public void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public Camera GetMainCamera() {
        if(mainCamera == null) {
            mainCamera = Camera.main;
        }
        if(mainCamera == null) {
            Debug.LogError("The mainCamera is returning null, fix it.");
        }
        return mainCamera;
    }
    
    public CameraFollow CameraFollow() {
        return GetMainCamera().GetComponent<CameraFollow>();
    }
    
    public Camera GUICamera() {
        return NGUIManager.Instance.Camera();
    }
}