using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile Player _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static PlayerManager() {
    }
    
    public static Player Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<Player>();
                    if(_instance == null) {
                        GameObject CameraManagerGameObject = new GameObject("PlayerManager");
                        _instance = (CameraManagerGameObject.AddComponent<Player>()).GetComponent<Player>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private PlayerManager() {
    }
    
    public void Awake() {
    }
    //END OF SINGLETON CODE CONFIGURATION
    
    public void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public GameObject GetPlayer() {
        return Instance.gameObject;
    }
}