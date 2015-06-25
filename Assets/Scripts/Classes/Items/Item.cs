using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
    // TODO: Refactor this so it separates colliders and triggers into separate
    //       lists and then disable triggers only on pick up and renable on drop
    //       and throw, but again on triggers.
    public List<Collider2D> colliders = null;
    public List<Collider2D> triggers = null;
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
    }
    
    public virtual void Initialize() {
        Collider2D[] childColliders = this.gameObject.GetComponentsInChildren<Collider2D>();
        // Debug.Log(childColliders.Length);
        foreach(Collider2D collider in childColliders) {
            if(collider.isTrigger) {
                triggers.Add(collider);
            }
            else {
                colliders.Add(collider);
            }
        }
    }
    
    //--------------------------------------------------------------------------
    public virtual void DisableTriggers() {
        ToggleTriggers(false);
    }
    
    public virtual void EnableTriggers() {
        ToggleTriggers(true);
    }
    
    public virtual void ToggleTriggers(bool enabled) {
        // Debug.Log("Setting all triggers to: " + enabled);
        // Debug.Log(triggers.Count);
        foreach(Collider2D collider in triggers) {
            collider.enabled = enabled;
        }
    }
    //--------------------------------------------------------------------------
    public virtual void DisableColliders() {
        ToggleColliders(false);
    }
    
    public virtual void EnableColliders() {
        ToggleColliders(true);
    }
    
    public virtual void ToggleColliders(bool enabled) {
        // Debug.Log("Setting all colliders to: " + enabled);
        // Debug.Log(colliders.Count);
        foreach(Collider2D collider in colliders) {
            collider.enabled = enabled;
        }
    }
}
