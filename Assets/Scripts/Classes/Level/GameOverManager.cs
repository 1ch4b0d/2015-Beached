using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameOverManager : MonoBehaviour {
    public CustomEventsManager gameOverEventManager = null;
    
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile GameOverManager _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static GameOverManager() {
    }
    
    public static GameOverManager Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<GameOverManager>();
                    if(_instance == null) {
                        GameObject GameOverManagerGameObject = new GameObject("GameOverManager");
                        _instance = (GameOverManagerGameObject.AddComponent<GameOverManager>()).GetComponent<GameOverManager>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private GameOverManager() {
    }
    
    protected void Awake() {
        Initialize();
    }
    //END OF SINGLETON CODE CONFIGURATION
    
    protected void Start() {
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    protected void Initialize() {
        _instance = this;
        
        if(gameOverEventManager == null) {
            gameOverEventManager = this.gameObject.GetComponent<CustomEventsManager>();
            if(gameOverEventManager == null) {
                Debug.LogError(this.gameObject.name + " needs its 'gameOverEventManager' reference to be set in the 'GameOverManager' Script");
            }
        }
    }
    
    public void TriggerGameOver() {
        if(gameOverEventManager != null) {
            gameOverEventManager.Execute();
        }
        else {
            Debug.Log(this.gameObject.name + " has a variable 'gameOverEventManager' that is not set in the 'GameOverManager' script.");
        }
    }
}