using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
    public Animator animator = null;
    
    public SpeechBubble speechBubble = null;
    public GameObject speechBubbleAnchor = null;
    
    public List<Collider2D> colliders = null;
    public List<Collider2D> triggers = null;
    
    protected virtual void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected virtual void Start() {
    }
    
    // Update is called once per frame
    protected virtual void Update() {
        UpdateAnimator(animator);
    }
    
    protected virtual void Initialize() {
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
        // speech bubble is optional, does not need to be assigned
        if(speechBubble != null
            && speechBubbleAnchor == null) {
            this.gameObject.LogComponentError("speechBubbleAnchor", this.GetType());
        }
        // speech bubble anchor is optional, and does not need to be assigned
    }
    
    public void SetFacing(bool isFacingRight) {
        if(isFacingRight) {
            if(this.gameObject.transform.localScale.x < 0) {
                this.gameObject.transform.localScale = new Vector3((this.gameObject.transform.localScale.x * -1),
                                                                   this.gameObject.transform.localScale.y,
                                                                   this.gameObject.transform.localScale.z);
            }
        }
        // facing left
        else {
            if(this.gameObject.transform.localScale.x > 0) {
                this.gameObject.transform.localScale = new Vector3((this.gameObject.transform.localScale.x * -1),
                                                                   this.gameObject.transform.localScale.y,
                                                                   this.gameObject.transform.localScale.z);
            }
        }
    }
    
    public void SetSpeechBubbleFacing(bool isFacingRight) {
        // if(speechBubble != null) {
        //     float currentFacing = this.gameObject.transform.localScale.x;
        //     if(currentFacing > 0) {
        //         speechBubble.SetFacing(true);
        //     }
        //     else {
        //         speechBubble.SetFacing(false);
        //     }
        // }
        if(speechBubble != null) {
            if(isFacingRight) {
                speechBubble.SetFacing(true);
            }
            else {
                speechBubble.SetFacing(false);
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
