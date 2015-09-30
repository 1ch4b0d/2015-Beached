using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// These rely directly on the manager to determine what they should be detecting
// It is a very tightly coupled script and should be assumed to be an class that was
// created in order to be used as a helper function linked to the actual collision detection
// DO NOT: give this any responsibility outside of being an interface hook for the colliderDetectionManager
public class ColliderManagerDetector : MonoBehaviour {
    public ColliderManager colliderManager = null;
    
    // Use this for initialization
    protected void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected void Start() {
    }
    
    // Update is called once per frame
    protected void Update() {
    }
    
    // Update is called once per frame
    protected void LateUpdate() {
    }
    
    // Update is called once per frame
    protected void Initialize() {
        // the collider manager MUST be declared, because the class makes the assumption it is declared ahead of time
        if(colliderManager == null) {
            colliderManager = this.gameObject.GetFirstParent<ColliderManager>();
            if(colliderManager == null) {
                this.gameObject.LogComponentError("colliderManager", this.GetType());
            }
        }
    }
    
    protected void OnCollisionEnter2D(Collision2D enterCollision2D) {
        // Debug.Log("entered");
        colliderManager.Add(enterCollision2D.gameObject.layer, enterCollision2D.gameObject);
    }
    
    protected void OnCollisionStay2D(Collision2D stayCollision2D) {
        // lol do nothing, maybe think about adding, but eh, doesn't make sense to me
    }
    
    protected void OnCollisionExit2D(Collision2D exitCollision2D) {
        // Debug.Log("exited");
        colliderManager.Remove(exitCollision2D.gameObject.layer, exitCollision2D.gameObject);
    }
}