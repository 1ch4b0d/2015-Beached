using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// TODO: It's probably better to either a) Make it so that it iterates through all
//       the keycodes every execution, instead of ignoring them, because right now
//       they have to be explicitly run through the update function in this script
//       b) make it so that it only does it for items that don't exist in the
//       actual input. This means that it will serve of the Input.Get* logic as a
//       primary, and then if it doesn't work, fall back to a cache of values that
//       are arbitrary. I don't know, these are all hypotheticals
public class InputManager : MonoBehaviour {
    public Dictionary<string, bool> onKeyDown = null;
    public Dictionary<string, bool> onKeyHeld = null;
    public Dictionary<string, bool> onKeyUp = null;
    
    //BEGINNING OF SINGLETON CODE CONFIGURATION
    private static volatile InputManager _instance;
    private static object _lock = new object();
    
    //Stops the lock being created ahead of time if it's not necessary
    static InputManager() {
    }
    
    public static InputManager Instance {
        get {
            if(_instance == null) {
                lock(_lock) {
                    _instance = GameObject.FindObjectOfType<InputManager>();
                    if(_instance == null) {
                        GameObject CameraManagerGameObject = new GameObject("InputManager");
                        _instance = (CameraManagerGameObject.AddComponent<InputManager>()).GetComponent<InputManager>();
                    }
                }
            }
            return _instance;
        }
    }
    
    private InputManager() {
    }
    
    // Use this for initialization
    protected void Awake() {
        Initalize();
    }
    // END OF SINGLETON CODE CONFIGURATION
    
    // Use this for initialization
    protected void Start() {
    }
    
    // Update is called once per frame
    protected void Update() {
        // These are the keys to check for
        PerformInputLogic("Run");
        PerformInputLogic("Dash");
        PerformInputLogic("Jump");
        PerformInputLogic("Crouch");
        PerformInputLogic("Submit");
    }
    
    protected void LateUpdate() {
        // Reset();
    }
    
    protected void Reset() {
        foreach(string key in new List<string>(onKeyDown.Keys)) {
            onKeyDown[key] = false;
        }
        foreach(string key in new List<string>(onKeyHeld.Keys)) {
            onKeyHeld[key] = false;
        }
        foreach(string key in new List<string>(onKeyUp.Keys)) {
            onKeyUp[key] = false;
        }
    }
    // Update is called once per frame
    protected void Initalize() {
        onKeyDown = new Dictionary<string, bool>();
        onKeyHeld = new Dictionary<string, bool>();
        onKeyUp = new Dictionary<string, bool>();
    }
    
    public void SetButtonDown(string keyName, bool isDown) {
        onKeyDown[keyName.ToLower()] = isDown;
    }
    public bool GetButtonDown(string keyName) {
        return GetKeyDown(keyName);
    }
    
    public void SetButton(string keyName, bool isKeyHeld) {
        onKeyHeld[keyName.ToLower()] = isKeyHeld;
    }
    public bool GetButton(string keyName) {
        return GetKey(keyName);
    }
    
    public void SetButtonUp(string keyName, bool isKeyUp) {
        onKeyUp[keyName.ToLower()] = isKeyUp;
    }
    public bool GetButtonUp(string keyName) {
        return GetKeyUp(keyName);
    }
    //--------------------------------------------------------------------------
    public void SetKeyDown(string keyName, bool isDown) {
        onKeyDown[keyName.ToLower()] = isDown;
    }
    public bool GetKeyDown(string keyName) {
        bool isKeyDown = false;
        onKeyDown.TryGetValue(keyName.ToLower(), out isKeyDown);
        return isKeyDown;
    }
    
    public void SetKeyDown(KeyCode keyCode, bool isDown) {
        SetKeyDown(keyCode.ToString().ToLower(), isDown);
    }
    public bool GetKeyDown(KeyCode keyCode) {
        return GetKeyDown(keyCode.ToString());
    }
    //---------------------
    public void SetKey(string keyName, bool isKeyHeld) {
        onKeyHeld[keyName.ToLower()] = isKeyHeld;
    }
    public bool GetKey(string keyName) {
        bool isKeyHeld = false;
        onKeyHeld.TryGetValue(keyName.ToLower(), out isKeyHeld);
        return isKeyHeld;
    }
    
    public void SetKey(KeyCode keyCode, bool isKeyHeld) {
        SetKey(keyCode.ToString(), isKeyHeld);
    }
    public bool GetKey(KeyCode keyCode) {
        return GetKey(keyCode.ToString());
    }
    //--------------------------
    public void SetKeyUp(string keyName, bool isKeyUp) {
        onKeyUp[keyName.ToLower()] = isKeyUp;
    }
    public bool GetKeyUp(string keyName) {
        bool isKeyUp = false;
        onKeyUp.TryGetValue(keyName.ToLower(), out isKeyUp);
        return isKeyUp;
    }
    
    public void SetKeyUp(KeyCode keyCode, bool isKeyUp) {
        SetKey(keyCode.ToString().ToLower(), isKeyUp);
    }
    public bool GetKeyUp(KeyCode keyCode) {
        return GetKeyUp(keyCode.ToString());
    }
    //--------------------------
    
    public float GetAxis(string axis) {
        return Input.GetAxis(axis);
    }
    
    protected void PerformInputLogic(string keyName) {
        InputManager.Instance.SetButtonDown(keyName, Input.GetButtonDown(keyName));
        InputManager.Instance.SetButton(keyName, Input.GetButton(keyName));
        InputManager.Instance.SetButtonUp(keyName, Input.GetButtonUp(keyName));
        
        foreach(KeyCode keyCode in KeyCode.GetValues(typeof(KeyCode))) {
            string keyCodeString = keyCode.ToString();
            InputManager.Instance.SetButtonDown(keyCodeString, Input.GetKeyDown(keyCode));
            InputManager.Instance.SetButton(keyCodeString, Input.GetKey(keyCode));
            InputManager.Instance.SetButtonUp(keyCodeString, Input.GetKeyUp(keyCode));
        }
        // if(Input.GetButtonDown(keyName)) {
        //     InputManager.Instance.SetKeyDown(keyName, true);
        // }
        // if(Input.GetButton(keyName)) {
        //     InputManager.Instance.SetKey(keyName, true);
        // }
        // if(Input.GetButtonUp(keyName)) {
        //     InputManager.Instance.SetKeyUp(keyName, true);
        // }
    }
}
