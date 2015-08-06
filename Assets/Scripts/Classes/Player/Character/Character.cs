using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
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
    
    protected virtual void PerformLogic() {
    }
}