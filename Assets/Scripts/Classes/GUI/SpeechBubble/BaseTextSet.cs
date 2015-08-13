using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseTextSet : MonoBehaviour {
    public List<string> textSet = null;
    
    // Use this for initialization
    protected virtual void Awake() {
        Awake();
    }
    
    // Use this for initialization
    protected virtual void Start() {
        Start();
    }
    
    // Update is called once per frame
    protected virtual void Update() {
        Update();
    }
    
    protected virtual void Initialize() {
        if(textSet == null) {
            Debug.LogError(this.gameObject.name + " needs its 'textSet' reference to be set in the 'BaseTextSet' Script");
        }
    }
    
    public virtual List<string> GetTextSet() {
        return textSet;
    }
}
