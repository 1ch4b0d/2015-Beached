using UnityEngine;
using System.Collections;

public class CustomEventObject : MonoBehaviour {
    public CustomEvent<System.Action> customEvent = null;
    public bool loop = true;
    
    protected virtual void Awake() {
        customEvent = CustomEvent<System.Action>.Create();
        customEvent.SetEvent(Execute);
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
    }
    
    public virtual void Execute() {
    }
}
