using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class DebugCustomEventsManager : MonoBehaviour {
    public CustomEventsManager customEventsManager = null;
    public KeyCode debugKeyCode = KeyCode.Return;
    
    protected void Awake() {
        Initialize();
    }
    
    protected void Start() {
    }
    
    protected void Update() {
        if(InputManager.Instance.GetKeyDown(debugKeyCode)) {
            // Debug.Log("Executing");
            customEventsManager.Execute();
        }
    }
    
    protected void Initialize() {
        if(customEventsManager == null) {
            this.gameObject.LogComponentError("customEventsManager", this.GetType());
        }
    }
}