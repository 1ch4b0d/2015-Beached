using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour {

    public GameObject mainCamera = null;
    public GameObject guiCamera = null;
    
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
    
    public GameObject GetMainCamera() {
        return mainCamera;
    }
    public GameObject GetGUICamera() {
        return guiCamera;
    }
}
