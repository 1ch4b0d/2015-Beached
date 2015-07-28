using UnityEngine;
using System.Collections;

public class BaseRigidbodyHelper : MonoBehaviour {
    public bool clampVelocity = false;
    // public float minVelocity = -1f;
    // public float maxVelocity = 1f;
    public Vector3 minVelocity = new Vector3(-1, -1, 0);
    public Vector3 maxVelocity = new Vector3(1, 1, 0);
    
    // Use this for initialization
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
    }
    
    // Update is called once per frame
    protected virtual void LateUpdate() {
        if(clampVelocity) {
            ClampVelocity();
        }
    }
    
    protected virtual void Initialize() {
    }
    
    public virtual void ClampVelocity() {
    }
}
