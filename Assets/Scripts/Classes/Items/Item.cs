using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
    public Animator animator = null;
    
    public List<Collider2D> colliders = null;
    public List<Collider2D> triggers = null;
    
    // Use this for initialization
    void Start() {
        Initialize();
    }
    
    // Update is called once per frame
    void Update() {
        UpdateAnimator(animator);
    }
    
    public virtual void Initialize() {
        // Colliders are set here
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
        //---------------------
        if(animator == null) {
            animator = this.gameObject.GetComponent<Animator>();
            if(animator == null) {
                Debug.LogError("Animator is missing from the 'Item' script of the gameObject: " + this.gameObject.name);
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
    //--------------------------------------------------------------------------
    public virtual void UpdateAnimator(Animator animatorToUpdate) {
    }
}
