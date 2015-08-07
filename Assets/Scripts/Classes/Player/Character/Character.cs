using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    public bool isPaused = false;
    
    // Use this for initialization
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
        PerformLogic();
    }
    
    protected virtual void Initialize() {
    }
    
    public virtual void Pause() {
    }
    
    public virtual void Unpause() {
    }
    
    public virtual bool IsPaused() {
        return isPaused;
    }
    
    protected virtual void PerformLogic() {
    }
}